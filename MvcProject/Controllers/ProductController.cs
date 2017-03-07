using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProject.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult SubmitData(Product p)
        {
            if (ModelState.IsValid)
            //p.Title != string.Empty
            //&& p.ShortDescription != string.Empty
            //&& p.LongDescription != string.Empty
            //&& p.Price > 0)
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

                            p.Date = DateTime.Now;
                            ctx.Prodoct.Add(p);
                            ctx.SaveChanges();
                            ViewBag.Message = "File uploaded successfully";
                        }

                    }

                }
                return RedirectToAction("Index");


            }
            return View("AddProduct");
        }
    }
}