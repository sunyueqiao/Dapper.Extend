using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Dapper.Extend.Data.Sql.SqlObject;
using System.Collections.Concurrent;
using Dapper;
using Dapper.Extend.Data.Sql.Attirbute;
using Dapper.Extend.Data.Sql.Core;

namespace Dapper.Extend.Data.Sql.Mapper
{
    public static class SqlBuilder<T> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private static DynamicParameters PrepareParameters(T t)
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

        public static SqlObject BuildInsert(T t)
        {
            TableObject<T> tableObject = SqlObjectContext<T>.Build()
                .CurrentRelationObject
                .FilterIdentity();
            StringBuilder sql = new StringBuilder();
            sql.Append($"insert into {tableObject.TableName}")
                .Append($"({string.Join(",", tableObject.Columns)}) values")
                .Append($"(@{string.Join(",@", tableObject.Columns)})");
            DynamicParameters parameters = PrepareParameters(t);
            SqlObject sqlObject = new SqlObject { Sql = sql.ToString(), Parameters = parameters };

            return sqlObject;
        }

        public static SqlObject BuildUpdate(T t)
        {
            TableObject<T> tableObject = SqlObjectContext<T>.Build()
                .CurrentRelationObject
                .FilterPrimaryKey()
                .FilterIdentity();
            DynamicParameters parameters = PrepareParameters(t);
            StringBuilder sql = new StringBuilder();
            sql.Append($"update {tableObject.TableName} set");
            int index = 0;
            tableObject.Columns.ForEach(column =>
            {
                sql.Append($"{column}=@{column}");
                index = tableObject.Columns.IndexOf(column);
                if (index < tableObject.Columns.Count - 1)
                {
                    sql.Append(",");
                }
            });
            sql.Append(" where ").Append(BuildSqlCondition(tableObject));
            SqlObject sqlObject = new SqlObject { Sql = sql.ToString(), Parameters = parameters };
            return sqlObject;
        }

        public static SqlObject BuildDelete(T t)
        {
            TableObject<T> tableObject = SqlObjectContext<T>.Build().CurrentRelationObject;
            StringBuilder sql = new StringBuilder();
            sql.Append($"delete from {tableObject.TableName} where ").Append(BuildSqlCondition(tableObject));
            DynamicParameters parameters = PrepareParameters(t);
            SqlObject sqlObject = new SqlObject { Sql = sql.ToString(), Parameters = parameters };
            return sqlObject;
        }

        private static string BuildSqlCondition(TableObject<T> tableObject)
        {
            StringBuilder condition = new StringBuilder();
            int index = 0;
            tableObject.PrimaryColumns.ForEach(column =>
            {
                condition.Append($"{column}=@{column}");
                index = tableObject.PrimaryColumns.IndexOf(column);
                if (index < tableObject.PrimaryColumns.Count - 1)
                {
                    condition.Append(" and ");
                }
            });
            return condition.ToString();
        }
    }
}
