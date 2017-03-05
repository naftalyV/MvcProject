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

        [Authorize]
        [HttpPost]
        public ActionResult SubmitData(Prodoct p)
        {
            HttpFileCollectionWrapper wrapper = HttpContext.Request.Files as HttpFileCollectionWrapper;
            int length = wrapper.Count;
            for (int i = 0; i < length; i++)
            {
                if (wrapper[i] != null && wrapper[i].ContentLength > 0 && wrapper[i].ContentType.StartsWith("image"))
                {
                    switch (i)
                    {
                        case 0:
                            p.picture1 = GetByteArray(wrapper[i]);
                            break;
                        case 1:
                            p.picture2 = GetByteArray(wrapper[i]);
                            break;
                        case 2:
                            p.picture3 = GetByteArray(wrapper[i]);
                            break;
                        default:
                            break;
                    }

                    using (var ctx = new BuyForUDB())
                    {

                        ctx.Prodoct.Add(p);
                        ctx.SaveChanges();
                        ViewBag.Message = "File uploaded successfully";
                    }

                }

            }
            return View();

            //if (file != null && file.ContentLength > 0 && file.ContentType.StartsWith("image"))
            //{
            // extract only the fielname
            //var fileName = Path.GetFileName(file.FileName);
            // store the file inside ~/Images folder
            //var path = Path.Combine(Server.MapPath("~/Images"), fileName);
            //file.SaveAs(path);
            //TODO:..
            //p.picture1 = GetByteArray(file);
            //p.Date = DateTime.Now;
            //  p.Price = 9;
            //  p.Title = "hi";
            //  p.Id = 99;


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
