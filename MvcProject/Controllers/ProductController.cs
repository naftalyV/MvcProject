﻿using MvcProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProject.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        [Authorize]
        public ActionResult SubmitData()
        {
            return View("AddProduct");
        }

        [Authorize]
        [HttpPost]
        public ActionResult SubmitData(Product p)
        {
                
            
                HttpFileCollectionWrapper wrapper = HttpContext.Request.Files as HttpFileCollectionWrapper;
                int length = wrapper.Count;

                p.picture1 = GetByteArray(wrapper[0]);
                p.picture2 = GetByteArray(wrapper[1]);
                p.picture3 = GetByteArray(wrapper[2]);
            //if// (ModelState.IsValid)
            //   (p.Title != string.Empty
            //    && p.ShortDescription != string.Empty
            //    && p.LongDescription != string.Empty
            //    && p.Price > 0
            //   && p.picture1!=null)
            { 
                using ( var ctx = new BuyForUDB())
                {
                    User user = ctx.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                    if (user != null)
                    {
                        p.Owner = ctx.Users.Where(u => u.Id == user.Id).FirstOrDefault();
                        ctx.Users.Attach(p.Owner);
                        p.Date = DateTime.Now;
                    ctx.Product.Add(p);
                    ctx.SaveChanges();
                    ViewBag.Message = "File uploaded successfully";
                }

                return RedirectToAction("HomePage", "Home");


            }
            return View("AddProduct");
        }
        private static byte[] GetByteArray(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0 && file.ContentType.StartsWith("image"))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                    return array;
                }
            }
            return null;
        }

        //[HttpPost]
        //public ActionResult ShowInHomePage(int id)
        //{
        //    using (var ctx = new BuyForUDB())
        //    {

        //        var imageData = ctx.Product.Where(p => p.Id == id && p.picture1!=null).FirstOrDefault();

        //        return File(imageData.picture1, "image/jpg");
        //    }
        //}
    }
}