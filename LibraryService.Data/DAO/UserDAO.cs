
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
        private LibraryContext db = new LibraryContext();

        public User GetCurrentUser(string currentUserId)
        {
            return db.Users
               .AsNoTracking()
               .Include(u => u.BookmarkedLibraryItems)
               .Include(u => u.ReservedLibraryItems)
               //.Include(u => u.LoanedLibraryItems)
               .SingleOrDefault(x => x.UserId == currentUserId);
        }

        public void EditUser(User user)
        {
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

        public IList<User> GetUsers()
        {
            return db.Users.AsNoTracking().ToList();
        }

        public User GetUser(string id)
        {
            return db.Users
                .Include(u => u.BookmarkedLibraryItems)
                .Include(u => u.ReservedLibraryItems)
                .AsNoTracking().Where(u => u.UserId == id).FirstOrDefault(); 
        }

        public User GetUserByUsername(string username)
        {
            var user = from u in db.Users where u.UserName == username select u;
            return user.First();
        }

        public static User GetUserWithTracking(LibraryContext context, string id)
        {
            return context.Users
                .Include(u => u.BookmarkedLibraryItems)
                .Include(u => u.ReservedLibraryItems)
                .Where(u => u.UserId == id).FirstOrDefault();
        }

        public void CreateUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            db.Users.Attach(user);
            db.Users.Remove(user);
            db.SaveChanges();
        }     
    }
}
