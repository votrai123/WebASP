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
        public ActionResult Index(int page = 1, int pageSize = 8)
        {
            var dao = new ContentDao();
            var model = dao.ListAllPaping(page, pageSize);
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Create (Content content, HttpPostedFileBase file)
        {
            try
            {

                if (file.ContentLength > 0 && ModelState.IsValid)
                {
                    var dao = new ContentDao();
                    content.Status = true;
                    content.ViewCount = 0;
                    content.TopHot = DateTime.Now;
                    content.CreatedDate = DateTime.Now;
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Areas/Admin/UploadedFiles"), _FileName);
                    content.Image = _path; 
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
    }
}