
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
                .Include(u => u.LoanedLibraryItems)
                .SingleOrDefault(x => x.UserId == currentUserId);
                
            //User user =  db.Users.SingleOrDefault(x => x.UserId == currentUserId);
            //return user;
        }

        public void EditUser(User user)
        {
            db.Users.Attach(user);
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

        public IList<User> GetUsers()
        {
            return db.Users.AsNoTracking()
                .Include(u => u.BookmarkedLibraryItems)
                .Include(u => u.ReservedLibraryItems)
                .Include(u => u.LoanedLibraryItems)
                .ToList();
        }

        public User GetUser(string id)
        {
            return db.Users.AsNoTracking().Where(u => u.UserId == id).FirstOrDefault(); 
        }

        public static User GetUserWithTracking(LibraryContext context, string id)
        {
            return context.Users
                .Include(u => u.BookmarkedLibraryItems)
                .Include(u => u.ReservedLibraryItems)
                .Include(u => u.LoanedLibraryItems)
                .Where(u => u.UserId == id).FirstOrDefault();
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
