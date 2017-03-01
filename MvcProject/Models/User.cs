﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcProject.Models
{
    public class User
    {
        public int id { get; set; }
        [Required(ErrorMessage ="Please enter a first name")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter a last name")]
        [StringLength(50)]
        public string LastNama { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Please enter a email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter a user name")]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter a password")]
        [StringLength(50)]
        public string Password { get; set; }

    }
}