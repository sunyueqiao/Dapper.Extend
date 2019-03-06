using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Dapper.Extend.Extension
{
    public class DbConnectionContext
    {
        private readonly IDbConnectionStrategy dbConnectionStrategy;

        public DbConnectionContext(IDbConnectionStrategy dbConnectionStrategy)
        {
            this.dbConnectionStrategy = dbConnectionStrategy;
        }

        public IDbConnection ExecuteStragegy(string connectionString)
        {
            return this.dbConnectionStrategy.InitializeConnection(connectionString);
        }
    }
}
