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

        public List<CommentViewModel> ListComment(long idproduct)
        {
            IQueryable<CommentViewModel> model = from a in db.CommentProducts
                                                 join b in db.Users
                                                 on a.IDUser equals b.ID
                                                 select new CommentViewModel()
                                                 {
                                                     ID = a.ID,
                                                     IDUser = a.IDUser,
                                                     CommentCreatedDate = a.CreatedDate,
                                                     Content = a.Content,
                                                     FullName = b.FullName,
                                                     IDContent = a.IDProduct
                                                 };
            return model.OrderByDescending(x => x.CommentCreatedDate).Where(x => x.IDContent == idproduct).ToList();
        }
        public List<ThreeViewModel> ListByCategoryId(long categoryID, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            var join = from a in db.Products
                       join b in db.ProductCategories
                       on a.CategoryID equals b.ID
                       join c in db.Categories
                       on b.ParentID equals c.ID
                       select new ThreeViewModel()
                       {
                           IDCategory = c.ID,
                           CreatedDate = a.CreatedDate,
                           Description = a.Description,
                           Detail = a.Detail,
                           IDProduct = a.ID,
                           IDProductCategory = b.ID,
                           Image = a.Image,
                           MetaTitleCategory = c.MetaTitle,
                           MetaTitleProduct = a.MetaTitle,
                           MetaTitleProductCategory = b.MetaTitle,
                           NameCategory = c.Name,
                           NameProductCategory = b.Name,
                           Promotion = a.Promotion,
                           Prrice = a.Prrice,
                           Quantity = a.Quantity,
                           Status = a.Status,
                           TopHot = a.TopHot,
                           ViewCount = a.ViewCount,
                           CategoryID = a.CategoryID,
                           NameProduct = a.Name
                       };
            totalRecord = join.Where(x => x.IDCategory == categoryID || x.IDProductCategory == categoryID).Count();
            var model = join.Where(x => x.IDCategory == categoryID || x.IDProductCategory == categoryID).OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
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
        public int CountCommentById(long id)
        {
            return db.CommentProducts.Where(x => x.IDProduct == id).Count();
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
        public Category ViewDetailCategory(long id)
        {
            return db.Categories.Find(id);
        }

        public ProductCategory ViewDetailProductCategory(long id)
        {
            return db.ProductCategories.Find(id);
        }
        public List<ProductCategory> ListProductCategoryByGroupStatus(bool status)
        {
            return db.ProductCategories.Where(x => x.Status == status).ToList();
        }
        public void CountView(long id)
        {
            var product = db.Products.Find(id);
            product.ViewCount += 1;
            db.SaveChanges();
        }
        public bool DeleteComment(int id)
        {
            try
            {
                var comment = db.CommentProducts.Find(id);
                db.CommentProducts.Remove(comment);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
