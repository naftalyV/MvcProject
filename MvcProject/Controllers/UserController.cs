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
        public static User LoggedUser = null;

        // GET: User
        public ActionResult Index()
        {
            var lst = new List<Product>();
            using (var ctx = new BuyForUDB())
            {
                //lst = ctx.Prodoct.Where(p => p.Price < 100000).ToList();
                //return View(lst);
                return View(lst);
            }
        }
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
                    LoggedUser = user;
                    return RedirectToAction("Index");
                }

                else
                {
                    return View("Index");
                }
            }
        }
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");

        }
        public ActionResult NewUser()
        {
            return View("AddUser");
        }

        [HttpPost]
        public ActionResult SubmitUser(User U)
        {
            if (U.UserName != null && U.Password != null)
            {
                using (var ctx = new BuyForUDB())
                {

                    ctx.Users.Add(U);
                    ctx.SaveChanges();
                    ViewBag.Message = "פרטי משתמש נקלטו בהצלחה";
                    return RedirectToAction("Index");
                }
            }
            return View("AddUser");
        }
    }
}