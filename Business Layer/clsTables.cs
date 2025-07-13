using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class clsTables
    {

        public string ConnectionString {  get; private set; }
        private clsTablesData data;

        private clsTables(string connectionString , clsTablesData data) 
        {
            ConnectionString = connectionString;
            this.data = data;
        }

        static public clsTables GetObject(string UserName , string Password , 
            string DataBaseName , string Server = ".")
        {
            return GetObject(GenerateConnectionString(UserName, Password, DataBaseName, Server));   
        }

        static private string GenerateConnectionString(string UserName, string Password,
            string DataBaseName, string Server)
        {
            return $"Server ={Server};Database = {DataBaseName};User Id = {UserName} ; Password = {Password}";
        }

        static public clsTables GetObject(string connectionString)
        {
            clsTablesData data = clsTablesData.GetObject(connectionString);
            if (data != null)
            {
                return new clsTables(connectionString, data);
            }
            else return null;
        }


        public DataTable ListTables()
        {
            return data.ListTablesInDataBase();
        }

    }
}
