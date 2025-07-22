using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class clsDataAccessLayerFindCodeGenerator: IDataAccessLayerCodeGenerator
    {


         private string GenerateSqlParameters(ITableInfo Table, ICollection<string> Columns)
        {
            return string.Join(",", Table.Columns.
                Where(kvp => Columns.Contains(kvp.Key)).
                Select(kvp => $"@{kvp.Key} {kvp.Value.SqlDataType}"));
        }

         private string GenerateCParameters(ITableInfo Table, ICollection<string> Columns)
        {
            string Param =  string.Join(",", Table.Columns.
                Where(kvp => Columns.Contains(kvp.Key)).
                Select(kvp => $"{kvp.Value.DataType} {kvp.Key} "));
            Param += "," + string.Join(",", Table.Columns.Where(kvp => !Columns.Contains(kvp.Key)).Select(kvp => $"ref {kvp.Value.DataType} {kvp.Key}"));
            return Param;
        }

         private bool GenerateStoredProcedure(ITableInfo Table, string Parameters, string ObjectName, ICollection<string> FindByColumns)
        {
            string StoredProcedure = $@"Create Procedure  [dbo].[SP_Find{ObjectName}By{string.Join("And", FindByColumns)}]
({Parameters}) AS
BEGIN
	SELECT * FROM [{Table.TableName}] Where {string.Join("AND ", FindByColumns.Select(item => $"{item} = @{item} "))}
END";
            return clsExecuteStoredProcedureData.AddStoredProcedure(Table.ConnectionString, StoredProcedure);
        }


         private string GenerateDataAccessLayerFindCode(ITableInfo Table, string Parameters, string ObjectName, ICollection<string> FindByColumns)
        {
            string Code = $@"static public bool Find({Parameters})
        {{
            bool IsFound = false;
            try
            {{
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {{
                    using (SqlCommand Command = new SqlCommand(""SP_Find{ObjectName}By{string.Join("And", FindByColumns)}"", Connection))
                    {{
                        Command.CommandType = CommandType.StoredProcedure;
                        ";
             foreach(var item in FindByColumns)
            {
                Code += $"\nCommand.Parameters.AddWithValue(\"@{item}\" , {item});";
            }
            Code += $@"
                        Connection.Open();
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {{
                            if (Reader.Read())
                            {{
                                IsFound = true;
                                ";
            foreach (var kvp in Table.Columns.Where(kvp => !FindByColumns.Contains(kvp.Key)))
            {
                if(Table.NullableColumns.Contains(kvp.Key))
                {
                    Code += $"\nif(Reader[\"{kvp.Key}\"] == DBNull.Value)";
                    Code += $"\n{kvp.Key} = null;";
                    Code += $"\nelse";
                }
                Code += $"\n{kvp.Key} = ({kvp.Value.DataType})Reader[\"{kvp.Key}\"];";
            }

                                Code+=$@"
                            }}
                        }}
                    }}
                }}
            }}
            catch (Exception ex)
            {{
                OnErrorOccur?.Invoke(ex);
            }}
            return IsFound;
        }}";
            return Code;

        }




         public string GenerateCode(ITableInfo Table, string ObjectName)
        {
            string Code = "";
            Dictionary<string, HashSet<string>> columns = new Dictionary<string, HashSet<string>>();
            columns.Add("PrimaryKey", Table.PrimaryKeys);
            foreach (var kvp in Table.UniqueColumns)
            {
                columns.Add(kvp.Key, kvp.Value);
            }

            foreach (var kvp in columns)
            {
                if(GenerateStoredProcedure(Table, GenerateSqlParameters(Table, kvp.Value), ObjectName, kvp.Value))
                    Code += "\n"+GenerateDataAccessLayerFindCode(Table, GenerateCParameters(Table, kvp.Value), ObjectName, kvp.Value);
            }
            return Code;

        }
    }
}
