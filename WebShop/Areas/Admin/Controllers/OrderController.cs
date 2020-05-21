using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebShop.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        // GET: Admin/Order
        public ActionResult Index(string searchString, int page = 1, int pageSize = 6)
        {
            var dao = new OrderDao();
            var model = dao.ListAllPaping(searchString, page, pageSize);
            ViewBag.searchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Update(int id)
        {

            var order = new OrderDao().ViewDetail(id);
            return View(order);
        }
        [HttpPost]
        public ActionResult Update(Order order)
        {
            if (ModelState.IsValid)
            {
                new OrderDao().Update(order);
                return RedirectToAction("Index", "Order");
            }
            return View("Update");
        }
        public ActionResult Detail(long id)
        {
            var dao = new OrderDao().ListByOrderID(id);
            ViewBag.Product = new OrderDao().ListAllProduct();
            return View(dao);
        }
        public ActionResult Delete(int id)
        {
            new OrderDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}