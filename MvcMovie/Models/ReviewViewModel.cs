using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcMovie.Migrations;

namespace MvcMovie.Models
{
    public class ReviewViewModel
    {
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public Review Review { get; set; }
        public List<Review> MovieReviews { get; set; }
    }
}