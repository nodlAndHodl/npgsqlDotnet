using Membership.DataAccess;
using Membership.Models;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Membership.Commands.Users
{
    public class SaveUserCommand
    {
        ICommandRunner Runner;
        public SaveUserCommand(ICommandRunner runner)
        {
            Runner = runner;
        }

        public int Execute(User user)
        {
            //validations
            //check if they exist
            var exists = Runner.ExecuteSingle<User>("select id from user_documents where id=@0", user.ID) != null;
            //use jsonconvert on the user object.
            var serialized = JsonConvert.SerializeObject(user);
            NpgsqlCommand cmd;
            if (exists)
            {                                                                               //setting the passed in params to a tsvector
                cmd = Runner.BuildCommand("update user_documents set body=@0, search_field=to_tsvector(concat(@2,@3,@4)) where id=@1", serialized, user.ID, user.Email, user.First, user.Last);
            }
            else
            {
                cmd = Runner.BuildCommand("insert into user_documents (body, search_field) values(@0, to_tsvector(concat(@2,@3,@4)))", serialized, user.Email, user.First, user.Last);
            }
            return Runner.Transact(cmd).Sum();
        }
    }
}
