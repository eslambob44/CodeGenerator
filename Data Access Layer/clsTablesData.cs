using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class clsTablesData

       
    {


        private event Action<Exception> onErrorOccur;
        public string ConnectionString { get; private set; }

        private clsTablesData(string connectionString)
        {
            this.ConnectionString = connectionString;
            onErrorOccur += clsDataAccessLayerSettings.DealingWithOnErrorOccurEvent;
        }


        public static clsTablesData GetObject(string connectionString)
        {
            if (CheckForValidityOfConnectionString(connectionString))
                return new clsTablesData(connectionString);
            else return null;
        }

        static public bool CheckForValidityOfConnectionString(string connectionString)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(connectionString))
                {
                    Connection.Open();

                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public DataTable ListTablesInDataBase()
        {
            DataTable dtTables = new DataTable();
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConnectionString))
                {
                    string Query = @"SELECT INFORMATION_SCHEMA.TABLES.TABLE_NAME 
                                    FROM INFORMATION_SCHEMA.TABLES
                                    WHERE TABLE_TYPE = 'BASE TABLE' 
                                    AND INFORMATION_SCHEMA.TABLES.TABLE_CATALOG=DB_NAME()";
                    using(SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        Connection.Open ();
                        using(SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if(Reader.HasRows)
                                dtTables.Load(Reader);
                        }
                    }
                }
            }catch(Exception e)
            {
                onErrorOccur?.Invoke(e);
            }
            return dtTables;


        }
    }
}
