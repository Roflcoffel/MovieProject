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
            context.Movies.AddOrUpdate(m => m,
                new Movie { Title = "TEST", Director = "TEST", Price = 1, ReleaseYear = 2014},
                new Movie { Title = ""},
                new Movie { Title = ""},
                new Movie { Title = ""}
                );

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
