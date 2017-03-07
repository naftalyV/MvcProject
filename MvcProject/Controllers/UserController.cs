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
        public ActionResult Index()
        {
            return View();
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