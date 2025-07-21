using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class clsBusinessLayerIsExistsCodeGenerator : IBusinessLayerCodeGenerator
    {


        private string GenerateParameters(ITableInfo Table, HashSet<string> UniqueParamters)
        {
            string Parameters = string.Join(",", Table.Columns.Where(kvp => UniqueParamters.Contains(kvp.Key)).Select(kvp => $"{kvp.Value.DataType} {kvp.Key}"));
            return Parameters;
        }

        private string GenerateFunction(ITableInfo Table, string ObjectName, HashSet<string> UniqueParameters)
        {
            string Code = $@"static public bool Is{string.Join("And" , UniqueParameters)}UsedBefore({GenerateParameters(Table, UniqueParameters)})
        {{
            return return cls{ObjectName}Data.Is{string.Join("And", UniqueParameters)}UsedBefore({string.Join("," , UniqueParameters)});
        }}";
            return Code;
        }



        public string GenerateCode(ITableInfo Table, string ObjectName)
        {
            Dictionary<string, HashSet<string>> FindParameters = new Dictionary<string, HashSet<string>>();
            foreach (var kvp in Table.UniqueColumns)
            {
                FindParameters.Add(kvp.Key, kvp.Value);
            }

            StringBuilder Code = new StringBuilder("\n");
            foreach (var item in FindParameters)
            {
                Code.AppendLine(GenerateFunction(Table, ObjectName, item.Value));
            }

            return Code.ToString();


        }
    }
}
