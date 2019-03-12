using Dapper.Extend.Extension;
using Dapper.Extend.Mapper;
using Dapper.Extend.SqlBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Extend
{
    public abstract class SqlDal<TPrimary, TEntity> where TPrimary : struct where TEntity : class
    {
        private readonly DapperExtension _dapperExtension;
        private readonly BaseSqlBuilder<TEntity> _baseSqlBuilder;
        protected SqlDal(string connectionString, BaseSqlBuilder<TEntity> baseSqlBuilder,DapperExtension dapperExtension)
        {
            this._dapperExtension = dapperExtension;
            this._baseSqlBuilder = baseSqlBuilder;
        }

        public virtual TPrimary Insert(TEntity entity)
        {
            SqlObjectData sqlObject = this._baseSqlBuilder.BuildInsert(entity);
            return this._dapperExtension.Insert<TPrimary>(sqlObject.Sql, sqlObject.Parameters);
        }

        public virtual int Update(TEntity entity)
        {
            SqlObjectData sqlObject = this._baseSqlBuilder.BuildUpdate(entity);
            return this._dapperExtension.Update(sqlObject.Sql, sqlObject.Parameters);
        }

        public virtual int Delete(TEntity entity)
        {
            SqlObjectData sqlObject = this._baseSqlBuilder.BuildDelete(entity);
            return this._dapperExtension.Delete(sqlObject.Sql, sqlObject.Parameters);
        }

        public virtual IEnumerable<TEntity> Select(TEntity entity)
        {
            SqlObjectData sqlObjectData = this._baseSqlBuilder.BuildSelect(entity);
            return this._dapperExtension.Select<TEntity>(string.Empty, null);
        }

        public virtual TEntity SelectEntity(TEntity entity)
        {
            SqlObjectData sqlObjectData = this._baseSqlBuilder.BuildSelect(entity);
            return this._dapperExtension.SelectOne<TEntity>(sqlObjectData.Sql, sqlObjectData.Parameters);
        }

        public virtual IEnumerable<TEntity> Select(string sql, DynamicParameters parameters)
        {
            return this._dapperExtension.Select<TEntity>(sql, parameters);
        }
    }
}
