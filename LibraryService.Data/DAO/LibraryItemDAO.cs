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

        public void ExtendLoan(int id)
        {
            LibraryItem libraryItem = GetLibraryItemWithTracking(db, id);

            if (libraryItem.Status != Status.Reserved && libraryItem.ReturnDate < DateTime.Today.AddDays(3))
            {
                libraryItem.ReturnDate = DateTime.Today.AddDays(7);
                db.Entry(libraryItem).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void ReturnLibraryItem(int id)
        {
            LibraryItem libraryItem = GetLibraryItemWithTracking(db, id);

            if (libraryItem.Status == Status.Loaned)
            {
                libraryItem.LoanedBy = null;
                libraryItem.Status = Status.Available;
                libraryItem.ReturnDate = null;
                libraryItem.Status = Status.Available;
                db.Entry(libraryItem).State = EntityState.Modified;
                db.SaveChanges();
            }

            if (libraryItem.Status == Status.Reserved)
            {
                libraryItem.LoanedBy = null;
                libraryItem.Status = Status.Reserved;
                libraryItem.ReturnDate = null;
                libraryItem.Status = Status.Available;
                db.Entry(libraryItem).State = EntityState.Modified;
                db.SaveChanges();

            }
        }

        public IList<LibraryItem> TextSearch(IList<LibraryItem> loanedItems, string searchString)
        {
            return loanedItems.Where(b => b.Title.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0).ToList<LibraryItem>();
        }
    }
}