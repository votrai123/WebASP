using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;
namespace Model.Dao
{
    public class UserDao
    {
        WebShopDbContext db = null;
        public UserDao()
        {
            db = new WebShopDbContext();
        }

        public User GetById(string userName)
        {
            return db.Users.OrderByDescending(x => x.ID).SingleOrDefault(x => x.UserName == userName);
        }

        public User ViewDetail(int id)
        {
            return db.Users.Find(id);
        }
        public long Insert(User entity)
        {
            try
            {
                db.Users.Add(entity);
                db.SaveChanges();
                return entity.ID;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
        public long InsertForFacebook(User entity)
        {
            var user = db.Users.SingleOrDefault(x => x.UserName == entity.UserName);
            if (user == null)
            {
                db.Users.Add(entity);
                db.SaveChanges();
                return entity.ID;
            }
            else
            {
                return user.ID;
            }

        }

        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.ID);
                user.FullName = entity.FullName;
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;

                }
                user.Phone = entity.Phone;
                user.Country = entity.Country;
                user.StreetAddress = entity.StreetAddress;

                user.role = entity.role;
                user.Status = entity.Status;
                user.ModifiedDate = DateTime.Now;
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
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<User> ListAllPaping(string searchString, int page, int pageSize)
        {
            IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.FullName.Contains(searchString) || x.Email.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public int Login(string userName, string passWord, bool isLoginAdmin = true)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.Status == false)
                {
                    return -1;
                }
                else if (result.Password != passWord)
                {
                    return -2;
                }
                else if (result.Password == passWord && result.role == true || result.role == false)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
        }

        public bool CheckUserName(string username)
        {
            return db.Users.Count(x => x.UserName == username) > 0;
        }

        public bool CheckEmail(string email)
        {
            return db.Users.Count(x => x.Email == email) > 0;
        }
    }
}
