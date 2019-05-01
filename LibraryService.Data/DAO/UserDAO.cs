
using LibraryService.DAL;
using LibraryService.Data.IDAO;
using LibraryService.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LibraryService.Data.DAO
{
    public class UserDAO : IUserDAO
    {
        private LibraryContext db = new LibraryContext();

        //Get the currently signed in user
        public User GetCurrentUser(string currentUserId)
        {
            return db.Users
               .AsNoTracking()
               .Include(u => u.BookmarkedLibraryItems)
               .Include(u => u.ReservedLibraryItems)
               .Include(u => u.LoanedLibraryItems)
               .SingleOrDefault(x => x.UserId == currentUserId);
        }

        //Get the items the signed in user has reserved, bookmarked and loaned.
        public IList<User> GetUsers()
        {
            return db.Users.AsNoTracking()
                .Include(u => u.BookmarkedLibraryItems)
                .Include(u => u.ReservedLibraryItems)
                .Include(u => u.LoanedLibraryItems)
                .ToList();
        }

        //gets a single user without tracking
        public User GetUser(string id)
        {
            return db.Users
                .Include(u => u.BookmarkedLibraryItems)
                .Include(u => u.ReservedLibraryItems)
                .Include(u => u.LoanedLibraryItems)
                .AsNoTracking().Where(u => u.UserId == id).FirstOrDefault();
        }

        //Gets a user through tracking
        public static User GetUserWithTracking(LibraryContext context, string id)
        {
            return context.Users
                .Include(u => u.BookmarkedLibraryItems)
                .Include(u => u.ReservedLibraryItems)
                .Include(u => u.LoanedLibraryItems)
                .Where(u => u.UserId == id).FirstOrDefault();
        }

        //Edit information about the user
        public void EditUser(User user)
        {
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }
   

        //Allows the staff or admin to add a new user to the database.
        public void CreateUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        //Allows an admin to delete a user.
        public void DeleteUser(User user)
        {
            db.Users.Attach(user);
            db.Users.Remove(user);
            db.SaveChanges();
        }
    }
}
