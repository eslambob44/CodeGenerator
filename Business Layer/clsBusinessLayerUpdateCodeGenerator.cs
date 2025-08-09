using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class clsBusinessLayerUpdateCodeGenerator : IBusinessLayerCodeGenerator
    {
        private string GenerateFunction(ITableInfo Table , string ObjectName)
        {
            string Code = $@"private bool _Update()
{{
    return cls{ObjectName}Data.Update(DTO);
}}";
            return Code;
        }



        public string GenerateCode(ITableInfo Table, string ObjectName)
        {

            return GenerateFunction(Table , ObjectName);

        }
    }
}
