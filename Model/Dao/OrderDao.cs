using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using Model.ViewModel;
using PagedList;

namespace Model.Dao
{
    public class OrderDao
    {
        WebShopDbContext db = null;
        public OrderDao()
        {
            db = new WebShopDbContext();
        }
        public long Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.ID;
        }

        public IEnumerable<Order> ListAllPaping(string searchString, int page, int pageSize)
        {
            IQueryable<Order> model = db.Orders;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ID.ToString().Contains(searchString) || x.Email.Contains(searchString) || x.Phone.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public bool Update(Order entity)
        {
            try
            {
                var order = db.Orders.Find(entity.ID);
                order.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public Order ViewDetail(int id)
        {
            return db.Orders.Find(id);
        }
        public List<OrderViewModel> ListByOrderID(long OrderID)
        {
            var join = from a in db.OrderDetails
                       join b in db.Products
                       on a.ProductID equals b.ID
                       select new OrderViewModel()
                       {
                           ProductID = a.ProductID,
                           ImageProduct = b.Image,
                           NameProduct = b.Name,
                           OrderCreatedDate = a.CreateDate,
                           OrderID = a.OrderID,
                           OrderPrice = a.Price,
                           OrderQuantity = a.Quantity
                       };
            var model = join.Where(x => x.OrderID == OrderID).OrderByDescending(x => x.OrderCreatedDate).ToList();
            return model;
        }

        public List<Product> ListAllProduct()
        {
            var model = db.Products.ToList();
            return model;
        }
        public bool Delete(int id)
        {

            var orderdetail = db.OrderDetails.ToList();
            foreach (var item in orderdetail)
            {
                if (item.OrderID == id)
                {
                    db.OrderDetails.Remove(item);
                }
            }
            var order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return true;

        }
    }
}
