using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace MvcMovie.Models
{
    public class Review
    {
        public int Id { get; set; }
        [StringLength(1000)]
        public string Text { get; set; }
    }
}