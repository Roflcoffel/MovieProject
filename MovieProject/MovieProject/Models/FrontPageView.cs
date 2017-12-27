using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieProject.Models
{
    public class FrontPageView
    {
        private List<Movie> mostPopular = new List<Movie>();
        public List<Movie> MostPopular
        {
            get
            {
                return mostPopular;
            }
            set
            {
                mostPopular = value;
            }
        }
        public List<Movie> Newest { get; set; }
        public List<Movie> Oldest { get; set; }
        public List<Movie> Cheapest { get; set; }
        public Customer BestCustomer { get; set; }
    }
}