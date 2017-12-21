using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MovieProject.Models
{
    public class MovieContext : DbContext
    {
        public MovieContext() : base("MovieConnection")
        {

        }

        public DbSet<Movie> Movies { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderRow> OrderRows { get; set; }
    }
}