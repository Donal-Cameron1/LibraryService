using LibraryService.DAL;
using LibraryService.Data.DAL;
using LibraryService.Data.IDAO;
using LibraryService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Data.DAO
{
    public class BookDAO : IBookDAO
    {
        private LibraryContext db = new LibraryContext();
        
        public IList<Book> BookGenreFilter(IList<Book> query, string genre)
        {
            return query.Where(b => b.BookGenre.ToString().Equals(genre)).ToList<Book>();
        }

        public IList<Book> BookStatusFilter(IList<Book> query, string status)
        {
            return query.Where(b => b.Status.ToString().Equals(status)).ToList<Book>();
        }

        public IList<Book> BookTextSearch(IList<Book> query, string searchString)
        {
            return query.Where(b => b.Title.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0
                                 || b.Author.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0).ToList<Book>();
        }

        public IList<Book> BookTypeFilter(IList<Book> query, string type)
        {
            return query.Where(b => b.Type.ToString().Equals(type)).ToList<Book>();
        }

        public void CreateBook(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
        }

        public void DeleteBook(Book book)
        {
            db.Books.Attach(book);
            db.Books.Remove(book);
            db.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();
        }

        public Book GetBook(int id)
        {
            var book = 
                from b in db.Books
                where b.id == id
                select b;
            return book.First();
        }

        public IList<Book> GetBooks()
        {
            IQueryable<Book> bookquery;
            bookquery = from b
                        in db.Books
                        select b;
            return bookquery.AsNoTracking().ToList<Book>();
        }

        public static Book GetBookWithTracking(LibraryContext context, int id)
        {
            return context.Books.Where(b => b.id == id).FirstOrDefault();
        }

        public IList<Book> GetNewBooks()
        {
            var baselineDate = DateTime.Now.AddDays(-7);
            //get books that got added in the last 7 days
            IQueryable<Book> newbooks =  
                from b in db.Books
                where b.DateAdded > baselineDate
                select b;
            return newbooks.OrderByDescending(x => x.DateAdded).ToList();
        }

        public void BookmarkBook(int id, string currentUserId)
        {
            Book book = GetBookWithTracking(db, id);
            User user = UserDAO.GetUserWithTracking(db, currentUserId);
            user.BookmarkedLibraryItems.Add(book);
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteBookmark(int id, string currentUserId)
        {
            Book book = GetBookWithTracking(db, id);
            User user = UserDAO.GetUserWithTracking(db, currentUserId);
            user.BookmarkedLibraryItems.Remove(book);
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void ReserveBook(int id, string currentUserId)
        {
            Book book = GetBookWithTracking(db, id);
            User user = UserDAO.GetUserWithTracking(db, currentUserId);
            user.ReservedLibraryItems.Add(book);
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void LoanBook(List<int> bookIds, string UserId)
        {
            User user = UserDAO.GetUserWithTracking(db, UserId);

            foreach (int id in bookIds)
            {
                var thisbook = this.GetBook(id);
                thisbook.UserId = UserId;
                thisbook.Status = Status.Loaned;
                thisbook.ReturnDate = DateTime.Now.AddDays(14);

                this.UpdateBook(thisbook);
            }   
        }

        public void DeleteReservation(int id, string currentUserId)
        {
            Book book = GetBookWithTracking(db, id);
            User user = UserDAO.GetUserWithTracking(db, currentUserId);
            book.Status = Status.Available;
            user.ReservedLibraryItems.Remove(book);
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

        public List<Book> GetBooksForUserID(string UserID)
        {
                IQueryable<Book> books =
                from b in db.Books
                where b.UserId == UserID
                select b;
            return books.ToList();

        }

        public void ReturnBook(int BookID)
        {
            var ThisBook = this.GetBook(BookID);
            ThisBook.Status = Status.Available;
            ThisBook.UserId = null;
            ThisBook.ReturnDate = null;
            this.UpdateBook(ThisBook);

        }

        public void RenewBook(int BookID)
        {
            var ThisBook = this.GetBook(BookID);
            ThisBook.ReturnDate = DateTime.Today.AddDays(7);
            this.UpdateBook(ThisBook);

        }

    }
}
