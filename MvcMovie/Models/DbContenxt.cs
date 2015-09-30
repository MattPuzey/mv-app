using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcMovie.Models
{
    public class MovieDBContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        //ToDo:Correct typo 
        public DbSet<Review> Reviews { get; set; }

        public MovieDBContext()
        {
            Configuration.LazyLoadingEnabled = false;
        }
    }
}