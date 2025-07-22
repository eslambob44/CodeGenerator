using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Business_Layer.clsTable;

namespace Business_Layer
{
    public class clsTable: ITableInfo
    {
        public string ConnectionString { get; private set; }
        public string TableName { get; private set; }

        clsTableData _data;

        public struct stDataType
        {
            public string SqlDataType { get; set; }
            public string DataType { get; set; }
        }

        public string MapSqlDataTypeToCDataType(string sqlDataType, bool IsNullable)
        {
            sqlDataType = sqlDataType.ToLower();
            string cDataType = "";
            switch (sqlDataType)
            {
                // Reference types (return immediately)
                case "nvarchar(max)":
                case "varchar(max)":
                case "char":
                case "nchar":
                case "text":
                case "ntext":
                case "xml":
                    return "string";

                case "varbinary":
                case "binary":
                case "image":
                case "timestamp":
                    return "byte[]";

                case "uniqueidentifier":
                    return "Guid";

                // Value types (assign to cDataType)
                case "bigint":
                    cDataType = "long";
                    break;

                case "bit":
                    cDataType = "bool";
                    break;

                case "date":
                case "datetime":
                case "datetime2":
                case "smalldatetime":
                    cDataType = "DateTime";
                    break;

                case "datetimeoffset":
                    cDataType = "DateTimeOffset";
                    break;

                case "time":
                    cDataType = "TimeSpan";
                    break;

                case "tinyint":
                    cDataType = "byte";
                    break;

                case "smallint":
                    cDataType = "short";
                    break;

                case "int":
                    cDataType = "int";
                    break;

                case "decimal":
                case "numeric":
                case "money":
                case "smallmoney":
                    cDataType = "decimal";
                    break;

                case "float":
                    cDataType = "double";
                    break;

                case "real":
                    cDataType = "float";
                    break;

                case "sql_variant":
                    cDataType = "object";
                    break;

                default:
                    throw new ArgumentException($"Unsupported SQL type: {sqlDataType}");
            }
            cDataType += (IsNullable) ? "?" : "";
            return cDataType;
        }

        public Dictionary<string, stDataType> Columns { get; private set; } = new Dictionary<string, stDataType>();
        public HashSet<string> NullableColumns { get; private set; } = new HashSet<string>();
        public HashSet<string> PrimaryKeys { get; private set; } = new HashSet<string>();
        public bool IsPrimaryKeyIdentity { get; private set; }
        public Dictionary<string, HashSet<string>> UniqueColumns { get; private set; } = new Dictionary<string, HashSet<string>>();
        public Dictionary<string, Dictionary<string, string>> ForeignKeys { get; private set; } = new Dictionary<string, Dictionary<string, string>>();
        private void _LoadColumns()
        {
            Parallel.Invoke(_LoadAllColumnsAndNullableColumns, _LoadPrimaryKeyColumns,
                _LoadUniqueColumns, _LoadForeignKeyColumns);

            IsPrimaryKeyIdentity = _IsPrimaryKeyIdentity();
        }



        private bool _IsPrimaryKeyIdentity()
        {
            return _data.IsPrimaryKeyIdentity();
        }




        private void _LoadAllColumnsAndNullableColumns()
        {
            DataTable dtColumns = new DataTable();
            dtColumns = _data.GetColumns();

            foreach (DataRow row in dtColumns.Rows)
            {
                bool IsNullable = row["IS_NULLABLE"].ToString() == "YES";
                stDataType type = new stDataType();
                type.SqlDataType = row["DATA_TYPE"].ToString();
                if (type.SqlDataType == "nvarchar" || type.SqlDataType == "varchar")
                    type.SqlDataType += "(max)";
                type.DataType = MapSqlDataTypeToCDataType(type.SqlDataType, IsNullable);
                if (IsNullable)
                    NullableColumns.Add(row["COLUMN_NAME"].ToString());

                Columns.Add(row["COLUMN_NAME"].ToString(), type);
            }
        }
        private void _LoadPrimaryKeyColumns()
        {
            DataTable dtPrimaryKeys = _data.GetPrimaryKeyColumns();

            foreach (DataRow row in dtPrimaryKeys.Rows)
            {
                PrimaryKeys.Add(row["COLUMN_NAME"].ToString());
            }
        }
        private void _LoadUniqueColumns()
        {
            DataTable dtUniqueConstraint = _data.GetUniqueColumns();
            foreach (DataRow row in dtUniqueConstraint.Rows)
            {
                if (!UniqueColumns.ContainsKey(row["ConstraintName"].ToString()))
                {
                    UniqueColumns[row["ConstraintName"].ToString()] = new HashSet<string>();

                }

                UniqueColumns[row["ConstraintName"].ToString()].Add(row["ColumnName"].ToString());

            }
        }
        private void _LoadForeignKeyColumns()
        {
            DataTable dtForeignKeys = _data.GetForeignKeysColumns();
            foreach (DataRow row in dtForeignKeys.Rows)
            {
                if (!ForeignKeys.ContainsKey(row["ReferencedTable"].ToString()))
                {
                    ForeignKeys[row["ReferencedTable"].ToString()] = new Dictionary<string, string>();
                }

                ForeignKeys[row["ReferencedTable"].ToString()].Add(row["ParentColumn"].ToString(), row["ReferencedColumn"].ToString());
            }
        }

        private clsTable(string ConnectionString, string TableName, clsTableData data)
        {
            this.ConnectionString = ConnectionString;
            this.TableName = TableName;
            this._data = data;
            _LoadColumns();
        }

        internal static clsTable GetObject(string ConnectionString, string TableName)
        {
            clsTableData data = clsTableData.GetObject(ConnectionString, TableName);
            if (data != null)
                return new clsTable(ConnectionString, TableName, data);
            else return null;
        }

        public string GenerateCode(ICodeGenerator[] CodeGenerators , string ObjectName , string FolderLocation)
        {


            bool IsExcuted = true;
            foreach(var codeGenerator in CodeGenerators)
            {
                IsExcuted &= codeGenerator?.GenerateCode(this,FolderLocation,ObjectName)!= null;    
            }
            if (IsExcuted) return FolderLocation;
            else return null;
        }
        

    }
}

public interface ITableInfo
{
     string TableName { get; }
     string ConnectionString { get; }
     Dictionary<string, stDataType> Columns { get; }
     HashSet<string> NullableColumns { get; }
     HashSet<string> PrimaryKeys { get; }
     bool IsPrimaryKeyIdentity { get; }
     Dictionary<string, HashSet<string>> UniqueColumns { get; }
     Dictionary<string, Dictionary<string, string>> ForeignKeys { get; }
}

public interface ICodeGenerator
{

    string GenerateCode(ITableInfo TableInfo ,string FolderLocation, string ObjectName);
}

public class clsCodeGeneratorMock:ICodeGenerator
{
    
    public string GenerateCode(ITableInfo TableInfo,string FolderLocation, string ObjectName)
    {
        return null;
    }
}