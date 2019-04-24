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
            var _book = this.GetBook(book.id);

            _book.Author = book.Author;
            _book.BookGenre = book.BookGenre;
            _book.Library = book.Library;
            _book.Pages = book.Pages;
            _book.User = book.User;
            _book.id = book.id;
            _book.AgeRestriction = book.AgeRestriction;
            _book.BookmarkedBy = book.BookmarkedBy;
            _book.DateAdded = book.DateAdded;
            _book.LibraryId = book.LibraryId;
            _book.LoanedBy = book.LoanedBy;
            _book.PublishedAt = book.PublishedAt;
            _book.Publisher = book.Publisher;
            _book.PurchaseValue = book.PurchaseValue;
            _book.ReservedBy = book.ReservedBy;
            _book.Genre = book.Genre;

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

        public void DeleteReservation(int id, string currentUserId)
        {
            Book book = GetBookWithTracking(db, id);
            User user = UserDAO.GetUserWithTracking(db, currentUserId);
            user.ReservedLibraryItems.Remove(book);
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
