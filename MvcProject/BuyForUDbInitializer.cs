using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MvcProject;
using MvcProject.Models;

namespace MvcProject
{
    public class BuyForUDbInitializer :DropCreateDatabaseAlways<BuyForUDB>
    {
        protected override void Seed(BuyForUDB context)
        {
            var users = new List<User>() {
                new User() { },
                new User() { },
                new User() { },
                new User() { },
                new User() { },
                new User() { }
            };
            
            //context.Users.Add();
        }
    }
}