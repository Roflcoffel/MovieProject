﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MovieProject.Models
{
    public class Movie
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [StringLength(100)]
        [Required]
        public string Director { get; set; }
        [Required]
        public int ReleaseYear { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        public virtual ICollection<OrderRow> OrderRows { get; set; }
    }
}