using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class clsDataAccessLayerDeleteCodeGenerator : IDataAccessLayerCodeGenerator
    {
         private string GenerateSqlParameters(ITableInfo Table , ICollection<string> Columns)
        {
            return string.Join("," , Table.Columns.
                Where(kvp => Columns.Contains(kvp.Key)).
                Select(kvp=> $"@{kvp.Key} {kvp.Value.SqlDataType}"));
        }

         private string GenerateCParameters(ITableInfo Table, ICollection<string> Columns)
        {
            return string.Join(",", Table.Columns.
                Where(kvp => Columns.Contains(kvp.Key)).
                Select(kvp => $"{kvp.Value.DataType} {kvp.Key} "));
        }

         private bool GenerateStoredProcedure(ITableInfo Table  , string Parameters, string ObjectName , ICollection<string> DeleteByColumns)
        {
            string StoredProcedure = $@"Create Procedure  [dbo].[SP_Delete{ObjectName}By{string.Join("And", DeleteByColumns)}]
({Parameters}) AS
BEGIN
	Delete From [{Table.TableName}] Where {string.Join("AND ", DeleteByColumns.Select(item => $"{item} = @{item} "))}
END";
            return clsExecuteStoredProcedureData.AddStoredProcedure(Table.ConnectionString, StoredProcedure);
        }


         private string GenerateDataAccessLayerDeleteCode(ITableInfo Table, string Parameters, string ObjectName, ICollection<string> DeleteByColumns)
        {
            string Code = $@"static public bool Delete({Parameters})
        {{
            bool IsDeleted = false;
            try
            {{
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {{
                    using (SqlCommand Command = new SqlCommand(""SP_Delete{ObjectName}By{string.Join("And", DeleteByColumns)}"", Connection))
                    {{
                        Command.CommandType = System.Data.CommandType.StoredProcedure;
                        ";
            foreach(var Column in DeleteByColumns)
            {
                Code += $"\nCommand.Parameters.AddWithValue(@\"{Column}\" , {Column});";
            }

            Code += $@"
                        Connection.Open();
                        IsDeleted = Command.ExecuteNonQuery()>0;
                    }}
                }}
            }}
            catch (Exception e)
            {{
                OnErrorOccur?.Invoke(e);
            }}
            return IsDeleted;
        }}";
            return Code;    

        }




         public string GenerateCode(ITableInfo Table , string ObjectName)
        {
            string Code = "";
            Dictionary<string, HashSet<string>> columns = new Dictionary<string, HashSet<string>>();
            columns.Add("PrimaryKey", Table.PrimaryKeys);
            foreach(var kvp in Table.UniqueColumns)
            {
                columns.Add(kvp.Key , kvp.Value);
            }

            foreach(var kvp in columns)
            {
               if( GenerateStoredProcedure(Table, GenerateSqlParameters(Table, kvp.Value), ObjectName, kvp.Value))
                    Code += GenerateDataAccessLayerDeleteCode(Table, GenerateCParameters(Table, kvp.Value), ObjectName, kvp.Value);
            }
            return Code;

        }


    }
}
