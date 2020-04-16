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
    public class SlideController : Controller
    {
        // GET: Admin/Slide
        public ActionResult Index(int page=1,int pageSize=5)
        {
            var dao = new SlideDao();
            var model = dao.ListAllPaping( page, pageSize);
            return View(model);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Slide slide, HttpPostedFileBase file)
        {
            try
            {

                if (file.ContentLength > 0 && ModelState.IsValid)
                {

                    var dao = new SlideDao();
                    slide.Status = true;
                    slide.CreatedDate = DateTime.Now;
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("/Assets/Client/images"), _FileName);
                    slide.Image = "/Assets/Client/images/"+ _FileName;
                    long id = dao.Insert(slide);
                    if (id > 0)
                    {
                        file.SaveAs(_path);
                        ViewBag.Message = "File Uploaded Successfully!!";
                        ModelState.AddModelError("", "Them  thanh cong");
                        return RedirectToAction("Index", "Slide");

                    }
                    else
                    {
                        ModelState.AddModelError("", "Them khong thanh cong");
                    }

                }
                return View("Create", "Slide");



            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View("Create");
            }
        }
        public ActionResult Delete(int id)
        {
            new SlideDao().Delete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            var slide = new SlideDao().ViewDetail(id);
            return View(slide);
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult Update(Slide slide, HttpPostedFileBase file)
        {

            if (ModelState.IsValid)
            {
                var dao = new SlideDao();

                slide.ModifiedDate = DateTime.Now;
             
                if (file != null)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Assets/Client/images"), _FileName);
                    slide.Image = "/Assets/Client/images/" + _FileName;
                    file.SaveAs(_path);
                }


                var result = dao.Update(slide);
                if (result)
                {
                    ViewBag.Message = "File Uploaded Successfully!!";
                    ModelState.AddModelError("", "Cap nhat  thanh cong");
                    return RedirectToAction("Index", "Slide");

                }
                else
                {
                    ModelState.AddModelError("", "Cap nhat khong thanh cong");
                }

            }
            return View("Index", "Slide");



        }

    }
}