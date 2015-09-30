using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class ReviewsController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        // GET: Reviews
        public ActionResult ReviewsIndex(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies
                .Include(x => x.Reviews)
                .SingleOrDefault(x => x.ID == id);

            var viewModel = new ReviewViewModel
            {
                MovieId = movie.ID,
                MovieTitle = movie.Title,
                //Review = new Review(),
                MovieReviews = movie.Reviews
            };
            return View(viewModel);
        }

        public ActionResult CreateReview(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Movie movie = db.Movies.Find(id);
            Movie movie = db.Movies
                .Include(x => x.Reviews)
                .SingleOrDefault(x => x.ID == id);
                

            var viewModel = new ReviewViewModel
            {
                MovieId = movie.ID,
                MovieTitle = movie.Title,
                Review = new Review(),
                MovieReviews = movie.Reviews 
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateReview(ReviewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var review = model.Review;

            Movie movie = db.Movies
                .Include(x => x.Reviews)
                .FirstOrDefault(x => x.ID == model.MovieId);

            if (movie != null)
            {
       
                movie.Reviews.Add(review);
                db.SaveChanges();
            }
            return RedirectToAction("ReviewsIndex");
        }

        public ActionResult EditReview(int id)
        {
            throw new NotImplementedException();
        }

        public ActionResult DeleteReview(int id)
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}