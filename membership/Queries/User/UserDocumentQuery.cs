using Membership.DataAccess;
using Membership.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Membership.Queries.Users
{
    public class UserDocumentQuery
    {
        ICommandRunner Runner;
        public UserDocumentQuery(ICommandRunner runner)
        {
            Runner = runner;
        }

        public long? ID { get; set; }
        public string Email { get; set; }
        public string UserKey { get; set; }
        public string ProviderKey { get; set; }
        public string ProviderName { get; set; }
        public string ProviderValue { get; set; }

        public UserDocumentQuery()
        {
            this.ProviderName = "Local";
        }

        public User Execute()
        {
            var sqlFormat = "select id, body from user_documents where {0}=@0";
            var sql = "";
            object arg = null;
            if (ID.HasValue)
            {
                arg = ID;
                sql = String.Format(sqlFormat, "id");
            }
            else if (!String.IsNullOrWhiteSpace(Email))
            {
                arg = this.Email;
                sql = String.Format(sqlFormat, "(body->>'Email')");
            }
            else if (!String.IsNullOrWhiteSpace(UserKey))
            {
                arg = this.UserKey;
                sql = String.Format(sqlFormat, "(body->>'UserKey')");
            }
            else if (!String.IsNullOrWhiteSpace(ProviderKey))
            {
                var logins = new List<Login>();
                logins.Add(new Login { ProviderKey = this.ProviderKey, ProviderName = this.ProviderName });
                var login = new { Logins = logins };
                var options = new JsonSerializerSettings();
                options.NullValueHandling = NullValueHandling.Ignore;
                var serialized = JsonConvert.SerializeObject(login, options);
                arg = serialized;
                sql = "select id, body from user_documents where body @> @0";
            }
            else
            {
                throw new InvalidOperationException("Need to set a param");
            }
            var record = Runner.ExecuteSingleDynamic(sql, arg);
            var result = record == null ? null : JsonConvert.DeserializeObject<User>(record.body);
            return result;
        }
    }
}
