using Membership.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Membership.Commands.Membership
{
    public class RegistrationResult
    {
        public long NewUserId { get; set; }
        public string Email { get; set; }
        public string  Status { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
    public class RegisterCommand
    {

        public string Email { get; set; }
        public string Password { get; set; }
        public string Confirmation { get; set; }
        public string IP { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        public string Status { get; set; }

        ICommandRunner CommandRunner;
        public RegisterCommand(ICommandRunner commandRunner)
        {
            this.CommandRunner = commandRunner;
            Status = "pending";
            IP = "127.0.0.1";
        }

        public RegistrationResult Execute()
        {
            var sql = $"select * from register(@0, @1, @2, @3, @4, @5, @6);";
            return CommandRunner.ExecuteSingle<RegistrationResult>(sql, 
                this.Email, this.Password, this.Confirmation, 
                this.IP, this.Status, this.First, this.Last);
        }
    }
}
