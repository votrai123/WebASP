using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
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
        public IEnumerable<Content> ListAllPaping(string searchString, int page, int pageSize)
        {
            IQueryable<Content> model = db.Contents;
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
    }
}
