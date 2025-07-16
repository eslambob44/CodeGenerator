using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    static internal class clsDataAccessLayerInsertCodeGenerator
    {
        
        static private string GenerateCParameters(ITableInfo Table)
        {
            string Parameter = "";
            
            if(Table.IsPrimaryKeyIdentity)
            {
                Parameter = string.Join(",", Table.Columns.Where(kvp => !Table.PrimaryKeys.Contains(kvp.Key))
                .Select(kvp => $"{kvp.Value.DataType} {kvp.Key}"));
            }
            else
            {
                Parameter = string.Join(",", Table.Columns.Select(kvp => $"{kvp.Value.DataType} {kvp.Key}"));
            }
            return Parameter;
            
        }

        static private string GenerateSqlParameter(ITableInfo Table)
        {
            string Parameter = "";

            if (Table.IsPrimaryKeyIdentity)
            {
                Parameter = string.Join(",", Table.Columns.Where(kvp => !Table.PrimaryKeys.Contains(kvp.Key))
                .Select(kvp => $"@{kvp.Key} {kvp.Value.SqlDataType}"));
            }
            else
            {
                Parameter = string.Join(",", Table.Columns.Select(kvp => $"@{kvp.Key} {kvp.Value.SqlDataType}"));
            }
            return Parameter;
        }


        static public bool GenerateStoredProcedure(ITableInfo Table , string Parameters , string ObjectName)
        {


            HashSet<string> Columns = Table.Columns.Select(kvp=>kvp.Key).ToHashSet<string>();
            if (Table.IsPrimaryKeyIdentity)
            {
                Columns.RemoveWhere(s => Table.PrimaryKeys.Contains(s));
            }
            string StoredProcedure = $@"Create Procedure [dbo].[SP_AddNew{ObjectName}] ({Parameters})

As
BEGIN
	Insert Into {Table.TableName}({string.Join(",", Columns)})
	Values ({string.Join(",", Columns.Select(s => $"@{s}"))});

	{((Table.IsPrimaryKeyIdentity) ? "SELECT SCOPE_IDENTITY()":"")}
	
END";
            bool IsAdded = clsExecuteStoredProcedureData.AddStoredProcedure(Table.ConnectionString, StoredProcedure);
            return IsAdded;


        }


        static private string GenerateDataAccessInsertIdentity( ITableInfo Table , string CParameters , string ObjectName)
        {
            string PrimaryKeyType = string.Join(",", Table.Columns.Where(kvp => kvp.Key == Table.PrimaryKeys.FirstOrDefault()).Select(kvp => kvp.Value.DataType));
            string Code = $@"static public {PrimaryKeyType}? Insert({CParameters})
        {{
            {PrimaryKeyType}? ID = null;
            try
            {{
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {{
                    using (SqlCommand Command = new SqlCommand(""SP_AddNew{ObjectName}"", Connection))
                    {{
                        Command.CommandType = CommandType.StoredProcedure;
                        ";

            foreach (var kvp in Table.Columns.Where(kvp => !Table.PrimaryKeys.Contains(kvp.Key)))
            {
                if(Table.NullableColumns.Contains(kvp.Key))
                {
                    Code += $"\n if({kvp.Key}== null)";
                    Code += $"\n Command.Parameters.AddWithValue(\"@{kvp.Key}\" , DBNull.Value);";
                    Code += $"\n else";
                }
                Code += $"\n Command.Parameters.AddWithValue(\"@{kvp.Key}\" , {kvp.Key});";
                
            }


            Code += $@"
                        Connection.Open();
                        object Result = Command.ExecuteScalar();
                        if (Result != null && {PrimaryKeyType}.TryParse(Result.ToString(), out {PrimaryKeyType} Temp))
                        {{
                            ID = Temp;
                        }}

                    }}
                }}
            }}
            catch (Exception ex)
            {{
                OnErrorOccur?.Invoke(ex);
            }}
            return ID;
        }}";

            return Code;
        }


        static private string GenerateDataAccessInsert(ITableInfo Table, string CParameters, string ObjectName)
        {
            string Code = $@"static public bool Insert({CParameters})
        {{
            bool IsInserted = false;
            try
            {{
                using (SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {{
                    using (SqlCommand Command = new SqlCommand(""SP_AddNew{ObjectName}"", Connection))
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


            Code += $@"
                        Connection.Open();
                        IsInserted = Command.ExecuteNonQuery() > 0;

                    }}
                }}
            }}
            catch (Exception ex)
            {{
                OnErrorOccur?.Invoke(ex);
            }}
            return IsInserted;
        }}";
            
            

            return Code;
        }

        static public string GenerateCode(ITableInfo Table , string ObjectName)
        {
            string Parameter = GenerateCParameters(Table);
            string sqlParameter = GenerateSqlParameter(Table);
            if(!GenerateStoredProcedure(Table, sqlParameter, ObjectName))
            {
                return null;
            }
            if (Table.IsPrimaryKeyIdentity)
                return GenerateDataAccessInsertIdentity(Table, Parameter, ObjectName);
            else
                return GenerateDataAccessInsert(Table, Parameter, ObjectName);
            
        }
    }
}
