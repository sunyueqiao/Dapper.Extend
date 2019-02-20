using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Extend.Mapper
{
    public class SqlObjectData
    {
        public bool ExistIdentityColumn { get; set; }
        public string Sql { get; set; }
        public DynamicParameters Parameters { get; set; }

        public static SqlObjectData Init(bool existsIdentity, string sql, DynamicParameters parameters)
        {
            SqlObjectData sqlObject = new SqlObjectData { ExistIdentityColumn = existsIdentity, Sql = sql, Parameters = parameters };
            return sqlObject;
        }
    }
}
