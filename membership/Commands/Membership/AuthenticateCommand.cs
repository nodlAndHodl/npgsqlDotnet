using Membership.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Membership.Commands.Membership
{
    public class AuthenticationResult
    {
        public long UserID { get; set; }
        public long SessionID { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public string DisplayName { get; set; }
    }

    public class AuthenticateCommand
    {
        ICommandRunner Runner;
        public AuthenticateCommand(ICommandRunner runner)
        {
            Runner = runner;
            this.ProviderName = "local";
        }

        public string ProviderName { get; set; }
        public string ProviderKey { get; set; }
        public string ProviderValue { get; set; }

        public AuthenticationResult Execute()
        {
            var sql = "select * from authenticate(@0,@1,@2)";
            return Runner.ExecuteSingle<AuthenticationResult>(sql,
              this.ProviderKey,
              this.ProviderValue,
              this.ProviderName);
        }

    }
}
