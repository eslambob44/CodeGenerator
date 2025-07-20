using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class clsBusinessLayerListCodeGenerator : IBusinessLayerCodeGenerator
    {
        

        private string GenerateFunction( string ObjectName)
        {
            string Code = $@"static public DataTable List()
        {{
            return cls{ObjectName}Data.List();
        }}

        async static public Task<DataTable> ListAsync()
        {{
            return await cls{ObjectName}Data.ListAsync();
        }}";
            return Code;
        }



        public string GenerateCode(ITableInfo Table, string ObjectName)
        {

            return GenerateFunction(ObjectName);

        }
    }
}
