using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMovie.Models
{
    public class ReviewCreateModel
    {
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public Review Review { get; set; }
    }
}