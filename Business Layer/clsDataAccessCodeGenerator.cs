using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class clsDataAccessCodeGenerator : ICodeGenerator
    {
        private ITableInfo Table;
        private string TableName;
         public clsDataAccessCodeGenerator (ITableInfo Table)
        {
            this.Table = Table;
        }

        public string GenerateCode (string FolderLocation , string ObjectName )
        {
            clsDataAccessLayerInsertCodeGenerator.GenerateCode(Table, ObjectName);
            return null;
        }


    }
}
