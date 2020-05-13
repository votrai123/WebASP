using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using Model.ViewModel;
using PagedList;
using Common;
using WebShop.Common;

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
                                                 join b in db.Categories
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

        /// <summary>
        /// List all content for client
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<Content> ListAllPaging(int page, int pageSize)
        {
            IQueryable<Content> model = db.Contents;
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public long Insert(Content entity)
        {
            db.Contents.Add(entity);
            db.SaveChanges();
            if (!string.IsNullOrEmpty(entity.Tags))
            {
                string[] tags = entity.Tags.Split(',');
                foreach (var tag in tags)
                {
                    var tagId = ConvertTxt.utf8Convert3(tag);

                    var existedTag = this.CheckTag(tagId);

                    //insert to to tag table
                    if (!existedTag)
                    {
                        this.InsertTag(tagId, tag);
                    }

                    //insert to content tag
                    this.InsertContentTag(entity.ID, tagId);

                }
            }
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
                //Xử lý tag
                if (!string.IsNullOrEmpty(content.Tags))
                {
                    this.RemoveAllContentTag(content.ID);
                    string[] tags = content.Tags.Split(',');
                    foreach (var tag in tags)
                    {
                        var tagId = ConvertTxt.utf8Convert3(tag);
                        var existedTag = this.CheckTag(tagId);

                        //insert to to tag table
                        if (!existedTag)
                        {
                            this.InsertTag(tagId, tag);
                        }

                        //insert to content tag
                        this.InsertContentTag(content.ID, tagId);

                    }
                }
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
        public void InsertTag(string id, string name)
        {
            var tag = new Tag();
            tag.ID = id;
            tag.Name = name;
            db.Tags.Add(tag);
            db.SaveChanges();
        }

        public void InsertContentTag(long contentId, string tagId)
        {
            var contentTag = new ContentTag();
            contentTag.ContentID = contentId;
            contentTag.TagID = tagId;
            db.ContentTags.Add(contentTag);
            db.SaveChanges();
        }
        public bool CheckTag(string id)
        {
            return db.Tags.Count(x => x.ID == id) > 0;
        }
        public void RemoveAllContentTag(long contentId)
        {
            db.ContentTags.RemoveRange(db.ContentTags.Where(x => x.ContentID == contentId));
            db.SaveChanges();
        }
        public IEnumerable<Content> ListAllByTag(string tag, int page, int pageSize)
        {
            var model = (from a in db.Contents
                         join b in db.ContentTags
                         on a.ID equals b.ContentID
                         where b.TagID == tag
                         select new
                         {
                             Name = a.Name,
                             MetaTitle = a.MetaTitle,
                             Image = a.Image,
                             Description = a.Description,
                             CreatedDate = a.CreatedDate,
                             //CreatedBy = a.CreatedBy,
                             ID = a.ID

                         }).AsEnumerable().Select(x => new Content()
                         {
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Image = x.Image,
                             Description = x.Description,
                             CreatedDate = x.CreatedDate,
                             //CreatedBy = x.CreatedBy,
                             ID = x.ID
                         });
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public Content GetByID(long id)
        {
            return db.Contents.Find(id);
        }

        public Tag GetTag(string id)
        {
            return db.Tags.Find(id);
        }
        public List<Tag> ListTag(long contentId)
        {
            var model = (from a in db.Tags
                         join b in db.ContentTags
                         on a.ID equals b.TagID
                         where b.ContentID == contentId
                         select new
                         {
                             ID = b.TagID,
                             Name = a.Name
                         }).AsEnumerable().Select(x => new Tag()
                         {
                             ID = x.ID,
                             Name = x.Name
                         });
            return model.ToList();
        }

        public List<Tag> ListAllTag()
        {
            var model = (from a in db.Tags
                         join b in db.ContentTags
                         on a.ID equals b.TagID
                         select new
                         {
                             ID = b.TagID,
                             Name = a.Name
                         }).AsEnumerable().Select(x => new Tag()
                         {
                             ID = x.ID,
                             Name = x.Name
                         });
            return model.ToList();
        }
        public IEnumerable<Content> ListAllByCategory(long id, int page, int pageSize)
        {
            var model = (from a in db.Contents
                         join b in db.Categories
                         on a.CategoryID equals b.ID
                         where b.ID == id
                         select new
                         {
                             Name = a.Name,
                             MetaTitle = a.MetaTitle,
                             Image = a.Image,
                             Description = a.Description,
                             CreatedDate = a.CreatedDate,
                             //CreatedBy = a.CreatedBy,
                             ID = a.ID

                         }).AsEnumerable().Select(x => new Content()
                         {
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Image = x.Image,
                             Description = x.Description,
                             CreatedDate = x.CreatedDate,
                             //CreatedBy = x.CreatedBy,
                             ID = x.ID
                         });
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public Category ViewDetailCategory(long id)
        {
            return db.Categories.Find(id);
        }
    }
}
