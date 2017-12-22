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
            //frontPage.MostPopular =

            FrontPageView frontPage = new FrontPageView();

            frontPage.MostPopular = (from m in db.Movies
                                    join orows in db.OrderRows on m.Id equals orows.MovieId
                                    orderby db.OrderRows.Count() descending
                                    select m).Distinct().Take(5).ToList();

            frontPage.Newest = db.Movies.OrderBy(m => m.ReleaseYear).Take(5).ToList();
            frontPage.Oldest = db.Movies.OrderByDescending(m => m.ReleaseYear).Take(5).ToList();
            frontPage.Cheapest = db.Movies.OrderByDescending(m => m.Price).Take(5).ToList();

            return View(frontPage);

            //db.OrderRows.Sum(o => o.Price);

            //double maxOrderPrice = 0;
            //foreach (var order in db.Orders)
            //{
            //    double tempPrice = 0;
            //    foreach (var orderRow in db.OrderRows)
            //    {
            //        if (orderRow.OrderId == order.Id)
            //        {
            //            tempPrice += orderRow.Price;
            //        }
            //    }
            //    if (tempPrice >= maxOrderPrice)
            //    {
            //        maxOrderPrice = tempPrice;
            //        frontPage.BestCustomer = db.Customers.Find(order.CustomerId);
            //    }
            //}
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