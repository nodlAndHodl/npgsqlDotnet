using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Membership.Models
{

  public class Note
  {
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public Note()
    {
      this.CreatedAt = DateTime.Now;
    }
  }
  public class Log
  {
    public string Category { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public Log()
    {
      this.CreatedAt = DateTime.Now;
    }
  }
  public class Login
  {
    public string ProviderKey { get; set; }
    public string ProviderValue { get; set; }
    public string ProviderName { get; set; }
    public Login()
    {
      this.ProviderName = "Local";
    }
  }


  public class User
  {

    public User()
    {
        this.Notes = new List<Note>();
        this.Logins = new List<Login>();
        this.Logs = new List<Log>();
    }
    
    public long ID { get; set; }
    public string Email { get; set; }
    public string First { get; set; }
    public string Last { get; set; }
    public string UserKey { get; set; }
    public DateTime CreatedAt { get; set; }
    public int SignInCount { get; set; }
    public IPAddress IP { get; set; }
    public string Status { get; set; }
    public DateTime CurrentSignInAt { get; set; }
    public DateTime LastSignInAt { get; set; }

    public List<Note> Notes { get; set; }
    public List<Log> Logs { get; set; }
    public List<Login> Logins { get; set; }



  }
}
