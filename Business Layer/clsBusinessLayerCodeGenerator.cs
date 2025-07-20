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
{{
    enum enMode{{ AddNew , Update}};
    private enMode _Mode;
");
            foreach(var kvp in TableInfo.Columns.Where(kvp => TableInfo.PrimaryKeys.Contains(kvp.Key)))
            {
                Code.AppendLine($"public {kvp.Value.DataType} {kvp.Key} {{get; private set;}}");
            }
            foreach(var kvp in TableInfo.Columns.Where(kvp => !TableInfo.PrimaryKeys.Contains(kvp.Key)))
            {
                Code.AppendLine($"public {kvp.Value.DataType} {kvp.Key} {{get;  set;}}");
            }

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
