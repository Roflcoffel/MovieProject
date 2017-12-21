﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MovieProject.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [StringLength(30)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public bool Admin { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
    }
}