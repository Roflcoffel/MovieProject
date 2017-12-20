using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MovieProject.Models
{
    public class OrderRow
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int MovieId { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual Order Order { get; set; }
    }
}