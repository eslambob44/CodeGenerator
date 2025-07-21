using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class clsBusinessLayerFindCodeGenerator : IBusinessLayerCodeGenerator
    {
        private string GenerateParameters(ITableInfo Table , HashSet<string> FindByParameters)
        {
            string Parameters = string.Join("," , Table.Columns.Where(kvp =>  FindByParameters.Contains(kvp.Key)).Select(kvp => $"{kvp.Value.DataType} {kvp.Key}"));
            return Parameters;
        }

        private string GenerateFunction(ITableInfo Table , string ObjectName ,  HashSet<string> FindByParameters)
        {
            string Code = $@"static public cls{ObjectName} Find({GenerateParameters(Table, FindByParameters)})
        {{
            ";
            foreach (var kvp in Table.Columns.Where(kvp=> !FindByParameters.Contains(kvp.Key) ))
            {
                Code += $"\n{kvp.Value.DataType} {kvp.Key} = default({kvp.Value.DataType});";

            }
            Code +=
            $@"
            if (cls{ObjectName}Data.Find({string.Join("," , FindByParameters)},{string.Join("," , Table.Columns.Where(kvp => !FindByParameters.Contains(kvp.Key)).Select(kvp=> $"ref {kvp.Key}"))}))
            {{
                return new cls{ObjectName}({string.Join("," , Table.Columns.Select(kvp => kvp.Key))});
            }}
            else return null;
        }}
        ";

            return Code;
        }

        private string GenerateConstructor(ITableInfo Table , string ObjectName)
        {
            string Code = $@"private cls{ObjectName}({string.Join("," , Table.Columns.Select(kvp => $"{kvp.Value.DataType} {kvp.Key}"))})
        {{
            _Mode = enMode.Update;
            {string.Join("\n" , Table.Columns.Select(kvp=>$"this.{kvp.Key} = {kvp.Key};"))}

        }}";
            return Code;
        }

        public string GenerateCode(ITableInfo Table, string ObjectName)
        {
            Dictionary<string , HashSet<string>> FindParameters = new Dictionary<string , HashSet<string>>();
            FindParameters.Add("PrimaryKey", Table.PrimaryKeys);
            foreach (var kvp in Table.UniqueColumns)
            {
                FindParameters.Add(kvp.Key , kvp.Value);
            }

            StringBuilder Code = new StringBuilder( "\n");
            foreach (var item in FindParameters)
            {
                Code .AppendLine( GenerateFunction(Table, ObjectName, item.Value));
            }
            if (!string.IsNullOrEmpty(Code.ToString())) 
            {
                Code .AppendLine(GenerateConstructor(Table, ObjectName));
            }
            return Code.ToString();


        }
    }
}
