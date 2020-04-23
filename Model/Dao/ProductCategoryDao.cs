using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ViewModel;
namespace Model.Dao
{
    public class ProductCategoryDao
    {
        WebShopDbContext db = null;
        public ProductCategoryDao()
        {
            db = new WebShopDbContext();
        }
        public long Insert(ProductCategory entity)
        {
            db.ProductCategories.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public IEnumerable<CategoryViewModel> ListAllPaping(string searchString, int page, int pageSize)
        {
            IQueryable<CategoryViewModel> model = from a in db.ProductCategories
                                                  join b in db.Categories
                                                  on a.ParentID equals b.ID
                                                  select new CategoryViewModel()
                                                  {
                                                      ID = a.ID,
                                                      Name = a.Name,
                                                      MetaTitle = a.MetaTitle,
                                                      CateName = b.Name,
                                                      CreatedDate = a.CreatedDate,
                                                      Status = a.Status,

                                                  };
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString)||x.CateName.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategories.Find(id);
        }
        public bool Update(ProductCategory entity)
        {
            try
            {
                var productcategory = db.ProductCategories.Find(entity.ID);
                if (entity.MetaTitle != null)
                {
                    productcategory.MetaTitle = entity.MetaTitle;
                }
                productcategory.Name = entity.Name;
                productcategory.Status = entity.Status;
                productcategory.ModifiedDate = DateTime.Now;
                productcategory.MetaDescriptions = entity.MetaDescriptions;
                productcategory.ParentID = entity.ParentID;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool Delete(int id)
        {
            try
            {
                var productcategory = db.ProductCategories.Find(id);
                db.ProductCategories.Remove(productcategory);
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
    }
}
