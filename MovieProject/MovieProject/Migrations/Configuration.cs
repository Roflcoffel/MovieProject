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
            Movie mov1 = new Movie { Title = "Interstellar", Director = "Christoper Nolan", ReleaseYear = 2014, Price = 180, Url = "/Content/Image/Interstellar_film_poster.jpg" };
            Movie mov2 = new Movie { Title = "Hobbit: Battle of the five armies", Director = "Peter Jackson", ReleaseYear = 2014, Price = 179, Url = "/Content/Image/hobbit.jpg" };
            Movie mov3 = new Movie { Title = "The Wolf of Wall Street", Director = "Martin Scorcese", ReleaseYear = 2013, Price = 110, Url = "/Content/Image/wolf.jpg" };
            Movie mov4 = new Movie { Title = "Pulp Fiction", Director = "Quentin Tarantino", ReleaseYear = 1994, Price = 49, Url = "/Content/Image/pulpfiction.jpg" };

            context.Movies.AddOrUpdate(m => m.Id, mov1, mov2, mov3, mov4);

            Customer cus1 = new Customer { FirstName = "Jonas", LastName = "Gray", BillingAddress = "23 Green Corner Street", BillingZip = "56743", BillingCity = "Birmingham", DeliveryAddress = "23 Green Corner Street", DeliveryZip = "56743", DeliveryCity = "Birmingham", EmailAddress = "Jonas.gray@hotmail.com", PhoneNo = "0708123456" };
            Customer cus2 = new Customer { FirstName = "Jane", LastName = "Harolds", BillingAddress = "10 West Street", BillingZip = "48213", BillingCity = "London", DeliveryAddress = "10 West Street", DeliveryZip = "48213", DeliveryCity = "London", EmailAddress = "Jane_h77@gmail.com", PhoneNo = "0701245512" };
            Customer cus3 = new Customer { FirstName = "Peter", LastName = "Birro", BillingAddress = "12 Fox Street", BillingZip = "45681", BillingCity = "New York", DeliveryAddress = "89 Moose Plaza", DeliveryZip = "45821", DeliveryCity = "Seattle", EmailAddress = "peter_the_great@hotmail.com", PhoneNo = "0739484322" };

            context.Customers.AddOrUpdate(p => p.Id, cus1, cus2, cus3);

            Order ord1 = new Order { Customer = cus1, OrderDate = new DateTime(2015, 1, 2) };
            Order ord2 = new Order { Customer = cus2, OrderDate = new DateTime(2013, 5, 5) };
            Order ord3 = new Order { Customer = cus3, OrderDate = new DateTime(2011, 9, 2) };

            context.Orders.AddOrUpdate(o => o.Id, ord1, ord2, ord3);

            OrderRow row1 = new OrderRow { Movie = mov1, Order = ord1, Price = 300 };
            OrderRow row2 = new OrderRow { Movie = mov2, Order = ord2, Price = 100 };
            OrderRow row3 = new OrderRow { Movie = mov2, Order = ord3, Price = 400 };
            OrderRow row4 = new OrderRow { Movie = mov1, Order = ord2, Price = 300 };
            OrderRow row5 = new OrderRow { Movie = mov2, Order = ord2, Price = 100 };
            OrderRow row6 = new OrderRow { Movie = mov3, Order = ord3, Price = 400 };

            context.OrderRows.AddOrUpdate(o => o.Id, row1, row2, row3, row4, row5, row6);
        }
    }
}
