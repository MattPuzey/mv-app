using System.Collections.Generic;
using MvcMovie.Models;

namespace MvcMovie.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MvcMovie.Models.MovieDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MvcMovie.Models.MovieDBContext";
        }

        protected override void Seed(MvcMovie.Models.MovieDBContext context)
        {
            context.Movies.AddOrUpdate(i => i.Title,

                 new Movie
                {
                    Title = "Ant Man",
                    ReleaseDate = DateTime.Parse("2015-7-17"),
                    Genre = "Action",
                    Rating = "PG",
                    Price = 8.99M, 
                },

                new Movie
                {
                    Title = "The Theory of Everything",
                    ReleaseDate = DateTime.Parse("2015-1-2"),
                    Genre = "Biographical Drama",
                    Rating = "PG",
                    Price = 9.99M,
                },

                new Movie
                {
                    Title = "Birdman",
                    ReleaseDate = DateTime.Parse("2015-1-2"),
                    Genre = "Black Comedy",
                    Rating = "R",
                    Price = 3.99M,
                },
                new Movie
                {
                    Title = "Avengers: Age of Ultron",
                    ReleaseDate = DateTime.Parse("2015-5-23"),
                    Genre = "Action",
                    Rating = "PG",
                    Price = 7.99M,
                },

                new Movie
                {
                    Title = "Jurassic World",
                    ReleaseDate = DateTime.Parse("2015-5-29"),
                    Genre = "Science Fiction",
                    Rating = "PG",
                    Price = 7.99M,
                },

                 new Movie
                 {
                     Title = "The Legend of Barney Thomson",
                     ReleaseDate = DateTime.Parse("2015-7-24"),
                     Genre = "Black Comedy",
                     Rating = "R",
                     Price = 7.99M,
                 },

                 new Movie
                 {
                     Title = "Mission Impossible - Rogue Nation",
                     ReleaseDate = DateTime.Parse("2015-7-30"),
                     Genre = "Action",
                     Rating = "R",
                     Price = 3.99M,
                 },

                  new Movie
                  {
                      Title = "Legend",
                      ReleaseDate = DateTime.Parse("2015-9-9"),
                      Genre = "Crime Thriller",
                      Rating = "R",
                      Price = 8.99M,
                  }
            );
        }
    }
}
