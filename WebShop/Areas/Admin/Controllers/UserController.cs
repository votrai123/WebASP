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
    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaping(page, pageSize);
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(User user)
        {
            var dao = new UserDao();
            var encryptedPw = Encrytor.MD5Hash(user.Password);
            user.Password = encryptedPw;
            dao.Insert(user);
            return Json(new { Messeage = "SUCCESS", JsonRequestBehavior.AllowGet });
        }

    }
}