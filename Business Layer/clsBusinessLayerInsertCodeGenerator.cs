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
    {string .Join("," , Table.Columns.Where(kvp => Table.PrimaryKeys.Contains(kvp.Key)).Select(kvp=>$"{kvp.Value.DataType}? {kvp.Key}"))} = cls{ObjectName}Data.Insert(this.DTO);
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
    return cls{ObjectName}Data.Insert(this.DTO);
}}";
            return Code;
        }

        private string GenerateConstructor(ITableInfo Table , string ObjectName)
        {
            string Code = $@"public cls{ObjectName}()
        {{
            _Mode = enMode.AddNew;
        }}";
            return Code;
        }



        public string GenerateCode(ITableInfo Table, string ObjectName)
        {
            string Code = "";
            if(Table.IsPrimaryKeyIdentity)
                Code =  GenerateIdentityFunction(Table, ObjectName);
            else Code =  GenerateNotIdentityFunction(Table, ObjectName);

            if (!string.IsNullOrEmpty(Code)) Code += GenerateConstructor(Table, ObjectName);
            return Code;

        }
    }
}
