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
    public class UserController : BaseController
    {

        // GET: Admin/User
        [Route("abc")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 8)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaping(searchString, page, pageSize);
            ViewBag.searchString = searchString;
            return View(model);
        }

        [HttpGet]
        [Route("Create")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {

                var dao = new UserDao();
                var encryptedPw = Encrytor.MD5Hash(user.Password);
                user.Password = encryptedPw;
                user.role = false;
                user.Status = true;
                user.CreatedDate = DateTime.Now;
                long id = dao.Insert(user);
                if (id > 0)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Them user khong thanh cong");
                }
            }
            return View("Create");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult Update(User user)
        {
            if (ModelState.IsValid)
            {

                var dao = new UserDao();
                if (!string.IsNullOrEmpty(user.Password))
                {
                    var encryptedPw = Encrytor.MD5Hash(user.Password);
                    user.Password = encryptedPw;
                }

                user.ModifiedDate = DateTime.Now;
                var result = dao.Update(user);
                if (result)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cap nhat user khong thanh cong");
                }
            }
            return View("Update");
        }
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}