using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class CommentProductDao
    {
        WebShopDbContext db = null;
        public CommentProductDao()
        {
            db = new WebShopDbContext();
        }
        public long Insert(CommentProduct comment)
        {
            db.CommentProducts.Add(comment);
            db.SaveChanges();
            return comment.ID;
        }
        //public IEnumerable<About> ListAllPaping(string searchString, int page, int pageSize)
        //{
        //    IQueryable<About> model = db.Abouts;
        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        model = model.Where(x => x.Name.Contains(searchString));
        //    }
        //    return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        //}

    }
}