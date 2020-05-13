using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebShop.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult Index(int page = 1, int pageSize = 5)
        {
            var model = new ContentDao().ListAllPaging(page, pageSize);
            int totalRecord = 0;
            ViewBag.Tags = new ContentDao().ListAllTag();
            ViewBag.Categorys = new ContentDao().ListByGroupStatusCategory(true);
            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(model);
        }
        public ActionResult Detail(long id)
        {
            var model = new ContentDao().GetByID(id);
            new ContentDao().CountView(id);
            ViewBag.Categorys = new ContentDao().ListByGroupStatusCategory(true);
            ViewBag.TagAll = new ContentDao().ListAllTag();
            ViewBag.Tags = new ContentDao().ListTag(id);
            return View(model);
        }

        public ActionResult Tag(string tagId, int page = 1, int pageSize = 10)
        {
            var model = new ContentDao().ListAllByTag(tagId, page, pageSize);
            int totalRecord = 0;
            ViewBag.TagAll = new ContentDao().ListAllTag();
            ViewBag.Categorys = new ContentDao().ListByGroupStatusCategory(true);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            ViewBag.Tag = new ContentDao().GetTag(tagId);
            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(model);
        }
        public ActionResult ListAllByCategory(long ID, int page = 1, int pageSize = 10)
        {
            var model = new ContentDao().ListAllByCategory(ID, page, pageSize);
            int totalRecord = 0;
            ViewBag.TagAll = new ContentDao().ListAllTag();
            ViewBag.Categorys = new ContentDao().ListByGroupStatusCategory(true);
            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            ViewBag.ViewDetailCategory = new ContentDao().ViewDetailCategory(ID);
            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            return View(model);
        }
    }
}