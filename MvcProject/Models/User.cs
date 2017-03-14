﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcProject.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a first name")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter a last name")]
        [StringLength(50)]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Please enter a email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter a user name")]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter a password")]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Product> Products { get; set; }

        [InverseProperty("Owner")]
        public virtual ICollection<Product> ProductsToSell { get; set; }
    }
}