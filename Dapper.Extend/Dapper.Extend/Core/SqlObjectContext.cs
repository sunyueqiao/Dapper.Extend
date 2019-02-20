using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using Dapper.Extend.Data.Sql.Attirbute;
using Dapper.Extend.Attirbute;
using Dapper.Extend.Mapper;

namespace Dapper.Extend.Data.Sql.Core
{
    public class SqlObjectContext<T> where T : class
    {
        /// <summary>
        /// 同步字典存储关系映射对象
        /// </summary>
        private static ConcurrentDictionary<string, EntityMapper<T>> _tableDictionary = new ConcurrentDictionary<string, EntityMapper<T>>();

        public static SqlObjectContext<T> Build()
        {
            return SqlObjectContextBuild.Instance;
        }

        public EntityMapper<T> CurrentRelationObject
        {
            get
            {
                return this.RefreshMappingEntity();
            }
        }

        /// <summary>
        /// 构造表集合对象 包含表名 列集合
        /// </summary>
        /// <returns></returns>
        private EntityMapper<T> RefreshMappingEntity()
        {
            Type type = typeof(T);
            string classFullName = type.FullName;
            string className = type.Name;
            EntityMapper<T> tableObject = new EntityMapper<T>
            {
                ClassFullName = classFullName,
                ClassName = className,
                TableName = this.GetTableName(type),
                RelationColumns = new List<RelationMapper>(),
                RelationPrimarys = new List<RelationMapper>()
            };
            if (_tableDictionary.ContainsKey(classFullName) && _tableDictionary.TryGetValue(classFullName, out tableObject))
            {
                return tableObject;
            }

            foreach (PropertyInfo property in Properties)
            {
                string columnName = this.GetColumnName(property);
                string propertyName = property.Name;
                if (EntityAttributeUtil<T>.IsIdentity(propertyName))
                {
                    tableObject.RelationIdentity = new RelationMapper { PropertyName = property.Name, ColumnName = columnName };
                }
                if (EntityAttributeUtil<T>.IsPrimaryKey(propertyName))
                {
                    tableObject.RelationPrimarys.Add(new RelationMapper { ColumnName = columnName, PropertyName = propertyName });
                }
                tableObject.RelationColumns.Add(new RelationMapper { ColumnName = columnName, PropertyName = propertyName });
            }

            _tableDictionary.TryAdd(classFullName, tableObject);
            return tableObject;
        }

        static PropertyInfo[] Properties
        {
            get
            {
                Type type = typeof(T);
                return type.GetProperties();
            }
        }

        private string GetTableName(Type type)
        {
            string tableName = type.Name;
            foreach (CustomAttributeData attr in type.CustomAttributes)
            {
                if (string.Equals(attr.AttributeType.FullName, typeof(TableNameAttribute).FullName))
                {
                    tableName = attr.ConstructorArguments.FirstOrDefault().Value.ToString();
                }
            }
            return tableName;
        }

        private string GetColumnName(PropertyInfo property)
        {
            string columnName = property.Name;
            foreach (CustomAttributeData attr in property.CustomAttributes)
            {
                if (string.Equals(attr.AttributeType.FullName, typeof(ColumnNameAttribute).FullName))
                {
                    columnName = attr.ConstructorArguments.FirstOrDefault().Value.ToString();
                }
            }

            return columnName;
        }

        private class SqlObjectContextBuild
        {
            public static SqlObjectContext<T> Instance { get; } = new SqlObjectContext<T>();
        }
    }
}
