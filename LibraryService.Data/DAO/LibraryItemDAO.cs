using LibraryService.DAL;
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
    }
}