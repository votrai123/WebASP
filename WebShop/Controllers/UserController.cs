using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Models;
using Common;
using BotDetect.Web.Mvc;

namespace WebShop.Controllers
{
    public class UserController : Controller
    {
        // GET: User

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [CaptchaValidationActionFilter("CaptchaCode", "registerCaptcha", "Wrong Captcha!")]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                if (dao.CheckUserName(model.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else if (dao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    var user = new User();
                    user.FullName = model.FullName;
                    user.Password = Common.Encrytor.MD5Hash(model.Password);
                    user.Phone = model.Phone;
                    user.Email = model.Email;
                    user.UserName = model.UserName;
                    user.StreetAddress = model.StreetAddress;
                    user.Country = model.Country;
                    user.CreatedDate = DateTime.Now;
                    user.Status = true;
                    user.role = false;

                    var result = dao.Insert(user);
                    if (result > 0)
                    {
                        return RedirectToAction("Register", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("","Đăng ký không thành công");
                    }
                }
            }
            return View(model);
        }
    }
}