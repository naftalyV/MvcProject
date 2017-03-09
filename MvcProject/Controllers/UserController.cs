using MvcProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcProject.Controllers
{
    public class UserController : Controller
    {
        
        // GET: User

        public ActionResult Login(User user)
        {

            using (var ctx = new BuyForUDB())
            {

                var userDtails = ctx.Users.Where
                    (u => u.UserName == user.UserName
                    && u.Password == user.Password)
                    .FirstOrDefault();

                if (userDtails != null)
                {
                    FormsAuthentication.SetAuthCookie($"{userDtails.FirstName} {userDtails.LastNama}", true);
                   
                    return RedirectToAction("HomePage","Home");
                }

                else
                {
                    return View("HomePage");
                }
            }
        }
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("HomePage", "Home");

        }
        public ActionResult NewUser()
        {
            return View("AddUser");
        }

        [HttpPost]
        public ActionResult SubmitUser(User u)
        {
            if (u.UserName != null && u.Password != null)
            {
                using (var ctx = new BuyForUDB())
                {
                    if (!User.Identity.IsAuthenticated)
                    {
                        var eu = ctx.Users.Where(User => User.UserName == u.UserName).FirstOrDefault();
                        if (eu==null)
                        {
                            ctx.Users.Add(u);
                            ctx.SaveChanges();
                            ViewBag.Message = "פרטי משתמש נקלטו בהצלחה";
                            return RedirectToAction("HomePage", "Home");
                        }
                        else
                        {
                            ViewBag.massege = "שם משתמש כבר קיים";
                            return View("AddUser");

                        }
                    }
                }
            }
            return View("AddUser");

        }
    }
}