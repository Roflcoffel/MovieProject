using MovieProject.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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

        public ActionResult Browse(string search, int? page)
        {
            ViewBag.Message = TempData["Message"];

            var allMovies = db.Movies.ToList();

            //Search
            if (!String.IsNullOrEmpty(search))
            {
                allMovies = allMovies.Where(s => s.Title.Contains(search)).ToList();

                if (allMovies.Count() == 0)
                {
                    ViewBag.ErrorMessage = "Doesn't exist!";
                }
            }

            var pageNumber = page ?? 1;
            var onePageOfMovies = allMovies.ToPagedList(pageNumber, 5);
            
            return View(onePageOfMovies);
        }

        public ActionResult OverView()
        {
            ViewBag.Message = TempData["Message"];

            return View(db.Movies.ToList());
        }


        [HttpPost]
        public ActionResult OverView(string searchString)
        {

            var movies = from m in db.Movies
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
                   
                if(movies.Count() == 0)
                {
                    ViewBag.ErrorMessage = "Doesn't exist!";
                }
            }
           
           
            return View(movies);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

                        string extension = Path.GetExtension(filename);
                        if (extension == ".jpg" || extension == ".png")
                        {
                            file.SaveAs(path);
                            movie.Url = "/Content/Image/" + filename;
                            movie.Rating = 0;
                            db.Movies.Add(movie);
                            db.SaveChanges();

                            TempData["Message"] = "File Uploaded";
                        }
                        else
                        {
                            TempData["Message"] = "Invalid File Format!";

                            return RedirectToAction("OverView");
                        }
                       
                    }
                }
                catch
                {
                    TempData["Message"] = "File Upload Failed!!";
                    return RedirectToAction("OverView");
                }

                return RedirectToAction("OverView");
                
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
        public ActionResult EditMovie(int id, Movie movie)
        {
            if (Request.Files.Count > 0)
            {
                if(Request.Files[0].ContentLength > 0)
                {
                    string filename = Path.GetFileName(Request.Files[0].FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/Image"), filename);

                    string extension = Path.GetExtension(filename);

                    if(extension == ".jpg" || extension == ".png")
                    {
                        Request.Files[0].SaveAs(path);
                        db.Movies.Find(id).Url = "/Content/Image/" + filename;
                        TempData["Message"] = "File Uploaded";
                    }
                    else
                    {
                        TempData["Message"] = "Invalid File Format!";

                        return RedirectToAction("OverView");
                    }

                }
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


        // Search Movie

        public ActionResult SearchMovie(string searchString)
        {
            var movies = from m in db.Movies
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            return RedirectToAction("OverView","Home",movies.ToList());
        }

        public ActionResult MovieInfo(int? Id)
        {
            return View(db.Movies.Find(Id));
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