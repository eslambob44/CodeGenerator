using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class clsDataAccessCodeGenerator : ICodeGenerator
    {

        private IDataAccessLayerCodeGenerator[] CodeGenerators;
         public clsDataAccessCodeGenerator ( IDataAccessLayerCodeGenerator[] codeGenerators)
        {
            this.CodeGenerators = codeGenerators;
        }

        public string GenerateCode (ITableInfo TableInfo,string FolderLocation , string ObjectName )
        {
            if (!Directory.Exists(FolderLocation)) FolderLocation = "D:\\";

            StringBuilder GeneratedCode = new StringBuilder("");
            foreach (var item in CodeGenerators) 
            {
                GeneratedCode.AppendLine(item.GenerateCode(TableInfo, ObjectName)) ;
            }

            StringBuilder Code= new StringBuilder($@"
    static public class cls{ObjectName}Data
    {{
        static public event Action<Exception> OnErrorOccur;"+"\n") ;
            Code.AppendLine(GeneratedCode.ToString());
            Code.AppendLine ("\t}");

            Utility.clsIO.WriteToFile(FolderLocation, $"cls{ObjectName}Data.cs", Code.ToString());
            
            
            return FolderLocation+"\\"+$"{ObjectName}Data.cs";
        }


    }
}

public interface IDataAccessLayerCodeGenerator
{
    string GenerateCode(ITableInfo Table, string ObjectName);
}
