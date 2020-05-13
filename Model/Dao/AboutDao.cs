using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class AboutDao
    {
        WebShopDbContext db = null;
        public AboutDao()
        {
            db = new WebShopDbContext();
        }
        public long Insert(About entity)
        {
            db.Abouts.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public IEnumerable<About> ListAllPaping(string searchString, int page, int pageSize)
        {
            IQueryable<About> model = db.Abouts;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public bool Delete(int id)
        {
            try
            {
                var about = db.Abouts.Find(id);
                db.Abouts.Remove(about);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public About ViewDetail(int id)
        {
            return db.Abouts.Find(id);
        }
        public bool Update(About entity)
        {
            try
            {
                var about = db.Abouts.Find(entity.ID);
                about.Name = entity.Name;
                if (!string.IsNullOrEmpty(entity.Image))
                {
                    about.Image = entity.Image;

                }
                if (entity.MetaTitle != null)
                {
                    about.MetaTitle = entity.MetaTitle;
                }
                about.Status = entity.Status;
                about.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public List<About> ListAll()
        {
            return db.Abouts.Where(x => x.Status == true).OrderBy(y => y.CreatedDate).ToList();
        }
    }
}
