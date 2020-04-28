using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Models;
using System.Web.Script.Serialization;
using Model.EF;

namespace WebShop.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[Common.CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        public ActionResult AddItem(long productId, int quantity)
        {
            var product = new ProductDao().ViewDetail(productId);
            var cart = Session[Common.CommonConstants.CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.ID == productId))
                {

                    foreach (var item in list)
                    {
                        if (item.Product.ID == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    //tạo mới đối tượng cart item
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    var promotion = product.Prrice - product.Promotion;
                    item.Discount = quantity * (decimal)promotion;
                    item.SubTotal = quantity * (decimal)product.Prrice;

                    list.Add(item);
                }
                //Gán vào session
                Session[Common.CommonConstants.CartSession] = list;
            }
            else
            {
                //tạo mới đối tượng cart item
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var promotion = product.Prrice - product.Promotion;
                item.Discount = (decimal)promotion;
                item.SubTotal = (decimal)product.Prrice;
                var list = new List<CartItem>();
                list.Add(item);
                //Gán vào session
                Session[Common.CommonConstants.CartSession] = list;
            }
            return RedirectToAction("Index");
        }

        public JsonResult DeleteAll()
        {
            Session[Common.CommonConstants.CartSession] = null;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[Common.CommonConstants.CartSession];
            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[Common.CommonConstants.CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Update(string CartModel)
        {
            var jsoncart = new JavaScriptSerializer().Deserialize<List<CartItem>>(CartModel);
            var sessioncart = (List<CartItem>)Session[Common.CommonConstants.CartSession];

            foreach (var item in sessioncart)
            {
                var JsonItem = jsoncart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (JsonItem != null)
                {
                    item.Quantity = JsonItem.Quantity;
                }
            }
            Session[Common.CommonConstants.CartSession] = sessioncart;
            return Json(new
            {
                status = true
            });
        }

        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[Common.CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Payment(string FullName, string Email, string StreetAddress, string Country, string Phone, String Note)
        {
            try
            {
                var order = new Order();
                order.FullName = FullName;
                order.CreatedDate = DateTime.Now;
                order.Country = Country;
                order.Email = Email;
                order.Note = Note;
                order.Phone = Phone;
                order.StreetAddress = StreetAddress;
                order.Status = 1;

                var id = new OrderDao().Insert(order);
                var cart = (List<CartItem>)Session[Common.CommonConstants.CartSession];
                var detaildao = new OrderDetailDao();
                foreach (var item in cart)
                {
                    var orderdetail = new OrderDetail();
                    orderdetail.ProductID = item.Product.ID;
                    orderdetail.CreateDate = DateTime.Now;
                    orderdetail.OrderID = id;
                    orderdetail.Price = (item.SubTotal - item.Discount) * item.Quantity;
                    orderdetail.Quantity = item.Quantity;
                    detaildao.Insert(orderdetail);
                }
                Session[Common.CommonConstants.CartSession] = null;
                return Json(new
                {
                    status = true
                });
            }
            catch
            {
                return Json(new
                {
                    status = false
                });
            }
            
        }
        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Fail()
        {
            return View();
        }
    }
}