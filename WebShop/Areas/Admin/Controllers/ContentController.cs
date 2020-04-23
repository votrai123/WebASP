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
    public class ContentController : Controller
    {
        // GET: Admin/Content
        public ActionResult Index(string searchString, int page = 1, int pageSize = 8)
        {
            var dao = new ContentDao();
            var model = dao.ListAllPaping(searchString, page, pageSize);
            ViewBag.searchString = searchString;
            return View(model);
        }
        public ActionResult Create()
        {
            var dao = new ContentDao();
            ViewBag.Category = dao.ListByGroupStatus(true);
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Content content, HttpPostedFileBase file)
        {
            try
            {

                if (file.ContentLength > 0 && ModelState.IsValid)
                {

                    var dao = new ContentDao();
                    var convert = ConvertTxt.utf8Convert3(content.Name);
                    content.MetaTitle = convert;
                    content.Status = true;
                    content.ViewCount = 0;
                    content.TopHot = DateTime.Now;
                    content.CreatedDate = DateTime.Now;
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Assets/Client/images"), _FileName);
                    content.Image = "/Assets/Client/images/" + _FileName;
                    long id = dao.Insert(content);
                    if (id > 0)
                    {
                        file.SaveAs(_path);
                        ViewBag.Message = "File Uploaded Successfully!!";
                        ModelState.AddModelError("", "Them  thanh cong");
                        return RedirectToAction("Index", "Content");

                    }
                    else
                    {
                        ModelState.AddModelError("", "Them khong thanh cong");
                    }

                }
                return View("Index", "Content");



            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View("Create");
            }
        }
        public ActionResult Delete(int id)
        {
            new ContentDao().Delete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            var dao = new ContentDao();
            ViewBag.Category = dao.ListByGroupStatus(true);

            var content = new ContentDao().ViewDetail(id);
            return View(content);
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult Update(Content content, HttpPostedFileBase file)
        {

            if (ModelState.IsValid)
            {
                var dao = new ContentDao();

                content.ModifiedDate = DateTime.Now;
                if (!string.IsNullOrEmpty(content.Name))
                {
                    var convert = ConvertTxt.utf8Convert3(content.Name);
                    content.MetaTitle = convert;
                }

                if (file != null)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Assets/Client/images"), _FileName);
                    content.Image = "/Assets/Client/images/" + _FileName;
                    file.SaveAs(_path);
                }


                var result = dao.Update(content);
                if (result)
                {
                    ViewBag.Message = "File Uploaded Successfully!!";
                    ModelState.AddModelError("", "Cap nhat  thanh cong");
                    return RedirectToAction("Index", "Content");

                }
                else
                {
                    ModelState.AddModelError("", "Cap nhat khong thanh cong");
                }

            }
            return View("Index", "Content");



        }

    }
}
