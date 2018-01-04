using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MovieProject.Models
{
    public class Review
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int MovieId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public int Rating { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual Customer Customer { get; set; }
    }       
}