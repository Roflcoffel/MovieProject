using MovieProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult CreateMovie([Bind(Include = "Id,Title,Director,ReleaseYear,Price")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }


    }
}