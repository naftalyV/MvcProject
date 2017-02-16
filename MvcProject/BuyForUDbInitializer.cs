﻿using System;
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
            //var users = new List<User>() {
            //    new User() { },
            //    new User() { },
            //    new User() { },
            //    new User() { },
            //    new User() { },
            //    new User() { }
            //};
            var u1 = new User() {
                id = 1,
                FirstName = "naftaly",
                LastNama = "wiesenstern",
                BirthDate = DateTime.Now.AddYears(-32),
                Email = "naftaly276@gmail.com",
                UserName = "tooly",
                Password = "123"
            };
            context.Users.Add(u1);
        }
    }
}