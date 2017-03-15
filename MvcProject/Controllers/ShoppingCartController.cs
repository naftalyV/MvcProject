using MvcProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace MvcProject.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        [HttpGet]
        public ActionResult Cart()

        {
            var ItemToCart = new List<Product>();
            using (var ctx = new BuyForUDB())
            {
                ItemToCart = ctx.Product.Where(p => p.Status == State.ShoppingCart).ToList();

            }
            return View(ItemToCart);
        }
        [HttpPost]
        public ActionResult AddToCart(int id)
        {
            Product item;
            using (var ctx = new BuyForUDB())
            {
                item = ctx.Product.Include(p => p.User).Where(p => p.Id == id).FirstOrDefault();
                User user = ctx.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                if (user != null)
                {
                    item.User = ctx.Users.Where(u => u.Id == user.Id).FirstOrDefault();
                    ctx.Users.Attach(item.User);
                }
               
                item.Status = State.ShoppingCart;
            }
            return View(item);
        }
    }
}


