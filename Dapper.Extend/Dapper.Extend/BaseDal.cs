using Dapper.Extend;
using Dapper.Extend.Mapper;
using Dapper.Extend.SqlBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Extend
{
    public abstract class BaseDal<TPrimary, TEntity>
        where TPrimary : struct
        where TEntity : class
    {
        private DapperExtension _dapperExtension;
        private BaseSqlBuilder<TEntity> baseSqlBuilder;
        protected BaseDal(DbEnum dbEnum, string connectionString)
        {
            if (dbEnum == DbEnum.MySql)
            {
                this._dapperExtension = DapperExtension.UseMySql(connectionString);
                this.baseSqlBuilder = MysqlSqlBuilder<TEntity>.Build();
            }
            else if (dbEnum == DbEnum.SqlServer)
            {
                this._dapperExtension = DapperExtension.UseSqlServer(connectionString);
                this.baseSqlBuilder = SqlServerSqlBuilder<TEntity>.Build();
            }
            else
            {
                throw new Exception("未知的数据库类型");
            }
        }

        protected TPrimary Insert(TEntity entity)
        {
            SqlObjectData sqlObject = this.baseSqlBuilder.BuildInsert(entity);
            return this._dapperExtension.Insert<TPrimary>(sqlObject.Sql, sqlObject.Parameters);
        }

        protected int Update(TEntity entity)
        {
            SqlObjectData sqlObject = this.baseSqlBuilder.BuildUpdate(entity);
            return this._dapperExtension.Update(sqlObject.Sql, sqlObject.Parameters);
        }

        protected IEnumerable<TEntity> Select(TEntity entity)
        {
            SqlObjectData sqlObjectData = this.baseSqlBuilder.BuildSelect(entity);
            return this._dapperExtension.Select<TEntity>(string.Empty, null);
        }

        protected TEntity SelectEntity(TEntity entity)
        {
            SqlObjectData sqlObjectData = this.baseSqlBuilder.BuildSelect(entity);
            return this._dapperExtension.SelectOne<TEntity>(sqlObjectData.Sql, sqlObjectData.Parameters);
        }

    }
}
