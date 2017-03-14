﻿using MvcProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProject.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult HomePage()
        {
            var list = new List<Product>();
            using (var ctx = new BuyForUDB())
            {
                list = ctx.Product.Where(p => p.picture1 != null).ToList();

                return View(list);
            }
        }
             public ActionResult ShowInHomePage(int id)
        {
            using (var ctx = new BuyForUDB())
            {

                var imageData = ctx.Product.Where(p => p.Id == id && p.picture1 != null).FirstOrDefault();

                return File(imageData.picture1, "image/jpg");
            }
        }
        public ActionResult OrderByName()
        {
            List<Product> list;
            using (var ctx = new BuyForUDB())
            {
                list = ctx.Product.Where(p => p.Status == State.ForSale).OrderBy(p => p.Title).ToList();
                return View("HomePage", list);
            }
        }

        public ActionResult OrderByDate()
        {
            List<Product> list;
            using (var ctx = new BuyForUDB())
            {
                list = ctx.Product.Where(p => p.Status == State.ForSale).OrderBy(p => p.Date).ToList();
                return View("HomePage", list);
            }
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult MoreDedails(int id)
        {
           // var list = new List<Product>();
            using (var ctx = new BuyForUDB())
            {
              var  list = ctx.Product.Where(p => p.Id == id).FirstOrDefault();

                return View(list);
            }
        }
    }
}
