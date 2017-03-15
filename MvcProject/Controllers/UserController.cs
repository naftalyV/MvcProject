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
                //string str = "!!!שדות שם משתמש וסיסמא הינם שדות חובה";
                //return RedirectToAction("HomePage", "Home", new { Massege = str });
                return View();
                //return RedirectToAction("HomePage", "Home");

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
                        string str = "שם המשתמש או הסיסמא לא נכונים ";
                        return RedirectToAction("HomePage", "Home", new { Massege = str });
                        // return View("Login");
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
            if  (ModelState.IsValid)
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
                        ViewBag.Massege = "העדכון הצליח";
                        return RedirectToAction("HomePage", "Home");
                    }
                }
            }
            return View("");

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
                    return View(new User());
                }
            }
               
         
        }
    }
}