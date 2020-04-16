using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Slides = new SlideDao().ListAll();
            var productDao = new ProductDao();
            ViewBag.NewProducts = productDao.ListNewProduct(8);
            return View();
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            var model = new CategoryDao().ListByGroupStatus(true); ;
            return PartialView(model);
        }
    }
}