using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcMovie.Models;
using File = MvcMovie.Models.File;
using MvcMovie.Helpers;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        // GET: Movies
        public ActionResult Index(string movieGenre, string searchString)
        {

            var GenreLst = new List<string>();

            var GenreQry = from d in db.Movies
                orderby d.Genre
                select d.Genre;

            GenreLst.AddRange(GenreQry.Distinct());
            ViewBag.movieGenre = new SelectList(GenreLst);

            var movies = from m in db.Movies.Include(i => i.Images)
                select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            //TODO: Pass in viewmodel instead of movies could add get poster, get images etc. to viewModel or as 
            return View(movies);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,ReleaseDate,Genre,Price,Rating")] Movie movie, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();

                if (upload != null && upload.ContentLength > 0)
                {
                    
                    var posterImage = new File
                    {
                        // rename before storing 
                        FileName = Guid.NewGuid() + Path.GetExtension(upload.FileName),
                        FileType = FileType.Poster,
                        ContentType = upload.ContentType,
                    };

                    //Should move away from hardcoding path against model since Filename will be enough to find relative path
                    string filePath = string.Format("~/Content/public/{0}/{1}", movie.ID, posterImage.FileName);
                    posterImage.FilePath = filePath;


                    string fileStreamPath = Server.MapPath(filePath);
                    string directoryPath = Server.MapPath("~/Content/public/" + movie.ID + "/");
                    if (!Directory.Exists(directoryPath))  
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                    //save to disk
                    using (var fileStream = System.IO.File.Create(fileStreamPath))
                    {
                        upload.InputStream.Seek(0, SeekOrigin.Begin);
                        upload.InputStream.CopyTo(fileStream);
                    }

                    movie.Images = new List<File> { posterImage };
                }

                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Include(s => s.Images).SingleOrDefault(s => s.ID == id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,ReleaseDate,Genre,Price, Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            Movie movie = db.Movies
                .Include(x => x.Reviews)
                .Include(x => x.Images)
                .SingleOrDefault(x => x.ID == id);

            if (movie?.Reviews != null && movie.Reviews.Any())
            {
                var reviews = movie.Reviews.ToList();
                foreach (Review review in reviews)
                {
                    movie.Reviews.Remove(review);
                }
            }

            if (movie?.Images != null && movie.Images.Any())
            {
                var images = movie.Images.ToList();
                foreach (File image in images)
                {
                    movie.Images.Remove(image);
                }
                string target_dir = string.Format("~/Content/public/{0}/", movie.ID);
                string directoryPath = Server.MapPath(target_dir);

                DeleteHelpers.DeleteDirectory(directoryPath);
            }

            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
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