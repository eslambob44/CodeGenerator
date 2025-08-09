using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
     public class clsDataAccessLayerDTOCodeGenerator : IDataAccessLayerCodeGenerator
    {
        public string GenerateCode(ITableInfo Table, string ObjectName)
        {
            string Code = $@"public class {ObjectName}DTO{{";
            Code += string.Join("\n", Table.Columns.Select(
                kvp => $"public {kvp.Value.DataType} {kvp.Key} {{get;set;}}"));
            Code += "\n}";
            return Code;
        }
    }
}
