using MovieProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieProject.Controllers
{
    public class CartController : Controller
    {
        private MovieContext db = new MovieContext();
        
        // GET: Shopping Cart
        public ActionResult Index()
        {
            //Cross Controller Messages
            ViewBag.Message = TempData["Message"];

            List<Movie> sessionCart = (List<Movie>)Session["Cart"];

            Dictionary<int, int> tempCart = new Dictionary<int, int>();
            Dictionary<Movie, int> cart = new Dictionary<Movie, int>();

            if (sessionCart == null)
            {
                sessionCart = new List<Movie>();
            }
            
           
            if (sessionCart != null)
            {
                List<Movie> sessionCartUnique = sessionCart.Distinct(new MovieComparer()).ToList();

                foreach (var item in sessionCartUnique)
                {
                    tempCart.Add(item.Id, 0);
                }

                foreach (var item in sessionCart)
                {
                    var count = sessionCart.Where(x => x.Id == item.Id).Count();

                    tempCart[item.Id] = count;
                }

                //Convert Dict <int, int> to <Movie, int>
                foreach (var item in tempCart)
                {
                    cart.Add(db.Movies.Find(item.Key), item.Value);
                }

                return View(cart);
            }
            else
            {
                TempData["Message"] = "Cart Expired";
                return RedirectToAction("Index", "Home");
            }
           
        }

        //Cart Stuff
        public ActionResult AddToCart(int? id)
        {
            List<Movie> shoppingCart = (List<Movie>)Session["Cart"];

            if(shoppingCart == null)
            {
                shoppingCart = new List<Movie>();
            }

            var movie = db.Movies.Find(id);
            if (movie != null)
            {
                TempData["Message"] = "Movie Added";
                shoppingCart.Add(movie);
                Session["Cart"] = shoppingCart;
            }
            else
            {
                TempData["Message"] = "Movie Doesnt Exist";
            }
            

            return RedirectToAction("Browse", "Home");
        }

        public ActionResult RemoveFromCart(int? id)
        {
            List<Movie> shoppingCart = (List<Movie>)Session["Cart"];

            if (shoppingCart == null)
            {
                shoppingCart = new List<Movie>();
            }

            var movie = shoppingCart.Where(m => m.Id == id).Take(1).Single();
            if (movie != null)
            {
                TempData["Message"] = "Movie Removed";
                shoppingCart.Remove(movie);
                Session["Cart"] = shoppingCart;
            }
            else
            {
                TempData["Message"] = "Movie Doesnt Exist";
                RedirectToAction("Browse", "Home");
            }

            return RedirectToAction("Index", "Cart");
        }

        public ActionResult CheckOut()
        {
            List<Movie> shoppingCart = (List<Movie>)Session["Cart"];

            if(shoppingCart == null)
            {
                TempData["Message"] = "Cart Empty";
                return RedirectToAction("Browse", "Home");
            }

            var user = (User)Session["User"];
            if(user != null)
            {

                CheckOutVM chkVM = new CheckOutVM();

                chkVM.Customer = user.Customer;
                chkVM.ShoppingCart = shoppingCart;
                chkVM.TotalCost = (int)shoppingCart.Sum(x => x.Price);

                return View(chkVM);

            }

            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult CheckOut(CheckOutVM chkVM)
        {

            //Create Order
            Order order = new Order
            {
                CustomerId = chkVM.Customer.Id,
                OrderDate = DateTime.Today
            };

            db.Orders.Add(order);
            db.SaveChanges();

            foreach (var row in chkVM.ShoppingCart)
            {
                OrderRow orows = new OrderRow
                {
                    Order = order,
                    MovieId = row.Id,
                    Price = row.Price
                };
                db.OrderRows.Add(orows);
                db.SaveChanges();
            }

            TempData["Message"] = "Order Placed";

            Session["Cart"] = null;

            return RedirectToAction("Index","Home");
        }
    }
}