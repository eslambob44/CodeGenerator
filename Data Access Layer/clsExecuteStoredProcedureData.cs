using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    static public class clsExecuteStoredProcedureData
    {
        static public bool AddStoredProcedure(string ConnectionString , string StoredProcedure)
        {
            try
            {
                using(SqlConnection Connection = new SqlConnection(ConnectionString))
                {
                    string Query = StoredProcedure;
                    using(SqlCommand command = new SqlCommand(Query,Connection))
                    {
                        Connection.Open();
                        command.ExecuteNonQuery();
                    }
                    return true;
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2714)
                    return true;
                else return false;
            }

            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
