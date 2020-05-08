using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Common;

namespace WebShop.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        public ActionResult Index(string searchString, int page = 1, int pageSize = 6)
        {
            var dao = new ProductDao();
            var model = dao.ListAllPaping(searchString, page, pageSize);
            ViewBag.searchString = searchString;
            var categorydao = new CategoryDao();
            ViewBag.ListCategory = categorydao.ListByGroupStatus(true);
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var dao = new ProductDao();
            //ViewBag.ProductCategory = dao.ListByGroupStatus(true);
            ViewBag.Category = dao.ListByGroupStatus(true);
            return View();
        }
        public JsonResult GetProductCategory(int id)
        {
            var dao = new ProductDao().ListCategory(id);
            return Json(dao, JsonRequestBehavior.AllowGet);

        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var dao = new ProductDao();
                    var convert = ConvertTxt.utf8Convert3(product.Name);
                    product.MetaTitle = convert;
                    product.Status = true;
                    product.ViewCount = 0;
                    product.TopHot = DateTime.Now;
                    product.CreatedDate = DateTime.Now;
                    if (file.ContentLength > 0)
                    {

                        string _FileName = Path.GetFileName(file.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Assets/Client/images"), _FileName);
                        product.Image = "/Assets/Client/images/" + _FileName;

                        long id = dao.Insert(product);
                        if (id > 0)
                        {
                            file.SaveAs(_path);
                            ViewBag.Message = "File Uploaded Successfully!!";
                            ModelState.AddModelError("", "Them  thanh cong");
                            return RedirectToAction("Index", "Product");

                        }
                        else
                        {
                            ModelState.AddModelError("", "Them khong thanh cong");
                        }
                    }

                }
                return RedirectToAction("Create", "Product");

            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return RedirectToAction("Create", "Product");

            }
        }
        public ActionResult Delete(int id)
        {
            new ProductDao().Delete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            var dao = new ProductDao();
            var product = dao.ViewDetail(id);
            //ViewBag.ProductCategory = dao.ListByGroupStatus(true);
            ViewBag.Category = new CategoryDao().ListByGroupStatus(true);

            return View(product);
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult Update(Product product, HttpPostedFileBase file)
        {

            if (ModelState.IsValid)
            {
                var dao = new ProductDao();

                product.ModifiedDate = DateTime.Now;
                if (!string.IsNullOrEmpty(product.Name))
                {
                    var convert = ConvertTxt.utf8Convert3(product.Name);
                    product.MetaTitle = convert;
                }

                if (file != null)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Assets/Client/images"), _FileName);
                    product.Image = "/Assets/Client/images/" + _FileName;
                    file.SaveAs(_path);
                }


                var result = dao.Update(product);
                if (result)
                {
                    ViewBag.Message = "File Uploaded Successfully!!";
                    ModelState.AddModelError("", "Cap nhat  thanh cong");
                    return RedirectToAction("Index", "Product");

                }
                else
                {
                    ModelState.AddModelError("", "Cap nhat khong thanh cong");
                    return View("Update", "Product");

                }

            }
            return View("Update", "Product");



        }

    }
}