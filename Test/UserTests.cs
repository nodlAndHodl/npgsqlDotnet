using System;
using Membership.Commands.Users;
using Membership.DataAccess;
using Membership.Models;
using Membership.Queries.Membership;
using Membership.Queries.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class UserTests
    {
        ICommandRunner db;
        User user;
        User documentUser;
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

        [TestMethod]
        public void CreateUserDocument()
        {
            var docUser = new User();
            docUser.Email = "nshoup@test.com";
            docUser.First = "Nick";
            docUser.Last = "Shoup";
            docUser.UserKey = docUser.Email;
            docUser.Notes.Add(new Note() { Description = "Added as a test" });
            docUser.Logs.Add(new Log() { Description = "Added as a test log", Category = "Test Category" });
            var quer = new SaveUserCommand(db);
            var result = quer.Execute(docUser);
            Console.WriteLine($"Added user: {result}");
            Assert.IsTrue(result == 1);
        }

        [TestMethod]
        public void UserDocumentExists()
        {
            var q = new UserDocumentQuery(db);
            q.Email = "nshoup@test.com";
            var resultUser = q.Execute();
            Assert.IsNotNull(resultUser);
        }
    }
}
