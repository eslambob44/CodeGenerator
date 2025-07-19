using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    static internal class clsDataAccessLayerListCodeGenerator
    {
        static private bool GenerateStoredProcedure(ITableInfo Table)
        {
            string StoredProcudure = $@"Create Procedure [dbo].[SP_List{Table.TableName}]
AS
BEGIN
	SELECT * From [{Table.TableName}]
END";
            return clsExecuteStoredProcedureData.AddStoredProcedure(Table.ConnectionString, StoredProcudure);
        }


        enum enListMode { Syncronons , Asyncronons}
        static private string GenerateDataAccessLayerListCode(ITableInfo Table , enListMode ListMode)
        {
            string Code = $@"{((ListMode == enListMode.Asyncronons) ? "async " : "")}static public {((ListMode == enListMode.Asyncronons)?"Task<":"")}DataTable{((ListMode == enListMode.Asyncronons) ? ">" : "")} List{((ListMode == enListMode.Asyncronons) ? "Async" : "")}()
        {{
            DataTable dt = new DataTable();
            try
            {{
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {{
                    using (SqlCommand Command = new SqlCommand(""SP_List{Table.TableName}"", Connection))
                    {{
                        Command.CommandType = CommandType.StoredProcedure;
                        Connection.Open();
                        using (SqlDataReader Reader = {((ListMode == enListMode.Asyncronons) ? "await" : "")}Command.ExecuteReader{((ListMode == enListMode.Asyncronons) ? "Async" : "")}())
                        {{
                            if (Reader.HasRows)
                            {{
                                dt.Load(Reader);
                            }}
                        }}
                    }}
                }}
            }}
            catch (Exception ex)
            {{
                OnErrorOccur?.Invoke(ex);
            }}
            return dt;
        }}";
            return Code;
        }

        static public string GenerateCode(ITableInfo tableInfo)
        {
            if (GenerateStoredProcedure(tableInfo))
            {
                string Code = GenerateDataAccessLayerListCode(tableInfo,enListMode.Syncronons);
                Code += "\n" + GenerateDataAccessLayerListCode(tableInfo, enListMode.Asyncronons);
                return Code;
            }
            else return null;
        }
    }
}
