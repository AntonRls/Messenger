using Backend.Context;
using Backend.DB;
using Backend.Models;
using Newtonsoft.Json;
using System.Data.SQLite;
using System.Linq;
using System.Reflection.PortableExecutable;

namespace Backend.Database
{
    public class UserManager
    {
        DBManager manager;
        public UserManager(DBManager manager)
        {
            this.manager = manager;
        }

        public void InsertUser(User user)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Users.Add(new Models.User
                {
                    Name = user.Name,
                    LastName = user.LastName,
                    Password = user.Password
                });
                db.SaveChanges();
                user.Id = db.Users.OrderByDescending(p => p.Id)
                       .FirstOrDefault().Id;
            }
        }
        public bool AuthUser(User user)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var users = db.Users.Where(x => x.Id == user.Id && x.Password == user.Password).ToList();
                return users.Count > 0;
            }
        }
        public User GetUser(int id)
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                var user = db.Users.Find(id);
                User result = new()
                {
                    Id = id,
                    Name = user.Name,
                    LastName = user.LastName,
                    Password = user.Name
                };
                return result;
            }
        }
        public List<User> GetAllUser()
        {

            using(ApplicationContext db = new ApplicationContext())
            {
                var result = new List<User>();
                var users = db.Users.ToList();
                foreach(var user in users)
                {
                    result.Add(new User()
                    {
                        Id = user.Id,
                        Name = user.Name,
                        LastName = user.LastName,
                        Password = user.Password
                    });
                }
                result.Reverse();
                return result;
            }
        }
    }
}
