using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
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
            EntityMapper<T> entityMapper = SqlMapperContext<T>.Build()
                .GetEntityMapper(t)
                .ExcludeIdentity();
            StringBuilder sql = new StringBuilder();
            sql.Append($"insert into {entityMapper.TableName}")
                .Append($"({string.Join(",", entityMapper.InsertColumns.Keys)}) values ")
                .Append($"(@{string.Join(",@", entityMapper.InsertColumns.Values)})");
            DynamicParameters parameters = this.PrepareParameters(entityMapper);
            return SqlObjectData.Init(entityMapper.RelationIdentity != null, sql.ToString(), parameters);
        }

        public virtual SqlObjectData BuildUpdate(T t)
        {
            EntityMapper<T> entityMapper = SqlMapperContext<T>.Build()
                .GetEntityMapper(t);
            DynamicParameters parameters = this.PrepareParameters(entityMapper);
            StringBuilder sql = new StringBuilder();
            sql.Append($"update {entityMapper.TableName} set ");
            int index = 0;
            foreach (var item in entityMapper.UpdateColumns)
            {
                sql.Append($"{item.Key}=@{item.Value}");
                if (index < entityMapper.InsertColumns.Count - 1)
                {
                    sql.Append(",");
                }

                index++;
            }
            sql.Append(" where ").Append(BuildSqlCondition(entityMapper));
            return SqlObjectData.Init(entityMapper.RelationIdentity != null, sql.ToString(), parameters);
        }

        public virtual SqlObjectData BuildDelete(T t)
        {
            EntityMapper<T> entityMapper = SqlMapperContext<T>.Build().GetEntityMapper(t);
            StringBuilder sql = new StringBuilder();
            sql.Append($"delete from {entityMapper.TableName} where ").Append(BuildSqlCondition(entityMapper));
            DynamicParameters parameters = this.PrepareParameters(entityMapper);
            return SqlObjectData.Init(entityMapper.RelationIdentity != null, sql.ToString(), parameters);
        }

        public virtual SqlObjectData BuildSelect(T t)
        {
            EntityMapper<T> entityMapper = SqlMapperContext<T>.Build().GetEntityMapper(t);
            string condition = BuildSqlCondition(entityMapper);
            StringBuilder sql = new StringBuilder();
            List<string> columns = new List<string>();
            entityMapper.RelationColumns.ForEach(column =>
            {
                columns.Add($"{column.ColumnName} as {column.PropertyName} ");
            });
            sql.Append($"select {string.Join(",", columns)} from {entityMapper.TableName}");
            if (!string.IsNullOrEmpty(condition))
            {
                sql.Append(" where ").Append(condition);
            }

            DynamicParameters parameters = this.PrepareParameters(entityMapper);
            return SqlObjectData.Init(entityMapper.RelationIdentity != null, sql.ToString(), parameters);

        }

        private string BuildSqlCondition(EntityMapper<T> entityMapper)
        {
            StringBuilder condition = new StringBuilder();
            int index = 0;
            entityMapper.RelationPrimarys.ForEach(column =>
            {
                condition.Append($"{column.ColumnName}=@{column.PropertyName}");
                index = entityMapper.RelationPrimarys.IndexOf(column);
                if (index < entityMapper.RelationPrimarys.Count - 1)
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
        /// <param name="mapper"></param>
        /// <returns></returns>
        private DynamicParameters PrepareParameters(EntityMapper<T> mapper)
        {
            DynamicParameters parameters = new DynamicParameters();
            foreach (var item in mapper.RelationColumns)
            {
                parameters.Add($"@{item.PropertyName}", item.PropertyValue);
            }

            return parameters;
        }
    }
}
