using MvcProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcProject.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
       
        public ActionResult Index()
        {
            //using (var ctx = new BuyForUDB()) { var p = ctx.Prodoct.ToList(); }
               
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
                }

                return RedirectToAction("Index");
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");

        }
        [HttpPost]
        public ActionResult SubmitData(Prodoct p, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                string path = Path.Combine(Server.MapPath("~/Images"),
                                         Path.GetFileName(file.FileName));
                file.SaveAs(path);
                ViewBag.Message = "File uploaded successfully";



                byte[] data = GetByteArray(file);
                Server.MapPath("");
            }
            return null;
        }
              private static byte[] GetByteArray(HttpPostedFileBase file)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                file.InputStream.CopyTo(ms);
                byte[] array = ms.GetBuffer();
                return array;
            }
        }

        private void SaveInFileSystem(HttpPostedFileBase file)
        {
            string pic = System.IO.Path.GetFileName(file.FileName);
            string path = System.IO.Path.Combine(
                                   Server.MapPath("~/images/"), pic);

            file.SaveAs(path);
        }
    }
}
