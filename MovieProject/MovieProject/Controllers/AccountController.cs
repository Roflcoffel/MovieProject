using MovieProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Text;

namespace MovieProject.Controllers
{
    public class AccountController : Controller
    {
        private MovieContext db = new MovieContext();

        public ActionResult Index()
        {
            var user = (User)Session["User"];
            if (user.isLoggedIn)
            {
                return View(user.Customer);
            }
            else
            {
                TempData["Message"] = "Session Expired";
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            TempData["Message"] = "You Logged out";
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ShowOrders()
        {
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = TempData["Message"];

            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            try
            {
                if (user != null)
                {
                    var dbUser = db.Users.Where(u => u.Username == user.Username).FirstOrDefault();
                    //Get the Encrypted Password
                    string encodedPassword = Encrypt.EncodePassword(user.Password, dbUser.HashCode);

                    //Check if the encrypted input matches what is stored in the db.    
                    var query = db.Users.Where(u => u.Password.Equals(encodedPassword)).FirstOrDefault();

                    if (query != null)
                    {
                        //Start Session
                        dbUser.isLoggedIn = true;
                        db.SaveChanges();
                        Session["User"] = dbUser;
                        return RedirectToAction("Index", "Account");
                    }

                    ViewBag.ErrorMessage = "Invallid User Name or Password";
                    return View();
                }
                ViewBag.ErrorMessage = "Invallid User Name or Password";
                return View();
                
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = "Error!!!" + e;
                return View();
            }
        }

        public ActionResult Registration()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Registration(User newUser)
        {
            try
            {
                
                var chkUser = (from u in db.Users where u.Username == newUser.Username select u).FirstOrDefault();

                if (chkUser == null)
                {
                    var HashKey = Encrypt.GeneratePassword(10);
                    var password = Encrypt.EncodePassword(newUser.Password, HashKey);

                    newUser.Password = password;
                    newUser.isAdmin = false;
                    newUser.Customer = new Customer {
                        FirstName = "Temp",
                        LastName = "Temp",
                        BillingAddress = "Temp",
                        BillingCity = "Temp",
                        BillingZip = "Temp",
                        DeliveryAddress = "Temp",
                        DeliveryCity = "Temp",
                        DeliveryZip = "Temp",
                        EmailAddress = "Temp",
                        PhoneNo = "Temp",
                    };
                    //newUser.CustomerId = db.Customers.OrderByDescending(c => c.Id).Take(1).Single().Id;
                    newUser.HashCode = HashKey;
                    newUser.isLoggedIn = false;

                    db.Users.Add(newUser);
                    db.SaveChanges();
                    ModelState.Clear();
                    ViewBag.Message = "Account Created";

                    return View();
                }
                ViewBag.ErrorMessage = "User Allredy Exixts!";
                return View();
                
            }
            catch (DbEntityValidationException e)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in e.EntityValidationErrors)
                {
                    sb.Append(item + " errors: ");
                    foreach (var i in item.ValidationErrors)
                    {
                        sb.Append(i.PropertyName + " : " + i.ErrorMessage);
                    }
                    sb.Append(Environment.NewLine);
                }
                ViewBag.ErrorMessage = "Validation Error: " + sb.ToString();
                return View();
            }
        }
    }
}