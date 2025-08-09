using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    internal class clsBusinessLayerDTOCodeGenerator : IBusinessLayerCodeGenerator
    {
        public string GenerateCode(ITableInfo Table, string ObjectName)
        {
            string Code = $@"public cls{ObjectName}Data.{ObjectName}DTO DTO  
                            {{
                                get
                                    {{
                                        cls{ObjectName}Data.{ObjectName}DTO DTO = new cls{ObjectName}Data.{ObjectName}DTO();
                                        {string.Join("\n", Table.Columns.Select(kvp => $"DTO.{kvp.Key} = this.{kvp.Key};"))}
                                        return DTO;
                                    }}
                            }}
";
            Code += $@"/// <summary>
        /// Copy data from DTO except {string.Join(" And ",Table.PrimaryKeys)}
        /// </summary>
        /// <param name=""DTO""></param>
                public void CopyFromDTO({ObjectName}DTO DTO)
                        {{
                            {string.Join("\n", Table.Columns.Where(kvp => !Table.PrimaryKeys.Contains(kvp.Key))
                            .Select(kvp => $"this.{kvp.Key} = DTO.{kvp.Key};"))}
                        }}";
            return Code;
        }
    }
}
