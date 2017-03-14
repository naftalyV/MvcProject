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
      
        public int Id { get; set; }
        [Display(Name = "שם פרטי")]
        [Required(ErrorMessage = "Please enter a first name")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Display(Name = "שם משפחה")]
        [Required(ErrorMessage = "Please enter a last name")]
        [StringLength(50)]
        public string LastNama { get; set; }
        [Display(Name = "תאריך לידה")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Display(Name = "דואר אלקטרוני")]
        [Required(ErrorMessage = "Please enter a email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter a user name")]
        [StringLength(50)]
        [Display(Name = "שם משתמש")]
        public string UserName { get; set; }
        [Display(Name = "סיסמא")]
        [Required(ErrorMessage = "Please enter a password")]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "אימות סיסמא")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Fields \"Password\" and \"Confirm Password\" must be equal.")]
        public string ConfirmPassword { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Product> Products { get; set; }

        [InverseProperty("Owner")]
        public virtual ICollection<Product> ProductsToSell { get; set; }
    }
}