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