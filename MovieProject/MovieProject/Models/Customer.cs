using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MovieProject.Models
{
    public class Customer
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        [Required]
        public string LastName { get; set; }
        [StringLength(100)]
        [Required]
        public string BillingAddress { get; set; }
        [StringLength(100)]
        [Required]
        public string BillingZip { get; set; }
        [StringLength(100)]
        [Required]
        public string BillingCity { get; set; }
        [StringLength(100)]
        [Required]
        public string DeliveryAddress { get; set; }
        [StringLength(100)]
        [Required]
        public string DeliveryZip { get; set; }
        [StringLength(100)]
        [Required]
        public string DeliveryCity { get; set; }
        [StringLength(100)]
        [Required]
        public string EmailAddress { get; set; }
        [StringLength(100)]
        [Required]
        public string PhoneNo { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}