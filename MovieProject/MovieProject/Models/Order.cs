using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MovieProject.Models
{
    public class Order
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public int CustomerId { get; set; }

        public virtual ICollection<OrderRow> OrderRows { get; set; }
        public virtual Customer Customer { get; set; }
    }
}