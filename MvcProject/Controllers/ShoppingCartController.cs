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
                ItemToCart = ctx.Product.Where(p => p.Status == State.ShoppingCart &&
                p.User.UserName == User.Identity.Name).ToList();

            }
            return View(ItemToCart);
        }
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
                item.IsInCart = true;
                ctx.SaveChanges();
            }
            return RedirectToAction("HomePage", "Home");
        }
        public decimal RemoveFromCart(int id)
        {
            Product item;
            using (var ctx = new BuyForUDB())
            {
                item = ctx.Product.Include(p => p.User).Where(p => p.Id == id).FirstOrDefault();
                item.UserId = null;
                item.Status = State.ForSale;
                item.IsInCart = false;
                ctx.SaveChanges();
            }
            return item.Price;
        }
        public ActionResult Sale()
        {
            var ItemToSale = new List<Product>();
            using (var ctx = new BuyForUDB())
            {

                ItemToSale = ctx.Product.Where(p => p.Status == State.ShoppingCart &&
                                p.User.UserName == User.Identity.Name).ToList();
                foreach (var item in ItemToSale)
                {
                    item.Status = State.Sold;
                }
                ctx.SaveChanges();
                ViewBag.Massege1 = "תודה על קניתך";
                return RedirectToAction("HomePage", "Home");
            }
        }
    }
}

