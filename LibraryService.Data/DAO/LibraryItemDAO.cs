using LibraryService.DAL;
using LibraryService.Data.DAO;
using LibraryService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LibraryService.Services.Service
{
    public class LibraryItemDAO : ILibraryItemDAO
    {
        private LibraryContext db = new LibraryContext();

        public LibraryItemDAO()
        {
        }

        public static LibraryItem GetLibraryItemWithTracking(LibraryContext context, int id)
        {
            return context.LibraryItems
                .Include(b => b.ReservedBy)
                .Include(b => b.LoanedBy)
                .Include(b => b.BookmarkedBy)
                .Where(b => b.id == id).FirstOrDefault();
        }

        public LibraryItem GetLibaryItem(int id)
        {
            return db.LibraryItems
                .Include(b => b.ReservedBy)
                .Include(b => b.LoanedBy)
                .Include(b => b.BookmarkedBy)
                .Where(b => b.id == id)
                .AsNoTracking().FirstOrDefault();
        }

        public IList<LibraryItem> GetLibraryItems()
        {
            return db.LibraryItems
                .Include(b => b.ReservedBy)
                .Include(b => b.LoanedBy)
                .Include(b => b.BookmarkedBy).ToList();
        }

        public IList<LibraryItem> GetLoanedLibraryItems()
        {
            return db.LibraryItems.Where(b => b.Status == Status.Loaned).ToList();
        }

        public IList<LibraryItem> GetOverdueLibraryItems()
        {          
            var baselineDate = DateTime.Today;
            return db.LibraryItems.Where(b => b.ReturnDate < baselineDate).OrderByDescending(b => b.ReturnDate).ToList();
        }

        public IList<LibraryItem> GetReservedLibraryItems(string id)
        {
            return db.LibraryItems.Where(b => b.Status == Status.Reserved && b.ReservedBy.UserId == id).ToList();
        }

        public void LoanLibraryItem(int id)
        {
            LibraryItem libraryItem = GetLibraryItemWithTracking(db, id);
            libraryItem.LoanedBy = libraryItem.ReservedBy;
            libraryItem.ReturnDate = DateTime.Today.AddDays(14);
            libraryItem.ReservedBy = null;
            libraryItem.ReservedUntil = null;
            libraryItem.Status = Status.Loaned;

            db.Entry(libraryItem).State = EntityState.Modified;
            db.SaveChanges();
        }

        public IList<LibraryItem> TextSearch(IList<LibraryItem> loanedItems, string searchString)
        {
            return loanedItems.Where(b => b.Title.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0).ToList<LibraryItem>();
        }

        public void BookmarkLibraryItem(int id, string currentUserId)
        {
            LibraryItem libraryItem = GetLibraryItemWithTracking(db, id);
            User user = UserDAO.GetUserWithTracking(db, currentUserId);
            user.BookmarkedLibraryItems.Add(libraryItem);
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteBookmark(int id, string currentUserId)
        {
            LibraryItem libraryItem = GetLibraryItemWithTracking(db, id);
            User user = UserDAO.GetUserWithTracking(db, currentUserId);
            user.BookmarkedLibraryItems.Remove(libraryItem);
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void ReserveLibraryItem(int id, string currentUserId)
        {
            LibraryItem libraryItem = GetLibraryItemWithTracking(db, id);
            User user = UserDAO.GetUserWithTracking(db, currentUserId);
            user.ReservedLibraryItems.Add(libraryItem);
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteReservation(int id, string currentUserId)
        {
            LibraryItem libraryItem = GetLibraryItemWithTracking(db, id);
            User user = UserDAO.GetUserWithTracking(db, currentUserId);
            libraryItem.Status = Status.Available;
            user.ReservedLibraryItems.Remove(libraryItem);
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void UpdateLibraryItem(LibraryItem item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}