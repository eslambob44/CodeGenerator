using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class clsBusinessLayerListCodeGenerator : IBusinessLayerCodeGenerator
    {
        

        private string GenerateFunction(ITableInfo Table, string ObjectName)
        {
            string Code = $@"static public List<cls{ObjectName}Data.{ObjectName}DTO> List()
        {{
            return cls{ObjectName}Data.List();
        }}

        async static public Task<List<cls{ObjectName}Data.{ObjectName}DTO>> ListAsync()
        {{
            return await cls{ObjectName}Data.ListAsync();
        }}";
            return Code;
        }



        public string GenerateCode(ITableInfo Table, string ObjectName)
        {

            return GenerateFunction(Table, ObjectName);

        }
    }
}
