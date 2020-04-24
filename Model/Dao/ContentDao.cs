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
    public class ContentDao
    {

        WebShopDbContext db = null;

        public ContentDao()
        {
            db = new WebShopDbContext();
        }
        public IEnumerable<ContentViewModel> ListAllPaping(string searchString, int page, int pageSize)
        {
            IQueryable<ContentViewModel> model = from a in db.Contents
                                                 join b in db.ProductCategories
                                                  on a.CategoryID equals b.ID
                                                 select new ContentViewModel()
                                                 {
                                                     ID = a.ID,
                                                     Name = a.Name,
                                                     MetaTitle = a.MetaTitle,
                                                     CateName = b.Name,
                                                     CreatedDate = a.CreatedDate,
                                                     Status = a.Status,
                                                     Description = a.Description,
                                                     Detail = a.Detail,
                                                     Image = a.Image,
                                                     ModifiedDate = a.ModifiedDate,
                                                     TopHot = a.TopHot,
                                                     ViewCount = a.ViewCount
                                                 };
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public long Insert(Content entity)
        {
            db.Contents.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Delete(int id)
        {
            try
            {
                var content = db.Contents.Find(id);
                db.Contents.Remove(content);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Content ViewDetail(int id)
        {
            return db.Contents.Find(id);
        }
        public bool Update(Content entity)
        {
            try
            {
                var content = db.Contents.Find(entity.ID);
                content.Name = entity.Name;
                if (!string.IsNullOrEmpty(entity.Image))
                {
                    content.Image = entity.Image;

                }
                if (entity.MetaTitle != null)
                {
                    content.MetaTitle = entity.MetaTitle;
                }
                if (entity.CategoryID != null)
                {
                    content.CategoryID = entity.CategoryID;
                }
                content.Status = entity.Status;
                content.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public List<ProductCategory> ListByGroupStatus(bool status)
        {
            return db.ProductCategories.Where(x => x.Status == status).ToList();
        }
        public List<Category> ListByGroupStatusCategory(bool status)
        {
            return db.Categories.Where(x => x.Status == status).ToList();
        }
    }
}
