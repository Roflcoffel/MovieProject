using MovieProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MovieProject.Controllers
{
    public class HomeController : Controller
    {

        private MovieContext db = new MovieContext();

        // Get Movie

        public ActionResult Index(FrontPageView frontPage)
        {
            //frontPage.MostPopular =
            frontPage.Newest = db.Movies.OrderBy(m => m.ReleaseYear).Take(5).ToList();
            frontPage.Oldest = db.Movies.OrderByDescending(m => m.ReleaseYear).Take(5).ToList();
            frontPage.Cheapest = db.Movies.OrderByDescending(m => m.Price).Take(5).ToList();
            return View(frontPage);
        }

        public ActionResult Browse()
        {
            var query = db.Movies.ToList();

            return View(query);
        }

        // Create
        public ActionResult CreateMovie()
        {
            return View();
        }

        // Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMovie(HttpPostedFileBase file, Movie movie)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (file.ContentLength > 0)
                    {
                        string filename = Path.GetFileName(file.FileName);
                        string path = Path.Combine(Server.MapPath("~/Content/Image"), filename);
                        file.SaveAs(path);
                        movie.Url = "/Content/Image/" + filename;
                    }
                    ViewBag.Message = "File Uploaded";

                }
                catch
                {
                    ViewBag.Message = "File Upload Failed!!";
                    return View();
                }

                db.Movies.Add(movie);
                db.SaveChanges();

                return View();
                
            }

            return RedirectToAction("OverView");
        }


        // Delete 
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
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("OverView");
        }


        // View Data to Edit/Details/Delete

        public ActionResult OverView()
        {
            return View(db.Movies.ToList());

        }




        // GET: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMovie(HttpPostedFileBase file, Movie movie)
        {

            if (ModelState.IsValid)
            {
                //var oldMovie = db.Movies.Find(movie.Id);
                //db.Movies.Remove(oldMovie);

                //db.Movies.Whe
                try
                {
                    if (file.ContentLength > 0)
                    {
                        string filename = Path.GetFileName(file.FileName);
                        string path = Path.Combine(Server.MapPath("~/Content/Image"), filename);
                        file.SaveAs(path);
                        movie.Url = "/Content/Image/" + filename;
                    }
                    ViewBag.Message = "File Uploaded";

                }
                catch
                {
                    ViewBag.Message = "File Upload Failed!!";
                    return View();
                }

                //oldMovie = movie;
                db.Movies.Add(movie);
                db.SaveChanges();

                return View();

            }

            return View();
        }


        //public ActionResult EditMovie(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Movie movie = db.Movies.Find(id);
        //    if (movie == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(movie);
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMovie( Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("OverView");
            }
            return View(movie);
        }

    }

}

