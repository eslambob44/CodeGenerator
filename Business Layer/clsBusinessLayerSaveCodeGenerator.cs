using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    internal class clsBusinessLayerSaveCodeGenerator : IBusinessLayerCodeGenerator
    {
        string Code;
        public clsBusinessLayerSaveCodeGenerator(string Code)
        {
            this.Code = Code;
        }

        public enum enMode { AddNew, Update };
        public enMode Mode { get; }

        bool CheckForExistance(enMode enMode) 
        {
            return Code.IndexOf($"_{enMode.ToString()}()") != -1;
        }

        string GenerateFunction()
        {
            bool IsAddNew = CheckForExistance(enMode.AddNew);
            bool IsUpdate = CheckForExistance(enMode.Update);
            if (IsAddNew || IsUpdate)
            {
                string Code = $@"public bool Save()
        {{
            switch(_Mode)
            {{
                ";
                if (IsAddNew)
                    Code += $@"case enMode.AddNew:
                    if (_AddNew())
                    {{
                        _Mode = enMode.Update;
                        return true;
                    }}
                    else return false;";
                if (IsUpdate)
                    Code +=
                $@"
                case enMode.Update:
                    return _Update();
                ";

                Code += $@"
                default: return false;
            }}
        }}";
                return Code;
            }
            return null;
        }

        public string GenerateCode(ITableInfo Table, string ObjectName)
        {
            return GenerateFunction();
        }
    }



     
}
