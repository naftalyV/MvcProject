namespace MvcProject
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class BuyForUDB : DbContext
    {
        
        public BuyForUDB()
            : base("name=BuyForUDB")
        {
        }

       
        // public virtual DbSet<Users> MyEntities { get; set; }
    }


}