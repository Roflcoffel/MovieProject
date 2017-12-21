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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Browse()
        {
            var query = db.Movies.ToList();

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