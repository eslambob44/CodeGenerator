using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class clsDataAccessLayerUpdateCodeGenerator: IDataAccessLayerCodeGenerator
    {

         private string GenerateCParameters(ITableInfo Table)
        {
            string Parameter = "";
            Parameter = string.Join(",", Table.Columns.Select(kvp => $"{kvp.Value.DataType} {kvp.Key}"));
            return Parameter;

        }

         private string GenerateSqlParameter(ITableInfo Table)
        {
            string Parameter = "";
            Parameter = string.Join(",", Table.Columns.Select(kvp => $"@{kvp.Key} {kvp.Value.SqlDataType}"));
            return Parameter;
        }

         private bool GenerateStoredProcedure(ITableInfo Table, string Parameters, string ObjectName)
        {
            string StoredProcedure = $@"Create procedure [dbo].[SP_Update{ObjectName}] ({Parameters})

AS

BEGIN 
	
UPDATE [dbo].[{Table.TableName}]
   SET {string.Join(",", Table.Columns.Where(kvp => !Table.PrimaryKeys.Contains(kvp.Key))
   .Select(kvp => $"[{kvp.Key}] = @{kvp.Key}\n"))}
		WHERE {string.Join("AND" , Table.PrimaryKeys.Select(item=>$"[{item}] = @{item}"))};
END ";
            return clsExecuteStoredProcedureData.AddStoredProcedure(Table.ConnectionString, StoredProcedure);
        }

         private string GenerateDataAccessLayerUpdate(ITableInfo Table , string CParameters , string ObjectName)
        {
            string Code = $@"static public bool Update({CParameters})
        {{
            bool IsUpdated = false;
            try
            {{
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {{
                    using (SqlCommand Command = new SqlCommand(""SP_Update{ObjectName}"", Connection))
                    {{
                        Command.CommandType = CommandType.StoredProcedure;
                        ";

            foreach (var kvp in Table.Columns)
            {
                if (Table.NullableColumns.Contains(kvp.Key))
                {
                    Code += $"\n if({kvp.Key}== null)";
                    Code += $"\n Command.Parameters.AddWithValue(\"@{kvp.Key}\" , DBNull.Value);";
                    Code += $"\n else";
                }
                Code += $"\n Command.Parameters.AddWithValue(\"@{kvp.Key}\" , {kvp.Key});";

            }
            Code +=


            $@"Connection.Open();
                        IsUpdated = Command.ExecuteNonQuery() > 0;

                    }}
                }}
            }}
            catch (Exception ex)
            {{
                OnErrorOccur?.Invoke(ex);
            }}

            return IsUpdated;

        }}";
            return Code;
        }

         public string GenerateCode(ITableInfo tableInfo , string ObjectName)
        {
            GenerateStoredProcedure(tableInfo, GenerateSqlParameter(tableInfo), ObjectName);
            return GenerateDataAccessLayerUpdate(tableInfo,GenerateCParameters(tableInfo),ObjectName);
        }

    }
}
