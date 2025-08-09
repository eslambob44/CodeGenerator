using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    internal class clsBusinessLayerPropertiesCodeGenerator : IBusinessLayerCodeGenerator
    {
        public string GenerateCode(ITableInfo Table, string ObjectName)
        {
            StringBuilder Code =new StringBuilder($@"
     enum enMode{{ AddNew , Update}};
    private enMode _Mode;
    ");
            foreach (var kvp in Table.Columns.Where(kvp => Table.PrimaryKeys.Contains(kvp.Key)))
            {
                Code.AppendLine($"public {kvp.Value.DataType} {kvp.Key} {{get;{((Table.IsPrimaryKeyIdentity) ? "private" : "")} set;}}");
            }
            foreach (var kvp in Table.Columns.Where(kvp => !Table.PrimaryKeys.Contains(kvp.Key)))
            {
                Code.AppendLine($"public {kvp.Value.DataType} {kvp.Key} {{get;  set;}}");
            }

            return Code.ToString();

        }
    }
}
