using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MovieProject.Models
{
    public class MovieInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<MovieContext>
    {
        protected override void Seed(MovieContext context)
        {
            var customers = new Customer
            {
                FirstName = "Jonas",
                LastName = "Gray",
                BillingAddress = "23 Green Corner Street",

                
            };
            //base.Seed(context);
        }
    }
}