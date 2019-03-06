using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Dapper.Extend.Extension
{
    public interface IDbConnectionStrategy
    {
        /// <summary>
        /// 初始化连接
        /// </summary>
        /// <returns></returns>
        IDbConnection InitializeConnection(string connectionString);
    }
}
