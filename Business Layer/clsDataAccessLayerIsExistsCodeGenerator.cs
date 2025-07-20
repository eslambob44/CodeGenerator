using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class clsDataAccessLayerIsExistsCodeGenerator : IDataAccessLayerCodeGenerator
    {

        private string GenerateSqlParameters(ITableInfo Table, ICollection<string> Columns)
        {
            return string.Join(",", Table.Columns.
                Where(kvp => Columns.Contains(kvp.Key)).
                Select(kvp => $"@{kvp.Key} {kvp.Value.SqlDataType}"));
        }

        private string GenerateCParameters(ITableInfo Table, ICollection<string> Columns)
        {
            return string.Join(",", Table.Columns.
                Where(kvp => Columns.Contains(kvp.Key)).
                Select(kvp => $"{kvp.Value.DataType} {kvp.Key} "));
        }

        private bool GenerateStoredProcedure(ITableInfo Table, string Parameters, string ObjectName, ICollection<string> UniqueColumns)
        {
            string StoredProcedure = $@"CREATE procedure [dbo].[SP_Is{string.Join("And" ,UniqueColumns)}UsedBeforeIn{Table.TableName}] ({Parameters})

AS

BEGIN 
	
	SELECT 1 FROM [{Table.TableName}]
	Where {string.Join("AND" , UniqueColumns.Select(item =>$"{item} = @{item}"))};
	return @@ROWCOUNT;
	
END ";
            return clsExecuteStoredProcedureData.AddStoredProcedure(Table.ConnectionString, StoredProcedure);
        }


        private string GenerateDataAccessLayerIsUsedBeforeCode(ITableInfo Table, string Parameters, string ObjectName, ICollection<string> UniqueColumns)
        {
            string Code = $@"static public bool Is{string.Join("And", UniqueColumns)}UsedBefore({Parameters})
        {{
            bool IsUsed = false;
            try
            {{
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {{
                    using (SqlCommand Command = new SqlCommand(""SP_Is{string.Join("And", UniqueColumns)}UsedBeforeIn{Table.TableName}"", Connection))
                    {{
                        Command.CommandType = CommandType.StoredProcedure;
                        ";

            foreach(var item in UniqueColumns)
            {
                Code += $"\nCommand.Parameters.AddWithValue(\"@{item}\" , {item});";
            }

                        Code+=$@"
                        SqlParameter ReturnParam = new SqlParameter(""@OutPut"", SqlDbType.Int);
                        ReturnParam.Direction = ParameterDirection.ReturnValue;
                        Command.Parameters.Add(ReturnParam);
                        Connection.Open();
                        Command.ExecuteNonQuery();
                        IsUsed = Convert.ToInt32(ReturnParam.Value) > 0;
                    }}
                }}
            }}
            catch (Exception ex)
            {{
                OnErrorOccur?.Invoke(ex);
            }}
            return IsUsed;
        }}";
            return Code;

        }




        public string GenerateCode(ITableInfo Table, string ObjectName)
        {
            string Code = "";
            Dictionary<string, HashSet<string>> columns = new Dictionary<string, HashSet<string>>();
            foreach (var kvp in Table.UniqueColumns)
            {
                columns.Add(kvp.Key, kvp.Value);
            }

            foreach (var kvp in columns)
            {
                if(GenerateStoredProcedure(Table, GenerateSqlParameters(Table, kvp.Value), ObjectName, kvp.Value))
                    Code += GenerateDataAccessLayerIsUsedBeforeCode(Table, GenerateCParameters(Table, kvp.Value), ObjectName, kvp.Value);
            }
            return Code;

        }
    }
}
