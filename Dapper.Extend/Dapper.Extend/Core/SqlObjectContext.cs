using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using Dapper.Extend.Data.Sql.Attirbute;
using Dapper.Extend.SqlObject.Mapper;

namespace Dapper.Extend.Data.Sql.Core
{
    public class SqlObjectContext<T> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        private static ConcurrentDictionary<string, TableObject<T>> _tableDictionary = new ConcurrentDictionary<string, TableObject<T>>();

        public static SqlObjectContext<T> Build()
        {
            return SqlObjectContextBuild.Instance;
        }

        public TableObject<T> CurrentRelationObject
        {
            get
            {
                return GetMappingEntity();
            }
        }

        /// <summary>
        /// 构造表集合对象 包含表名 列集合
        /// </summary>
        /// <returns></returns>
        private TableObject<T> RefreshMappingEntity()
        {
            Type type = typeof(T);
            string classFullName = type.FullName;
            string className = type.Name;
            List<string> columns = new List<string>();
            List<string> primaryColumns = new List<string>();
            TableObject<T> tableObject = new TableObject<T> { TableName = className, Columns = columns, PrimaryColumns = primaryColumns };
            if (_tableDictionary.ContainsKey(classFullName) && _tableDictionary.TryGetValue(classFullName, out tableObject))
            {
                return tableObject;
            }

            foreach (PropertyInfo property in Properties)
            {
                if (EntityAttributeUtil<T>.IsIdentity(property.Name))
                {
                    tableObject.IdentityColumn = property.Name;
                }
                if (EntityAttributeUtil<T>.IsPrimaryKey(property.Name))
                {
                    tableObject.PrimaryColumns.Add(property.Name);
                }
                tableObject.Columns.Add(property.Name);
            }

            _tableDictionary.TryAdd(classFullName, tableObject);
            return tableObject;
        }

        public TableObject<T> GetMappingEntity()
        {
            return this.RefreshMappingEntity();
        }

        static PropertyInfo[] Properties
        {
            get
            {
                Type type = typeof(T);
                return type.GetProperties();
            }
        }

        private class SqlObjectContextBuild
        {
            public static SqlObjectContext<T> Instance { get; } = new SqlObjectContext<T>();
        }
    }
}
