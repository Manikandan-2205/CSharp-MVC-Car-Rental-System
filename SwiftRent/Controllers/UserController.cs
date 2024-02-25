using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SwiftRent.Models;

namespace SwiftRent.Controllers
{
    public class UserController : Controller
    {
        SwiftRentEntities db = new SwiftRentEntities();
        // GET: Userticket
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserDetail user)
        {
            if (ModelState.IsValid)
            {
                bool IsValid = db.UserDetails.Any(u => u.Username == user.Username && u.Password == user.Password);
                if (IsValid)
                {
                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "User Name or Password Invalid";
                    return View();
                }
            }
            return View();
        }

        public ActionResult Register()
        {
            ViewBag.RoleId = new SelectList(db.RoleDetails, "RoleId", "RoleName");
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserDetail user)
        {
            if (ModelState.IsValid)
            {
                ViewBag.RoleId = new SelectList(db.RoleDetails, "RoleId", "RoleName");
                db.UserDetails.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}