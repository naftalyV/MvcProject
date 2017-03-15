using MvcProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity.Migrations;
namespace MvcProject.Controllers
{
    public class UserController : Controller
    {

        // GET: User
        [HttpPost]
        public ActionResult Login(User user)
        {
            if (user.UserName == null || user.Password == null)
            {
                // return View("HomePage", "Home");
               // return View();
                return RedirectToAction("HomePage", "Home",user);
                //return View("HomePage", "");
            }
            else
            {

                using (var ctx = new BuyForUDB())
                {

                    var userDtails = ctx.Users.Where
                        (u => u.UserName == user.UserName
                        && u.Password == user.Password)
                        .FirstOrDefault();

                    if (userDtails != null)

                    {
                        //FormsAuthentication.SetAuthCookie($"{userDtails.FirstName} {userDtails.LastNama}", true);
                        FormsAuthentication.SetAuthCookie($"{userDtails.UserName}", true);


                        return RedirectToAction("HomePage", "Home");
                    }

                    else
                    {
                        ViewBag.Messeg = "שם לא קיים";
                        return RedirectToAction("HomePage", "Home");
                        // return View("ShowInHomePage");
                    }
                }
            }
        }
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("HomePage", "Home");

        }
       

        [HttpPost]
        public ActionResult EditUser(User u)
        {
            //if  //(ModelState.IsValid)
            {
                using (var ctx = new BuyForUDB())
                {
                    if (!User.Identity.IsAuthenticated)
                    {
                        var ExistsUser = ctx.Users.Where(User => User.UserName == u.UserName).FirstOrDefault();
                        if (ExistsUser == null)
                        {
                            ctx.Users.Add(u);
                            ctx.SaveChanges();
                            ViewBag.Message = "פרטי משתמש נקלטו בהצלחה";
                            return RedirectToAction("HomePage", "Home");
                        }
                        else
                        {
                            ViewBag.Massege = "שם משתמש כבר קיים";
                            return View("EditUser");

                        }
                    }
                    else
                    {
                        ctx.Users.AddOrUpdate(u);
                        ctx.SaveChanges();
                        return RedirectToAction("HomePage", "Home");
                    }
                }
            }
          //  return View("");

        }
        public ActionResult EditUser()
        {
            using (var ctx = new BuyForUDB())
            {
                var user = ctx.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
                if (user!=null)
                {
            return View("EditUser", user);

                }
                else
                {
                    return View();
                }
            }
               
         
        }
    }
}