﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class clsTableData
    {


        private event Action<Exception> onErrorOccur;
        public string ConnectionString { get; private set; }

        public string TableName {  get; private set; }

        private clsTableData(string connectionString , string TableName)
        {
            this.ConnectionString = connectionString;
            onErrorOccur += clsDataAccessLayerSettings.DealingWithOnErrorOccurEvent;
            this.TableName = TableName;
        }


        public static clsTableData GetObject(string connectionString , string TableName)
        {
            if (CheckForValidityOfConnectionStringAndTableName(connectionString,TableName))
                return new clsTableData(connectionString , TableName);
            else return null;
        }

        static public bool CheckForValidityOfConnectionStringAndTableName(string connectionString , string TableName)
        {
            bool IsValid = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection(connectionString))
                {
                    string Query = @"
                                IF OBJECT_ID(@TableName, 'U') IS NOT NULL
                                BEGIN
                                    Select 1;
                                END";
                    using(SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        Command.Parameters.AddWithValue("@TableName", TableName);
                        Connection.Open();
                        object result = Command.ExecuteScalar();
                        IsValid = (result != null && int.TryParse(result.ToString(), out int Temp));
                    }
                    

                }
            }
            catch (Exception e)
            {
                return false;
            }
            return IsValid;
        }

        public DataTable GetColumns()
        {
            DataTable dtColumns  = new DataTable();
            try
            {
                using(SqlConnection Connection = new SqlConnection((string)ConnectionString))
                {
                    string Query = @"SELECT INFORMATION_SCHEMA.TABLES.TABLE_NAME ,COLUMN_NAME,DATA_TYPE , COLUMN_DEFAULT,IS_NULLABLE
                                    FROM INFORMATION_SCHEMA.TABLES
                                    inner join INFORMATION_SCHEMA.COLUMNS on
                                    INFORMATION_SCHEMA.TABLES.TABLE_NAME 
                                    = INFORMATION_SCHEMA.COLUMNS.TABLE_NAME
                                    WHERE TABLE_TYPE = 'BASE TABLE' 
                                    AND INFORMATION_SCHEMA.TABLES.TABLE_CATALOG=DB_NAME() 
                                    and  INFORMATION_SCHEMA.TABLES.TABLE_NAME =  @TableName;
                                    ";
                    using(SqlCommand Command = new SqlCommand(Query , Connection))
                    {
                        Command.Parameters.AddWithValue("@TableName", TableName);
                        Connection.Open();
                        using(SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if(Reader.HasRows)
                            {
                                dtColumns.Load(Reader);
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
                onErrorOccur?.Invoke(e);
            }
            return dtColumns;
        }

        public DataTable GetPrimaryKeyColumns()
        {
            DataTable dtColumns = new DataTable();
            try
            {
                using (SqlConnection Connection = new SqlConnection((string)ConnectionString))
                {
                    string Query = @"EXEC sp_pkeys @TableName";
                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        Command.Parameters.AddWithValue("@TableName", TableName);
                        Connection.Open();
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.HasRows)
                            {
                                dtColumns.Load(Reader);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                onErrorOccur?.Invoke(e);
            }
            return dtColumns;
        }

        public bool IsPrimaryKeyIdentity()
        {
            bool IsIdentity = false;
            try
            {
                using (SqlConnection Connection = new SqlConnection((string)ConnectionString))
                {
                    string Query = @"
                                	SELECT 1

FROM 
    sys.identity_columns
WHERE 
    object_id = OBJECT_ID(@TableName);;

";
                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        Command.Parameters.AddWithValue("@TableName", TableName);
                        Connection.Open();
                        object Result = Command.ExecuteScalar();
                        IsIdentity = (Result != null && int.TryParse(Result.ToString(), out int Temp));
                    }
                }
            }
            catch (Exception e)
            {
                onErrorOccur?.Invoke(e);
            }
            return IsIdentity;
        }

        public DataTable GetUniqueColumns()
        {
            DataTable dtColumns = new DataTable();
            try
            {
                using (SqlConnection Connection = new SqlConnection((string)ConnectionString))
                {
                    string Query = @"SELECT 
                                    OBJECT_NAME(uc.parent_object_id) AS TableName,
                                    uc.name AS ConstraintName,
                                    'UNIQUE CONSTRAINT' AS ConstraintType,
                                    COL_NAME(uc.parent_object_id, c.column_id) AS ColumnName,
                                    c.index_column_id AS KeyOrder
                                FROM 
                                    sys.key_constraints uc
                                JOIN 
                                    sys.index_columns c ON uc.parent_object_id = c.object_id AND uc.unique_index_id = c.index_id
                                WHERE 
                                    uc.type = 'UQ' -- Unique constraints only
                                    AND OBJECT_NAME(uc.parent_object_id) = @TableName
                                
                                UNION
                                
                                SELECT 
                                    OBJECT_NAME(i.object_id) AS table_name,
                                    i.name AS constraint_name,
                                    'UNIQUE INDEX' AS constraint_type,
                                    COL_NAME(i.object_id, ic.column_id) AS column_name,
                                    ic.index_column_id AS ordinal_position
                                FROM 
                                    sys.indexes i
                                JOIN 
                                    sys.index_columns ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id
                                WHERE 
                                    i.is_unique = 1
                                    AND i.is_primary_key = 0
                                    AND NOT EXISTS (
                                        SELECT 1 
                                        FROM sys.key_constraints kc 
                                        WHERE kc.unique_index_id = i.index_id 
                                        AND kc.parent_object_id = i.object_id
                                    )
                                    AND OBJECT_NAME(i.object_id) = @TableName
                                ORDER BY 
                                    ConstraintName, KeyOrder;";
                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        Command.Parameters.AddWithValue("@TableName", TableName);
                        Connection.Open();
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.HasRows)
                            {
                                dtColumns.Load(Reader);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                onErrorOccur?.Invoke(e);
            }
            return dtColumns; 
        }

        public DataTable GetForeignKeysColumns()
        {
            DataTable dtColumns = new DataTable();
            try
            {
                using (SqlConnection Connection = new SqlConnection((string)ConnectionString))
                {
                    string Query = @"SELECT
                                    fk.name AS ForeignKeyName,
                                    parent.name AS ParentTable,
                                    pc.name AS ParentColumn,
                                    referenced.name AS ReferencedTable,
                                    rc.name AS ReferencedColumn
                                
                                FROM 
                                    sys.foreign_keys fk
                                    INNER JOIN sys.foreign_key_columns fkc 
                                        ON fk.object_id = fkc.constraint_object_id
                                    INNER JOIN sys.tables parent 
                                        ON fkc.parent_object_id = parent.object_id
                                    INNER JOIN sys.tables referenced 
                                        ON fkc.referenced_object_id = referenced.object_id
                                    INNER JOIN sys.columns pc 
                                        ON fkc.parent_object_id = pc.object_id 
                                        AND fkc.parent_column_id = pc.column_id
                                    INNER JOIN sys.columns rc 
                                        ON fkc.referenced_object_id = rc.object_id 
                                        AND fkc.referenced_column_id = rc.column_id
                                WHERE 
                                    parent.name = @TableName  -- For foreign keys FROM this table
                                    -- OR referenced.name = 'YourTableName'  -- For foreign keys TO this table
                                ORDER BY 
                                    fk.name, fkc.constraint_column_id;
                                
";
                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        Command.Parameters.AddWithValue("@TableName", TableName);
                        Connection.Open();
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.HasRows)
                            {
                                dtColumns.Load(Reader);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                onErrorOccur?.Invoke(e);
            }
            return dtColumns;
        }


    }
}
