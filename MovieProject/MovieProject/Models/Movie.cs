using System;
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

        [DataType(DataType.ImageUrl)]
        public string Url { get; set; }

        public int Rating { get; set; }

        public virtual ICollection<OrderRow> OrderRows { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

        //Functions
        public void UpdateRating()
        {
            Rating = Reviews.Sum(r => r.Rating) / Reviews.Count();
        }
    }
}