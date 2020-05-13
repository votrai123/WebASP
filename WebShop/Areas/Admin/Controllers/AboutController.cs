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
    public class AboutController : BaseController
    {
        // GET: Admin/About
        public ActionResult Index(string searchString, int page = 1, int pageSize = 4)
        {
            var dao = new AboutDao();
            var model = dao.ListAllPaping(searchString, page, pageSize);
            ViewBag.searchString = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(About about, HttpPostedFileBase file)
        {
            try
            {

                if (file.ContentLength > 0 && ModelState.IsValid)
                {

                    var dao = new AboutDao();
                    var convert = ConvertTxt.utf8Convert3(about.Name);
                    about.MetaTitle = convert;
                    about.Status = true;
                    about.CreatedDate = DateTime.Now;
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Assets/Client/images"), _FileName);
                    about.Image = "/Assets/Client/images/" + _FileName;
                    long id = dao.Insert(about);
                    if (id > 0)
                    {
                        file.SaveAs(_path);
                        ViewBag.Message = "File Uploaded Successfully!!";
                        ModelState.AddModelError("", "Them  thanh cong");
                        return RedirectToAction("Index", "About");

                    }
                    else
                    {
                        ModelState.AddModelError("", "Them khong thanh cong");
                    }

                }
                return View("Create");



            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View("Create");
            }
        }
        public ActionResult Delete(int id)
        {
            new AboutDao().Delete(id);
            return RedirectToAction("Index");
        }
        public ActionResult Update(int id)
        {
            var dao = new AboutDao();

            var about = new AboutDao().ViewDetail(id);
            return View(about);
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult Update(About about, HttpPostedFileBase file)
        {

            if (ModelState.IsValid)
            {
                var dao = new AboutDao();

                about.ModifiedDate = DateTime.Now;
                if (!string.IsNullOrEmpty(about.Name))
                {
                    var convert = ConvertTxt.utf8Convert3(about.Name);
                    about.MetaTitle = convert;
                }

                if (file != null)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Assets/Client/images"), _FileName);
                    about.Image = "/Assets/Client/images/" + _FileName;
                    file.SaveAs(_path);
                }


                var result = dao.Update(about);
                if (result)
                {
                    ViewBag.Message = "File Uploaded Successfully!!";
                    ModelState.AddModelError("", "Cap nhat  thanh cong");
                    return RedirectToAction("Index", "About");

                }
                else
                {
                    ModelState.AddModelError("", "Cap nhat khong thanh cong");
                }

            }
            return View("Update");



        }
    }
}