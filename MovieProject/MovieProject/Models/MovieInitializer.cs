using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace MovieProject.Models
{
    public class MovieInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<MovieContext>
    {
        protected override void Seed(MovieContext context)
        {
            context.Movies.AddOrUpdate(i => i.Title,
                new Movie
                {
                    Title="kfhasd",
                    
                
                }
                    );
            //var customers = new Customer
            //{
            //    FirstName = "Jonas",
            //    LastName = "Gray",
            //    BillingAddress = "23 Green Corner Street",

                
            //};
            //base.Seed(context);
        }
    }
}