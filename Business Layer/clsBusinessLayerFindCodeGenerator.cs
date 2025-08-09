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
            cls{ObjectName}Data.{ObjectName}DTO DTO = cls{ObjectName}Data.Find({string.Join(",", FindByParameters)});
            ";
            
            Code +=
            $@"
            if (DTO != null)
            {{
                return new cls{ObjectName}(DTO);
            }}
            else return null;
        }}
        ";

            return Code;
        }

        private string GenerateConstructor(ITableInfo Table , string ObjectName)
        {
            string Code = $@"private cls{ObjectName}({ObjectName}DTO DTO)
        {{
            _Mode = enMode.Update;
            {string.Join("\n" , Table.Columns.Select(kvp=>$"this.{kvp.Key} = DTO.{kvp.Key};"))}

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
