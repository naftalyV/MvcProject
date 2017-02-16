namespace MvcProject
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class BuyForUDB : DbContext
    {
        
        public BuyForUDB()
            : base("name=BuyForUDB")
        {
            Database.SetInitializer(new BuyForUDbInitializer());
        }


        public virtual DbSet<User> Users { get; set; }
    }


}