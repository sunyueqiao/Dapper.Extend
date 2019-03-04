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
    public class SqlMapperContext<T> where T : class
    {
        public static SqlMapperContext<T> Build()
        {
            return SqlObjectContextBuilder.Instance;
        }

        public EntityMapper<T> GetEntityMapper(T t)
        {
            return this.RefreshMapperData(t);
        }

        /// <summary>
        /// 构造表集合对象 包含表名 列集合
        /// </summary>
        /// <returns></returns>
        private EntityMapper<T> RefreshMapperData(T t)
        {
            Type type = typeof(T);
            string classFullName = type.FullName;
            string className = type.Name;
            EntityMapper<T> entityMapper = new EntityMapper<T>
            {
                ClassFullName = classFullName,
                ClassName = className,
                TableName = this.GetTableName(type),
                RelationColumns = new List<RelationMapper>(),
                RelationPrimarys = new List<RelationMapper>()
            };
            foreach (PropertyInfo property in t.GetType().GetProperties())
            {
                string columnName = this.GetColumnName(property);
                string propertyName = property.Name;
                object propertyValue = property.GetValue(t);
                if (EntityAttributeUtil<T>.IsIdentity(propertyName))
                {
                    entityMapper.RelationIdentity = new RelationMapper { PropertyName = property.Name, ColumnName = columnName, PropertyValue = propertyValue };
                }
                if (EntityAttributeUtil<T>.IsPrimaryKey(propertyName))
                {
                    entityMapper.RelationPrimarys.Add(new RelationMapper { ColumnName = columnName, PropertyName = propertyName, PropertyValue = propertyValue });
                }
                entityMapper.RelationColumns.Add(new RelationMapper { ColumnName = columnName, PropertyName = propertyName, PropertyValue = propertyValue });
            }

            return entityMapper;
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

        private class SqlObjectContextBuilder
        {
            public static SqlMapperContext<T> Instance { get; } = new SqlMapperContext<T>();
        }
    }
}
