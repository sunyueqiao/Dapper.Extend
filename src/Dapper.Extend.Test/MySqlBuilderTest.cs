using System;
using System.Diagnostics;
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
        private readonly UserInfoDal userInfoDal = new UserInfoDal();
        [TestMethod]
        public void TestInsertSql()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < 10000; i++)
            {
                UserInfo userInfo = new UserInfo { UserName = "张三" };
                long userId = this.userInfoDal.Insert(userInfo);
            }
            stopwatch.Stop();
            TimeSpan timeSpan = stopwatch.Elapsed;
            double seconds = timeSpan.TotalSeconds;
            Console.WriteLine(seconds);
        }

        [TestMethod]
        public void TestUpdateSql()
        {
            UserInfo userInfo = new UserInfo { UserId = 1, UserName = "李四" };
            int result = this.userInfoDal.Update(userInfo);
            Assert.IsFalse(result < 1);
        }

        [TestMethod]
        public void TestDeleteSql()
        {
            UserInfo userInfo = new UserInfo { UserId = 1, UserName = "李四" };
            int result = this.userInfoDal.Delete(userInfo);
            Assert.IsFalse(result < 1);
        }

        [TestMethod]
        public void TestSelectSql()
        {
            UserInfo userInfo = new UserInfo { UserId = 2 };
            UserInfo result = this.userInfoDal.SelectEntity(userInfo);
            Assert.IsTrue(result.UserId > 0);
        }
    }
}
