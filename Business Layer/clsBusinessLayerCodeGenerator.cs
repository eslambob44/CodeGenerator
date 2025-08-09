using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class clsBusinessLayerCodeGenerator : ICodeGenerator
    {

        private IBusinessLayerCodeGenerator[] CodeGenerators;
        public clsBusinessLayerCodeGenerator(IBusinessLayerCodeGenerator[] codeGenerators)
        {
            this.CodeGenerators = codeGenerators;
        }

        public string GenerateCode(ITableInfo TableInfo, string FolderLocation, string ObjectName)
        {
            StringBuilder Code = new StringBuilder();
            Code.AppendLine($@" public class cls{ObjectName}
            {{");
            IBusinessLayerCodeGenerator PropertiesCodeGenerator = new clsBusinessLayerPropertiesCodeGenerator();
            Code.AppendLine(PropertiesCodeGenerator.GenerateCode(TableInfo, ObjectName));

            IBusinessLayerCodeGenerator DTOCodeGenerator = new clsBusinessLayerDTOCodeGenerator();
            Code.AppendLine(DTOCodeGenerator.GenerateCode(TableInfo, ObjectName));

            
            foreach (var codeGenerator in CodeGenerators)
            {
                Code.AppendLine(codeGenerator.GenerateCode(TableInfo, ObjectName));
            }

            clsBusinessLayerSaveCodeGenerator SavecodeGenerator = new clsBusinessLayerSaveCodeGenerator(Code.ToString());
            Code.AppendLine(SavecodeGenerator.GenerateCode(TableInfo, ObjectName));
            Code.AppendLine("}");
            if(!Directory.Exists(FolderLocation))
                FolderLocation = "D:\\";

            Utility.clsIO.WriteToFile(FolderLocation, $"cls{ObjectName}.cs", Code.ToString());
            return FolderLocation + $"cls{ObjectName}.cs";
        }
    }
}

public interface IBusinessLayerCodeGenerator
{
    string GenerateCode(ITableInfo Table, string ObjectName);
}
