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

            //List<Movie> tempList = db.Movies.ToList();
            //for (int i = 0; i < 5; i++)
            //{
            //    int tempBiggest = 0;
            //    int tempId = 0;
            //    foreach (var movie in tempList)
            //    {
            //        int temp = db.OrderRows.Count(r => r.MovieId == movie.Id);
            //        if (temp > tempBiggest)
            //        {
            //            tempId = movie.Id;
            //            tempBiggest = temp;
            //        }
            //    }
            //    tempList.Remove(db.Movies.Find(tempId));
            //    Movie tempMovie = db.Movies.Find(tempId);
            //    frontPage.MostPopular.Add(tempMovie);
            //}

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
            var query = db.Movies.ToList();

            return View(query);
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
                    ViewBag.Message = "Permission Denied";
                    return RedirectToAction("Index", "Home");
                }
            } 
            else
            {
                ViewBag.Message = "Not Logged in";
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