using Model.EF;
using Model.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductDao
    {
        WebShopDbContext db = null;
        public ProductDao()
        {
            db = new WebShopDbContext();
        }

        public List<ProductViewModel> ListNewProduct(int top)
        {
            var model = from a in db.Products
                        join b in db.ProductCategories
                        on a.CategoryID equals b.ID
                        select new ProductViewModel()
                        {
                            ID = a.ID,
                            Name = a.Name,
                            MetaTitle = a.MetaTitle,
                            CateName = b.Name,
                            CreatedDate = a.CreatedDate,
                            Status = a.Status,
                            Code = a.Code,
                            Description = a.Description,
                            Detail = a.Detail,
                            Image = a.Image,
                            Promotion = a.Promotion,
                            Prrice = a.Prrice,
                            Quantity = a.Quantity,
                            TopHot = a.TopHot,
                            ViewCount = a.ViewCount


                        };
            return model.OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }
        public IEnumerable<ProductViewModel> ListAllPaping(string searchString, int page, int pageSize)
        {
            IQueryable<ProductViewModel> model = from a in db.Products
                                                 join b in db.ProductCategories
                                                 on a.CategoryID equals b.ID
                                                 select new ProductViewModel()
                                                 {
                                                     ID = a.ID,
                                                     Name = a.Name,
                                                     MetaTitle = a.MetaTitle,
                                                     CateName = b.Name,
                                                     CreatedDate = a.CreatedDate,
                                                     Status = a.Status,
                                                     Code = a.Code,
                                                     Description = a.Description,
                                                     Detail = a.Detail,
                                                     Image = a.Image,
                                                     Promotion = a.Promotion,
                                                     Prrice = a.Prrice,
                                                     Quantity = a.Quantity,
                                                     TopHot = a.TopHot,
                                                     ViewCount = a.ViewCount


                                                 };
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.CateName.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public List<ProductViewModel> ListByCategoryId(long categoryID, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            var join = from a in db.Products
                       join b in db.ProductCategories
                       on a.CategoryID equals b.ID
                       select new ProductViewModel()
                       {
                           ID = a.ID,
                           Name = a.Name,
                           MetaTitle = a.MetaTitle,
                           CateName = b.Name,
                           CreatedDate = a.CreatedDate,
                           Status = a.Status,
                           Code = a.Code,
                           Description = a.Description,
                           Detail = a.Detail,
                           Image = a.Image,
                           Promotion = a.Promotion,
                           Prrice = a.Prrice,
                           Quantity = a.Quantity,
                           TopHot = a.TopHot,
                           ViewCount = a.ViewCount,
                           CategoryID = a.CategoryID

                       };
            totalRecord = join.Where(x => x.CategoryID == categoryID).Count();
            var model = join.Where(x => x.CategoryID == categoryID).OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return model;
        }
        public long Insert(Product entity)
        {
            db.Products.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Delete(int id)
        {
            try
            {
                var product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Product ViewDetail(long id)
        {
            return db.Products.Find(id);
        }
        public bool Update(Product entity)
        {
            try
            {
                var product = db.Products.Find(entity.ID);
                product.Name = entity.Name;
                if (!string.IsNullOrEmpty(entity.Image))
                {
                    product.Image = entity.Image;

                }
                if (entity.MetaTitle != null)
                {
                    product.MetaTitle = entity.MetaTitle;
                }
                product.Status = entity.Status;
                if (entity.CategoryID != null)
                {
                    product.CategoryID = entity.CategoryID;
                }
                product.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public List<Category> ListByGroupStatus(bool status)
        {
            return db.Categories.Where(x => x.Status == status).ToList();
        }
        public List<ProductCategory> ListCategory(int id)
        {
            return db.ProductCategories.Where(x => x.ParentID == id).ToList();
        }
    }
}
