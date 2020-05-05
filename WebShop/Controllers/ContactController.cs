using Common;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebShop.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Send(string Email,string FullName,string Phone,string Note)
        {
            var feedback = new Feedback();
            feedback.Email = Email;
            feedback.Content = Note;
            feedback.Name = FullName;
            feedback.Phone = Phone;
            feedback.Status = true;
            feedback.CreatedDate = DateTime.Now;
            var id = new FeedbackDao().Insert(feedback);
            if (id > 0)
            {
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/Client/template/feedback.html"));

                content = content.Replace("{{name}}", FullName);
                content = content.Replace("{{phone}}", Phone);


                new MailHelper().SendMail(Email, "Phản hồi từ khách hàng"+FullName, content);
                return Json(new { status = true });
            }
            else
            {
                return Json(new { status = false });
            }
        }
    }
}