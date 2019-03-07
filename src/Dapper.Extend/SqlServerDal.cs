using Dapper.Extend.Extension;
using Dapper.Extend.Mapper;
using Dapper.Extend.SqlBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Extend
{
    public class SqlServerDal<TPrimary, TEntity> 
        where TPrimary : struct 
        where TEntity : class
    {
        private readonly DapperExtension _dapperExtension;
        private readonly BaseSqlBuilder<TEntity> _baseSqlBuilder;
        protected SqlServerDal(string connectionString)
        {
            this._dapperExtension = DapperExtension.UseSqlServer(connectionString);
            this._baseSqlBuilder = SqlServerSqlBuilder<TEntity>.Build();
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

        public int Delete(TEntity entity)
        {
            SqlObjectData sqlObject = this._baseSqlBuilder.BuildDelete(entity);
            return this._dapperExtension.Delete(sqlObject.Sql, sqlObject.Parameters);
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
