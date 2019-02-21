using System;
using Dapper.Extend.Mapper;
using Dapper.Extend.SqlBuilder;
using Dapper.Extend.Test.Dal;
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
            UserInfo userInfo = new UserInfo { UserId = 3, UserName = "张三" };
            SqlObjectData sqlObjectData = MysqlSqlBuilder<UserInfo>.Build().BuildInsert(userInfo);
            UserInfoDal userInfoDal = new UserInfoDal();
            long userId = userInfoDal.Insert(userInfo);
            Assert.IsFalse(userId < 1);
        }
    }
}
