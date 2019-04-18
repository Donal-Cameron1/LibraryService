
using LibraryService.DAL;
using LibraryService.Data.IDAO;
using LibraryService.Models;
using System.Linq;
using System.Data.Entity;
using LibraryService.Data.DAL;
using System.Collections.Generic;

namespace LibraryService.Data.DAO
{
    public class UserDAO : IUserDAO
    {
        private LibraryContext db = DbUtils.db;

        public User GetCurrentUser(string currentUserId)
        {
             return db.Users.Include(u => u.BookmarkedBooks).SingleOrDefault(x => x.UserId == currentUserId);
            //User user =  db.Users.SingleOrDefault(x => x.UserId == currentUserId);
            //return user;
        }

        public void EditUser(User user)
        {
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

        public IList<User> GetUsers()
        {
            return db.Users.ToList();
        }

        public User GetUser(string id)
        {
            return db.Users.Find(id);
        }

        public void CreateUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            db.Users.Remove(user);
            db.SaveChanges();
        }

    }
}
