using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.Dao;
using WebShop.Common;

namespace WebShop.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index(string searchString, int page =1, int pageSize =5)
        {
            var dao = new CategoryDao();
            var model = dao.ListAllPaping(searchString, page, pageSize);
            ViewBag.searchString = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {

                var dao = new CategoryDao();
                category.ShowOnHome = true;
                var convert = ConvertTxt.utf8Convert3(category.Name);
                category.MetaTitle = convert;
                category.Status = true;
                category.CreatedDate = DateTime.Now;
                long id = dao.Insert(category);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ModelState.AddModelError("", "Them category khong thanh cong");
                }
            }
            return View("Create");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var category = new CategoryDao().ViewDetail(id);
            return View(category);
        }
        [HttpPost]
        public ActionResult Update(Category category)
        {
            if (ModelState.IsValid)
            {

                var dao = new CategoryDao();
                if (!string.IsNullOrEmpty(category.Name))
                {
                    var convert = ConvertTxt.utf8Convert3(category.Name);
                    category.MetaTitle = convert;
                }

                category.ModifiedDate = DateTime.Now;
                var result = dao.Update(category);
                if (result)
                {
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ModelState.AddModelError("", "Cap nhat category khong thanh cong");
                }
            }
            return View("Update");
        }
        public ActionResult Delete(int id)
        {
            new CategoryDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}