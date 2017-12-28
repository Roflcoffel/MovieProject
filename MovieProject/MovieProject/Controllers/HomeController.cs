using MovieProject.Models;
using System;
using System.Collections.Generic;
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

        public ActionResult Index()
        {
            FrontPageView frontPage = new FrontPageView();

            ViewBag.Message = TempData["Message"];

            frontPage.MostPopular = db.Movies.OrderByDescending(m => m.OrderRows.Count()).Take(5).ToList();

            frontPage.Newest = db.Movies.OrderByDescending(m => m.ReleaseYear).Take(5).ToList();
            frontPage.Oldest = db.Movies.OrderBy(m => m.ReleaseYear).Take(5).ToList();
            frontPage.Cheapest = db.Movies.OrderBy(m => m.Price).Take(5).ToList();

            double maxOrderPrice = 0;
            List<Order> orders = db.Orders.ToList();
            foreach (var order in orders)
            {
                double tempPrice = 0;
                foreach (var orderRow in db.OrderRows)
                {
                    if (orderRow.OrderId == order.Id)
                    {
                        tempPrice += orderRow.Price;
                    }
                }
                if (tempPrice >= maxOrderPrice)
                {
                    maxOrderPrice = tempPrice;
                    frontPage.BestCustomer = db.Customers.Find(order.CustomerId);
                }
            }
            return View(frontPage);
        }

        public ActionResult Browse()
        {
            ViewBag.Message = TempData["Message"];

            var query = db.Movies.ToList();

            return View(query);
        }

        public ActionResult OverView()
        {
            List<Movie> movies = db.Movies.ToList();
            return View(movies);
        }

        // Create
        public ActionResult CreateMovie()
        {
            var user = (User)Session["User"];

            if(user != null)
            {
                if (user.isAdmin)
                {
                    return View();
                }
                else
                {
                    TempData["Message"] = "Permission Denied";
                    return RedirectToAction("Index", "Home");
                }
            } 
            else
            {
                TempData["Message"] = "Not Logged in";
                return RedirectToAction("Login", "Account");
            }
            
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

            return RedirectToAction("Index");
        }

        public ActionResult EditMovie(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMovie(int id, Movie movie, HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string filename = Path.GetFileName(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/Image"), filename);
                    file.SaveAs(path);
                    db.Movies.Find(id).Url = "/Content/Image/" + filename;
                }
                ViewBag.Message = "File Uploaded";

            }
            catch
            {
                ViewBag.Message = "File Upload Failed!!";
                return View();
            }
            db.Movies.Find(id).Title = movie.Title;
            db.Movies.Find(id).Director = movie.Director;
            db.Movies.Find(id).ReleaseYear = movie.ReleaseYear;
            db.Movies.Find(id).Price = movie.Price;
            db.SaveChanges();
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