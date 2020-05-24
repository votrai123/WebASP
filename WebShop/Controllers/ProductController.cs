using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Common;

namespace WebShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Category(long CateId, int page = 1, int pageSize = 9)
        {
            var category = new ProductDao().ViewDetailCategory(CateId);
            if (category != null)
            {
                ViewBag.Category = category;
            }
            else
            {
                var productcategory = new ProductDao().ViewDetailProductCategory(CateId);

                ViewBag.ProductCategory = productcategory;
            }
            int totalRecord = 0;
            var model = new ProductDao().ListByCategoryId(CateId, ref totalRecord, page, pageSize);
            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            int maxPage = 5;
            int totalPage = 0;
            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = maxPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            var ListCategory = new ProductDao().ListByGroupStatus(true);
            ViewBag.ListCategory = ListCategory;
            var ListProductCategory = new ProductDao().ListProductCategoryByGroupStatus(true);
            ViewBag.ListProductCategory = ListProductCategory;
            return View(model);
        }

        public ActionResult Detail(long id)
        {
            var product = new ProductDao().ViewDetail(id);
            new ProductDao().CountView(id);
            ViewBag.CountComment = new ProductDao().CountCommentById(id);
            ViewBag.Comment = new ProductDao().ListComment(id);
            ViewBag.Category = new ProductDao().ViewDetailProductCategory(product.CategoryID.Value);
            return View(product);
        }
        [ChildActionOnly]
        public ActionResult ListCategory()
        {
            var model = new ProductDao().ListByGroupStatus(true);
            ViewBag.ListCategory = model;
            return PartialView(model);
        }
        public JsonResult Comment(long IDUser, long IDProduct, string Content)
        {
            var comment = new CommentProduct();
            comment.IDUser = IDUser;
            comment.IDProduct = IDProduct;
            comment.Content = Content;
            comment.CreatedDate = DateTime.Now;
            var id = new CommentProductDao().Insert(comment);
            if (id > 0)
            {

                return Json(new { status = true });
            }
            else
            {
                return Json(new { status = false });
            }
        }
    }
}