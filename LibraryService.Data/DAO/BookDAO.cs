using LibraryService.DAL;
using LibraryService.Data.DAL;
using LibraryService.Data.IDAO;
using LibraryService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public void EditBook(Book book)
        {
            db.Books.Attach(book);
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();
            /*var entry = db.Entry(book);
            var state = entry.State;
            db.Books.Attach(book);
            var state1 = entry.State;
            entry.State = EntityState.Modified;
            db.Books.Attach(book);*/
            //db.SaveChanges();
        }

        public Book GetBook(int id)
        {
            return db.Books.AsNoTracking().Where(b => b.id == id).FirstOrDefault();
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
            IList<Book> newBooks = db.Books.AsNoTracking().Where(x => x.DateAdded > baselineDate).OrderByDescending(x => x.DateAdded).ToList();
            return newBooks;
        }

        public void BookmarkBook(int id, string currentUserId)
        {
            Book book = GetBookWithTracking(db, id);
            User user = UserDAO.GetUserWithTracking(db, currentUserId);
            user.BookmarkedLibraryItems.Add(book);
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

        public void RemoveReservation(int id, string currentUserId)
        {
            Book book = GetBookWithTracking(db, id);
            User user = UserDAO.GetUserWithTracking(db, currentUserId);

            user.ReservedLibraryItems.Remove(book);
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
