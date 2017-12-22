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
        
        //Account Page
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            try
            {
                if (user != null)
                {
                         
                    var encodingPasswordString = Encrypt.EncodePassword(user.Password, user.HashCode);
                        
                    //Check Login Detail User Name Or Password    
                    var query = (from s in db.Users
                                 where (s.Username == user.Username) && s.Password.Equals(encodingPasswordString)
                                 select s).FirstOrDefault();

                    if (query != null)
                    {
                        //Update User
                        user.isLoggedIn = true;
                        db.SaveChanges();
                        return RedirectToAction("Account", "Index");
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
                    var keyNew = Encrypt.GeneratePassword(10);
                    var password = Encrypt.EncodePassword(newUser.Password, keyNew);

                    newUser.Password = password;
                    newUser.isAdmin = true;
                    newUser.CustomerId = 1;
                    newUser.HashCode = keyNew;
                    newUser.isLoggedIn = false;

                    db.Users.Add(newUser);
                    db.SaveChanges();
                    ModelState.Clear();
                    ViewBag.Message = "Account Created";

                    return View();
                }
                ViewBag.ErrorMessage = "User Allredy Exixts!!!!!!!!!!";
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