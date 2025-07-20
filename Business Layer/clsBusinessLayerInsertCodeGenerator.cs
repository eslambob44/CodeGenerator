using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    public class clsBusinessLayerInsertCodeGenerator : IBusinessLayerCodeGenerator
    {
        private string GenerateIdentityFunction(ITableInfo Table, string ObjectName)
        {
            string Code = $@"private bool _AddNew()
{{
    {string .Join("," , Table.Columns.Where(kvp => Table.PrimaryKeys.Contains(kvp.Key)).Select(kvp=>$"{kvp.Value.DataType}? {kvp.Key}"))} = cls{ObjectName}Data.Insert({string.Join("," , Table.Columns.Keys)});
    if ({Table.PrimaryKeys.First()} != null)
    {{
        this.{Table.PrimaryKeys.First()} = {Table.PrimaryKeys.First()}.Value;
        return true;
    }}
    else return false;
}}";
            return Code;
        }


        private string GenerateNotIdentityFunction(ITableInfo tableInfo , string ObjectName)
        {
            string Code = $@"private bool _AddNew()
{{
    return cls{ObjectName}Data.Insert({string.Join(",", tableInfo.Columns.Keys)});
}}";
            return Code;
        }



        public string GenerateCode(ITableInfo Table, string ObjectName)
        {

            if(Table.IsPrimaryKeyIdentity)
                return GenerateIdentityFunction(Table, ObjectName);
            else return GenerateNotIdentityFunction(Table, ObjectName);

        }
    }
}
