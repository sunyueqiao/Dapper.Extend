using Dapper.Extend;
using Dapper.Extend.Extension;
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
        private readonly DapperExtension _dapperExtension;
        private readonly BaseSqlBuilder<TEntity> _baseSqlBuilder;
        protected BaseDal(DbEnum dbEnum, string connectionString)
        {
            if (dbEnum == DbEnum.MySql)
            {
                this._dapperExtension = DapperExtension.UseMySql(connectionString);
                this._baseSqlBuilder = MysqlSqlBuilder<TEntity>.Build();
            }
            else if (dbEnum == DbEnum.SqlServer)
            {
                this._dapperExtension = DapperExtension.UseSqlServer(connectionString);
                this._baseSqlBuilder = SqlServerSqlBuilder<TEntity>.Build();
            }
            else
            {
                throw new Exception("未知的数据库类型");
            }
        }

        public TPrimary Insert(TEntity entity)
        {
            SqlObjectData sqlObject = this._baseSqlBuilder.BuildInsert(entity);
            return this._dapperExtension.Insert<TPrimary>(sqlObject.Sql, sqlObject.Parameters);
        }

        public int Update(TEntity entity)
        {
            SqlObjectData sqlObject = this._baseSqlBuilder.BuildUpdate(entity);
            return this._dapperExtension.Update(sqlObject.Sql, sqlObject.Parameters);
        }

        public IEnumerable<TEntity> Select(TEntity entity)
        {
            SqlObjectData sqlObjectData = this._baseSqlBuilder.BuildSelect(entity);
            return this._dapperExtension.Select<TEntity>(string.Empty, null);
        }

        public TEntity SelectEntity(TEntity entity)
        {
            SqlObjectData sqlObjectData = this._baseSqlBuilder.BuildSelect(entity);
            return this._dapperExtension.SelectOne<TEntity>(sqlObjectData.Sql, sqlObjectData.Parameters);
        }

        public IEnumerable<TEntity> Select(string sql, DynamicParameters parameters)
        {
            return this._dapperExtension.Select<TEntity>(sql, parameters);
        }

    }
}
