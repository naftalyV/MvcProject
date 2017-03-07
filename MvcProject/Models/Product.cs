using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcProject.Models
{
    public enum State
    {
        ForSale,
        ShoppingCart,
            Sold
    }
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [InverseProperty("Owner ")]
       // [ForeignKey("Owner")]
      //  [Column(Order = 1)]
        public int? OwnerId { get; set; }
      //  [ForeignKey("User")]
       // [Column(Order = 2)]
       [InverseProperty(" User")]
        public int? UserId { get; set; }
        [Required(ErrorMessage = "Please enter a Title")]
        [StringLength(50)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter a Short Description")]
        [StringLength(500)]
        public string ShortDescription { get; set; }
        [Required(ErrorMessage = "Please enter a Long Description")]
        [StringLength(4000)]
        public string LongDescription { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required]
        public decimal Price { get; set; }
        public byte[] picture1 { get; set; }
        public byte[] picture2 { get; set; }
        public byte[] picture3 { get; set; }
        [Required]
        public  State Status{ get; set; }
        public virtual User User { get; set; }
        public virtual User Owner { get; set; }
    }
}