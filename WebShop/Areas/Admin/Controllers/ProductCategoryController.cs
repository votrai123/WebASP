using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Model.EF;
using Model.Dao;
using WebShop.Common;

namespace WebShop.Areas.Admin.Controllers
{
    public class ProductCategoryController : Controller
    {
        // GET: Admin/ProductCategory
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new ProductCategoryDao();
            var model = dao.ListAllPaping(searchString, page, pageSize);
            ViewBag.searchString = searchString;

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var dao = new CategoryDao();
            ViewBag.Category = dao.ListByGroupStatus(true);
            return View();
        }
        [HttpPost]
        public ActionResult Create(ProductCategory categoryp)
        {
            if (ModelState.IsValid)
            {

                var dao = new ProductCategoryDao();
                categoryp.ShowOnHome = true;
                var convert = ConvertTxt.utf8Convert3(categoryp.Name);
                categoryp.MetaTitle = convert;
                categoryp.Status = true;
                categoryp.CreatedDate = DateTime.Now;
                long id = dao.Insert(categoryp);
                if (id > 0)
                {
                    return RedirectToAction("Index", "ProductCategory");
                }
                else
                {
                    ModelState.AddModelError("", "Them khong thanh cong");
                }
            }
            return View("Create");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var dao = new CategoryDao();
            ViewBag.Category = dao.ListByGroupStatus(true);
            var productcategory = new ProductCategoryDao().ViewDetail(id);
            return View(productcategory);
        }
        [HttpPost]
        public ActionResult Update(ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {

                var dao = new ProductCategoryDao();
                if (!string.IsNullOrEmpty(productCategory.Name))
                {
                    var convert = ConvertTxt.utf8Convert3(productCategory.Name);
                    productCategory.MetaTitle = convert;
                }

                productCategory.ModifiedDate = DateTime.Now;
                var result = dao.Update(productCategory);
                if (result)
                {
                    return RedirectToAction("Index", "ProductCategory");
                }
                else
                {
                    ModelState.AddModelError("", "Cap nhat khong thanh cong");
                }
            }
            return View("Update");
        }
        public ActionResult Delete(int id)
        {
            new ProductCategoryDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}