using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcProject.Models
{
    public enum State
    {
        ShoppingCart,
            Sold,
        ForSale
    }
    public class Prodoct
    {
        public int Id { get; set; }
        public int? OwnerId { get; set; }
        public int? UserId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public byte?[] picture1 { get; set; }
        public byte?[] picture2 { get; set; }
        public byte?[] picture3 { get; set; }
        public  State Status{ get; set; }

    }
}