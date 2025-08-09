using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class clsDataAccessLayerListCodeGenerator: IDataAccessLayerCodeGenerator
    {
         private bool GenerateStoredProcedure(ITableInfo Table)
        {
            string StoredProcudure = $@"Create Procedure [dbo].[SP_List{Table.TableName}]
AS
BEGIN
	SELECT * From [{Table.TableName}]
END";
            return clsExecuteStoredProcedureData.AddStoredProcedure(Table.ConnectionString, StoredProcudure);
        }


        enum enListMode { Syncronons , Asyncronons}
         private string GenerateDataAccessLayerListCode(ITableInfo Table, string ObjectName, enListMode ListMode)
        {
            string Code = $@"{((ListMode == enListMode.Asyncronons) ? "async " : "")}static public {((ListMode == enListMode.Asyncronons) ? "Task<" : "")}List<{ObjectName}DTO>{((ListMode == enListMode.Asyncronons) ? ">" : "")} List{((ListMode == enListMode.Asyncronons) ? "Async" : "")}()
        {{
            List<{ObjectName}DTO> liDTO = new List<{ObjectName}DTO>();
            try
            {{
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {{
                    using (SqlCommand Command = new SqlCommand(""SP_List{Table.TableName}"", Connection))
                    {{
                        Command.CommandType = CommandType.StoredProcedure;
                        Connection.Open();
                        using (SqlDataReader Reader = {((ListMode == enListMode.Asyncronons) ? "await " : "")}Command.ExecuteReader{((ListMode == enListMode.Asyncronons) ? "Async" : "")}())
                        {{
                            while(Reader.Read())
                            {{
                                {ObjectName}DTO DTO = new {ObjectName}DTO();
                                ";
            foreach (var kvp in Table.Columns)
            {
                if (Table.NullableColumns.Contains(kvp.Key))
                {
                    Code += $"\nif(Reader[\"{kvp.Key}\"] == DBNull.Value)";
                    Code += $"\nDTO.{kvp.Key} = null;";
                    Code += $"\nelse";
                }
                Code += $"\nDTO.{kvp.Key} = ({kvp.Value.DataType})Reader[\"{kvp.Key}\"];";
            }

            Code +=$@"      liDTO.Add(DTO);
                            }}
                        }}
                    }}
                }}
            }}
            catch (Exception ex)
            {{
                OnErrorOccur?.Invoke(ex);
            }}
            return liDTO;
        }}";
            return Code;
        }

         public string GenerateCode(ITableInfo tableInfo , string ObjectName)
        {
            if (GenerateStoredProcedure(tableInfo))
            {
                string Code = GenerateDataAccessLayerListCode(tableInfo,ObjectName, enListMode.Syncronons);
                Code += "\n" + GenerateDataAccessLayerListCode(tableInfo,ObjectName, enListMode.Asyncronons);
                return Code;
            }
            return null;

        }
    }
}
