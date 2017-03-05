using MvcProject.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            if (p.Title != string.Empty
                && p.ShortDescription != string.Empty
                && p.LongDescription != string.Empty
                && p.Price > 0)
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
                            FormsAuthentication.CookiesSupported.ToString();
                            p.Date = DateTime.Now;
                            ctx.Prodoct.Add(p);
                            ctx.SaveChanges();
                            ViewBag.Message = "File uploaded successfully";
                        }

                    }

                }
                return RedirectToAction("Index");


            }return View("AddProduct");
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
        //[HttpPost]
        //public ActionResult Show(int id)
        //{
        //    //using (var ctx = new BuyForUDB())
        //    //{

        //    //    var imageData = ctx.Prodoct.Where(p => p.Id == id).FirstOrDefault();
        //    //    using (MemoryStream ms = new MemoryStream())
        //    //    {
        //    //       imageData.picture1.(ms);
        //    //        byte[] array = ms.GetBuffer();
        //    //        return array;
        //    //    }
        //    //    return File(imageData, "image/jpg");
        //    //}
        //}
       
    }
}
                

