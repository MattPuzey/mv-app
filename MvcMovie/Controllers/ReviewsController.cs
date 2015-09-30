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
        public ActionResult Index(int? id)
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
                MovieReviews = movie.Reviews.ToList()
            };
            return View(viewModel);
        }

        public ActionResult Create(int? id)
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
                Review = new Review(),
                MovieReviews = movie.Reviews.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReviewViewModel model)
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
            return RedirectToAction("Index", new { Id = model.MovieId });
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var review = db.Reviews
                .Include(x => x.Movie)
                .SingleOrDefault(x => x.Id == id);

            var viewModel = new ReviewViewModel
            {
                MovieId = review.Movie.ID,
                MovieTitle = review.Movie.Title,
                Review = review,
                MovieReviews = review.Movie.Reviews.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MovieId, MovieTitle, Review, MovieReviews")] ReviewViewModel model)
        {
            if (ModelState.IsValid)
            {
                var review = model.Review;
                var movieId = model.MovieId;
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { Id = movieId });
            }
            var viewModel = model;
            return View(viewModel);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var review = db.Reviews
                .Include(x => x.Movie)
                .SingleOrDefault(x => x.Id == id);

            var viewModel = new ReviewViewModel
            {
                MovieId = review.Movie.ID,
                MovieTitle = review.Movie.Title,
                Review = review,
                MovieReviews = review.Movie.Reviews.ToList()
            };
            return View(viewModel);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var review = db.Reviews
                .Include(x => x.Movie)
                .SingleOrDefault(x => x.Id == id);
            
            var movieId = review.Movie.ID;

            db.Reviews.Remove(review);
            db.SaveChanges();

            return RedirectToAction("Index", new { Id = movieId });
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