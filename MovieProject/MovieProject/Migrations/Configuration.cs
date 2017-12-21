namespace MovieProject.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MovieProject.Models.MovieContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MovieProject.Models.MovieContext context)
        {
            context.Movies.AddOrUpdate(m => m.Title,
                new Movie { Title = "Interstellar", Director = "Christoper Nolan", ReleaseYear = 2014, Price = 180 },
                new Movie { Title = "Hobbit: Battle of the five armies", Director = "Peter Jackson", ReleaseYear = 2014, Price = 179},
                new Movie { Title = "The Wolf of Wall Street", Director = "Martin Scorcese", ReleaseYear = 2013, Price = 110},
                new Movie { Title = "Pulp Fiction", Director = "Quentin Tarantino", ReleaseYear = 1994, Price = 49}
            );

            context.Customers.AddOrUpdate(p => p.FirstName,
                new Customer { FirstName = "Jonas", LastName = "Gray", BillingAddress = "23 Green Corner Street", BillingZip = "56743", BillingCity = "Birmingham", DeliveryAddress = "23 Green Corner Street", DeliveryZip = "56743", DeliveryCity = "Birmingham", EmailAddress = "Jonas.gray@hotmail.com", PhoneNo = "0708123456" },
                new Customer { FirstName = "Jane", LastName = "Harolds", BillingAddress = "10 West Street", BillingZip = "48213", BillingCity = "London", DeliveryAddress = "10 West Street", DeliveryZip = "48213", DeliveryCity = "London", EmailAddress = "Jane_h77@gmail.com", PhoneNo = "0701245512" },
                new Customer { FirstName = "Peter", LastName = "Birro", BillingAddress = "12 Fox Street", BillingZip = "45681", BillingCity = "New York", DeliveryAddress = "89 Moose Plaza", DeliveryZip = "45821", DeliveryCity = "Seattle", EmailAddress = "peter_the_great@hotmail.com", PhoneNo = "0739484322" }
            );
        }
    }
}
