using Membership.Commands.Membership;
using Membership.DataAccess;
using Membership.Models;
using Membership.Queries.Actors;
using Membership.Queries.Sales;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Membership
{
  class Program
  {
    static void Main(string[] args)
    {
          var db = new CommandRunner("dvds");

            var q = new RegisterCommand(db);
            q.Email = "semail@mailer.com";
            q.Password = "password";
            q.Confirmation = "password";
            q.First = "Nick";
            q.Last = "Shoup";
            var result = q.Execute();
            Console.WriteLine(result.Message, result.NewUserId, result.Status);


            var qAuth = new AuthenticateCommand(db);
            qAuth.ProviderKey = "semail@mailer.com";
            qAuth.ProviderValue = "password";

            var authResult = qAuth.Execute();
            Console.WriteLine(authResult.Message, authResult.UserID);

            var query = new RawSalesByDate(db);
            query.Year = 2007;
            var sales = query.Execute();

            foreach (var sale in sales)
            {
                //Console.WriteLine(sale.Title);
            }

          var films = db.Execute<Film>("select * from film");

            foreach (var film in films)
            {
               // Console.WriteLine(film.Title);
            }


            var filmsDynamic = db.ExecuteDynamic("select * from film");

            foreach (var film in filmsDynamic)
            {
               // Console.WriteLine(film.title);
            }



            //var cmd = db.BuildCommand("insert into actor(first_name, last_name, last_update) values (@0, @1, @2)", "Nick", "Shoup", DateTime.Now);

            //var resulsts = db.Transact(cmd);
            //foreach (var result in resulsts)
            //{
            //    Console.WriteLine(result);
            //}

            var actorQuery = new ActorQuery(db);
            actorQuery.ActorId = 3;
            var actors = actorQuery.Execute();
            Console.WriteLine(actors.Fullname);
            foreach (var film in actors.Films)
            {
               // Console.WriteLine(film.Title) ;
            }

            var actor1 = new Actor() { First = "Woody", Last = "Harrelson" };
            var actor2 = new Actor() { First = "Joe", Last = "Biff" };
            var actor3 = new Actor() { First = "Jolene", Last = "Silidkdk" };

            var qCommand = new Commands.Actors.AddBatchOfActors(db);
            qCommand.Actors = new Actor[] { actor1, actor2, actor3 };
            qCommand.Execute();
            Console.Read();
    }
  }
}
