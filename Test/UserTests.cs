using System;
using Membership.DataAccess;
using Membership.Models;
using Membership.Queries.Membership;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class UserTests
    {
        ICommandRunner db;
        User user;
        public UserTests()
        {
            Helpers.Loader.ReloadDb();
            db = new CommandRunner("dvds");
            var userQuery = new UserQuery(db) { Email = "test@text.com" };
            user = userQuery.Execute();
            //user = db.ExecuteSingle<User>("select * from users;");
        }

        [TestMethod]
        public void UserShouldExist()
        {
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void UserHasKey()
        {
            Assert.IsNotNull(user.UserKey);
        }

    }
}
