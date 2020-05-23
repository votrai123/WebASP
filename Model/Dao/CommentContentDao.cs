using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class CommentContentDao
    {
        WebShopDbContext db = null;
        public CommentContentDao()
        {
            db = new WebShopDbContext();
        }
        public long Insert(CommentContent comment)
        {
            db.CommentContents.Add(comment);
            db.SaveChanges();
            return comment.ID;

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

    }
}
