using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;

namespace Model.Dao
{
    public class SlideDao
    {
        WebShopDbContext db = null;
        public SlideDao()
        {
            db = new WebShopDbContext();
        }

        public List<Slide> ListAll()
        {
            return db.Slides.Where(x => x.Status == true).OrderBy(y => y.DisplayOrder).ToList();
        }
        public long Insert(Slide entity)
        {
            db.Slides.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public IEnumerable<Slide> ListAllPaping( int page, int pageSize)
        {
            IQueryable<Slide> model = db.Slides;
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public bool Update(Slide entity)
        {
            try
            {
                var slide = db.Slides.Find(entity.ID);
                if (!string.IsNullOrEmpty(entity.Image))
                {
                    slide.Image = entity.Image;

                }
                slide.Status = entity.Status;
                slide.ModifiedDate = DateTime.Now;
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
                var slide = db.Slides.Find(id);
                db.Slides.Remove(slide);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public Slide ViewDetail(int id)
        {
            return db.Slides.Find(id);
        }
    }
}
