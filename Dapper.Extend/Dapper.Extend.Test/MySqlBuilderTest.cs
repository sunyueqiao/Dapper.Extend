using System;
using Dapper.Extend.Mapper;
using Dapper.Extend.SqlBuilder;
using Dapper.Extend.Test.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dapper.Extend.Test
{
    [TestClass]
    public class MySqlBuilderTest
    {
        [TestMethod]
        public void TestInsertSql()
        {
            UserInfo userInfo = new UserInfo { UserId = 123, UserName = "张三" };
            SqlObjectData sqlObjectData = MysqlSqlBuilder<UserInfo>.Build().BuildInsert(userInfo);
            Console.WriteLine(sqlObjectData.ToString());
        }
    }
}
