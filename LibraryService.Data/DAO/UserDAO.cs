
using LibraryService.DAL;
using LibraryService.Data.IDAO;
using LibraryService.Models;
using System.Linq;
using System.Data.Entity;
using LibraryService.Data.DAL;

namespace LibraryService.Data.DAO
{
    public class UserDAO : IUserDAO
    {
        private LibraryContext db = DbUtils.db;

        public User GetUser(string currentUserId)
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
    }
}
