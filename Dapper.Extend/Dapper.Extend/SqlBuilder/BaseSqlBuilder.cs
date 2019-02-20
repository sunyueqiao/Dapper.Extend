using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections.Concurrent;
using Dapper;
using Dapper.Extend.Data.Sql.Attirbute;
using Dapper.Extend.Data.Sql.Core;
using Dapper.Extend.Mapper;

namespace Dapper.Extend.SqlBuilder
{
    public abstract class BaseSqlBuilder<T> where T : class
    {
        public virtual SqlObjectData BuildInsert(T t)
        {
            EntityMapper<T> entityMapper = SqlObjectContext<T>.Build()
                .CurrentRelationObject
                .FilterIdentity();
            StringBuilder sql = new StringBuilder();
            sql.Append($"insert into {entityMapper.TableName}")
                .Append($"({string.Join(",", entityMapper.ColumnNames)}) values")
                .Append($"(@{string.Join(",@", entityMapper.PropertyNames)})");
            DynamicParameters parameters = this.PrepareParameters(t);
            return SqlObjectData.Init(entityMapper.RelationIdentity != null, sql.ToString(), parameters);
        }

        public virtual SqlObjectData BuildUpdate(T t)
        {
            EntityMapper<T> entityMapper = SqlObjectContext<T>.Build()
                .CurrentRelationObject
                .FilterPrimaryKey()
                .FilterIdentity();
            DynamicParameters parameters = this.PrepareParameters(t);
            StringBuilder sql = new StringBuilder();
            sql.Append($"update {entityMapper.TableName} set");
            int index = 0;

            entityMapper.ColumnNames.ForEach(column =>
            {
                sql.Append($"{column}=@{column}");
                index = entityMapper.ColumnNames.IndexOf(column);
                if (index < entityMapper.RelationColumns.Count - 1)
                {
                    sql.Append(",");
                }
            });
            sql.Append(" where ").Append(BuildSqlCondition(entityMapper));
            return SqlObjectData.Init(entityMapper.RelationIdentity != null, sql.ToString(), parameters);
        }

        public virtual SqlObjectData BuildDelete(T t)
        {
            EntityMapper<T> entityMapper = SqlObjectContext<T>.Build().CurrentRelationObject;
            StringBuilder sql = new StringBuilder();
            sql.Append($"delete from {entityMapper.TableName} where ").Append(BuildSqlCondition(entityMapper));
            DynamicParameters parameters = this.PrepareParameters(t);
            return SqlObjectData.Init(entityMapper.RelationIdentity != null, sql.ToString(), parameters);
        }

        public virtual SqlObjectData BuildSelect(T t)
        {
            EntityMapper<T> entityMapper = SqlObjectContext<T>.Build().CurrentRelationObject;
            string condition = BuildSqlCondition(entityMapper);
            StringBuilder sql = new StringBuilder();
            sql.Append($"select {string.Join(",", entityMapper.ColumnNames)} from {entityMapper.TableName}");
            if (!string.IsNullOrEmpty(condition))
            {
                sql.Append(condition);
            }
            DynamicParameters parameters = this.PrepareParameters(t);
            return SqlObjectData.Init(entityMapper.RelationIdentity != null, sql.ToString(), parameters);

        }

        private string BuildSqlCondition(EntityMapper<T> tableObject)
        {
            StringBuilder condition = new StringBuilder();
            int index = 0;
            tableObject.RelationPrimarys.ForEach(column =>
            {
                condition.Append($"{column.ColumnName}=@{column.PropertyName}");
                index = tableObject.RelationPrimarys.IndexOf(column);
                if (index < tableObject.RelationPrimarys.Count - 1)
                {
                    condition.Append(" and ");
                }
            });
            return condition.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private DynamicParameters PrepareParameters(T t)
        {
            Type type = typeof(T);
            DynamicParameters parameters = new DynamicParameters();
            PropertyInfo[] propertyInfos = type.GetProperties();
            foreach (PropertyInfo property in propertyInfos)
            {
                if (!EntityAttributeUtil<T>.IsIdentity(property.Name))
                {
                    parameters.Add($"@{property.Name}", property.GetValue(t));
                }
            }

            return parameters;
        }
    }
}
