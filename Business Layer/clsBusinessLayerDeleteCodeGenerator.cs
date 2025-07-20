using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business_Layer
{
    public class clsBusinessLayerDeleteCodeGenerator : IBusinessLayerCodeGenerator
    {
        private string GenerateParameters(ITableInfo Table, HashSet<string> DeleteByParameters)
        {
            string Parameters = string.Join(",", Table.Columns.Where(kvp => DeleteByParameters.Contains(kvp.Key)).Select(kvp => $"{kvp.Value.DataType} {kvp.Key}"));
            return Parameters;
        }

        private string GenerateFunction(ITableInfo Table, string ObjectName, HashSet<string> DeleteByParameters)
        {
            string Code = $@"static public bool Delete({GenerateParameters(Table, DeleteByParameters)})
        {{
            return cls{ObjectName}Data.Delete({string.Join(",", DeleteByParameters)});
        }}";
            return Code;
        }

       

        public string GenerateCode(ITableInfo Table, string ObjectName)
        {
            Dictionary<string, HashSet<string>> FindParameters = new Dictionary<string, HashSet<string>>();
            FindParameters.Add("PrimaryKey", Table.PrimaryKeys);
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
