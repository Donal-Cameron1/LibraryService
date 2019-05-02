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

        //gets a single library item and loads ReservedBy, LoanedBy, BookmarkedBy with it
        public static LibraryItem GetLibraryItemWithTracking(LibraryContext context, int id)
        {
            return context.LibraryItems
                .Include(b => b.ReservedBy)
                .Include(b => b.LoanedBy)
                .Include(b => b.BookmarkedBy)
                .Where(b => b.id == id).FirstOrDefault();
        }

        //gets a single library item without tracking and loads ReservedBy, LoanedBy, BookmarkedBy with it
        public LibraryItem GetLibaryItem(int id)
        {
            return db.LibraryItems
                .Include(b => b.ReservedBy)
                .Include(b => b.LoanedBy)
                .Include(b => b.BookmarkedBy)
                .Where(b => b.id == id)
                .AsNoTracking().FirstOrDefault();
        }

        //gets all libraryitems and loads ReservedBy, LoanedBy, BookmarkedBy with it
        public IList<LibraryItem> GetLibraryItems()
        {
            return db.LibraryItems
                .Include(b => b.ReservedBy)
                .Include(b => b.LoanedBy)
                .Include(b => b.BookmarkedBy).ToList();
        }

        //gets all loaned libraryitems from the database
        public IList<LibraryItem> GetLoanedLibraryItems()
        {
            return db.LibraryItems.Where(b => b.Status == Status.Loaned).ToList();
        }

        //gets all items which are overdue(returndate is in the past)
        public IList<LibraryItem> GetOverdueLibraryItems()
        {
            var baselineDate = DateTime.Today;
            return db.LibraryItems
                .Include(b => b.ReservedBy)
                .Include(b => b.LoanedBy)
                .Include(b => b.BookmarkedBy)
                .Where(b => b.ReturnDate < baselineDate).OrderByDescending(b => b.ReturnDate).ToList();
        }

        //gets all reserved items of a specific user
        public IList<LibraryItem> GetReservedLibraryItemsOfUser(string id)
        {
            return db.LibraryItems
                .Include(b => b.ReservedBy)
                .Include(b => b.LoanedBy)
                .Include(b => b.BookmarkedBy)
                .Where(b => b.Status == Status.Reserved && b.ReservedBy.UserId == id).ToList();
        }

        //gets all loaned items of a specific user
        public IList<LibraryItem> GetLoanedLibraryItemsOfUser(string id)
        {
            return db.LibraryItems
                .Include(b => b.ReservedBy)
                .Include(b => b.LoanedBy)
                .Include(b => b.BookmarkedBy)
                .Where(b => b.Status == Status.Loaned && b.LoanedBy.UserId == id).ToList();
        }

        //saves any changes to the database
        public void UpdateLibraryItem(LibraryItem item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }

        //changes the status of an item to loaned, maps it to the user, deletes the reservation and adds a returndate
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

        //The user has the option to add another week to their loan but this function will only be available 3 days out from the return date
        //also only if the item is not reserved
        public void ExtendLoan(int id)
        {
            LibraryItem libraryItem = GetLibraryItemWithTracking(db, id);

            if (libraryItem.ReservedBy == null && libraryItem.ReservedUntil == null && libraryItem.ReturnDate < DateTime.Today.AddDays(3))
            {
                libraryItem.ReturnDate = libraryItem.ReturnDate.Value.AddDays(7);
                db.Entry(libraryItem).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        //deletes the loan(loanedBy, returndate), sets status to available 
        //if item already got reserved it sets the status to reserved instead of available
        public void ReturnLibraryItem(int id)
        {
            LibraryItem libraryItem = GetLibraryItemWithTracking(db, id);

            if (libraryItem.ReservedBy == null && libraryItem.ReservedUntil == null)
            {
                libraryItem.LoanedBy = null;
                libraryItem.ReturnDate = null;
                libraryItem.Status = Status.Available;
                db.Entry(libraryItem).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                libraryItem.LoanedBy = null;
                libraryItem.ReturnDate = null;
                libraryItem.Status = Status.Reserved;
                db.Entry(libraryItem).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        //filters an item list by the entered searchStríng
        public IList<LibraryItem> TextSearch(IList<LibraryItem> loanedItems, string searchString)
        {
            return loanedItems.Where(b => b.Title.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0).ToList<LibraryItem>();
        }

        //gets the selected book and saves it to the bookmarkedlibraryitems list of the user
        public void BookmarkLibraryItem(int id, string currentUserId)
        {
            LibraryItem libraryItem = GetLibraryItemWithTracking(db, id);
            User user = UserDAO.GetUserWithTracking(db, currentUserId);
            user.BookmarkedLibraryItems.Add(libraryItem);
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

        //removes an item of the bookmarkedlibraryitems of a user
        public void DeleteBookmark(int id, string currentUserId)
        {
            LibraryItem libraryItem = GetLibraryItemWithTracking(db, id);
            User user = UserDAO.GetUserWithTracking(db, currentUserId);
            user.BookmarkedLibraryItems.Remove(libraryItem);
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

        //gets the selected item and changes the status to reserved, adds it to the reservedlibraryitem list of the user 
        public void ReserveLibraryItem(int id, string currentUserId)
        {
            LibraryItem libraryItem = GetLibraryItemWithTracking(db, id);
            User user = UserDAO.GetUserWithTracking(db, currentUserId);
            user.ReservedLibraryItems.Add(libraryItem);
            libraryItem.ReservedUntil = DateTime.Today.AddDays(5);
            if (libraryItem.Status == Status.Available)
            {
                libraryItem.Status = Status.Reserved;
            }
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

        //removes the item from the reserved items list of the user
        public void DeleteReservation(int id, string currentUserId)
        {
            LibraryItem libraryItem = GetLibraryItemWithTracking(db, id);
            User user = UserDAO.GetUserWithTracking(db, currentUserId);
            libraryItem.Status = Status.Available;
            user.ReservedLibraryItems.Remove(libraryItem);
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

    }
}