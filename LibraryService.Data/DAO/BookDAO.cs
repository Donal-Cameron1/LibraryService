using LibraryService.DAL;
using LibraryService.Data.IDAO;
using LibraryService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LibraryService.Data.DAO
{
    public class BookDAO : IBookDAO
    {
        private LibraryContext db = new LibraryContext();

        //filters a list of books by genre
        public IList<Book> BookGenreFilter(IList<Book> query, string genre)
        {
            return query.Where(b => b.BookGenre.ToString().Equals(genre)).ToList<Book>();
        }

        //filters a list of books by status
        public IList<Book> BookStatusFilter(IList<Book> query, string status)
        {
            return query.Where(b => b.Status.ToString().Equals(status)).ToList<Book>();
        }

        //filters a list of books by the entered searchString
        public IList<Book> BookTextSearch(IList<Book> query, string searchString)
        {
            return query.Where(b => b.Title.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0
                                 || b.Author.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0).ToList<Book>();
        }

        //filters a list of books by type
        public IList<Book> BookTypeFilter(IList<Book> query, string type)
        {
            return query.Where(b => b.Type.ToString().Equals(type)).ToList<Book>();
        }

        //filters a list of book by librray
        public IList<Book> BookLibraryFilter(IList<Book> query, string library)
        {
            return query.Where(b => b.LibraryId.ToString().Equals(library)).ToList<Book>();
        }

        //adds a new created book to the database
        public void CreateBook(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
        }

        //removes a book from the database
        public void DeleteBook(Book book)
        {
            db.Books.Attach(book);
            db.Books.Remove(book);
            db.SaveChanges();
        }

        //saves changes to the database
        public void UpdateBook(Book book)
        {
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();
        }

        //gets a single book without tracking from the database
        public Book GetBook(int id)
        {
            return db.Books.AsNoTracking().Where(b => b.id == id).FirstOrDefault();
        }

        //gets all books from the database and loads BookmarkedBy, reservedBy, loanedBy with it
        public IList<Book> GetBooks()
        {
            return db.Books
                .Include(b => b.BookmarkedBy)
                .Include(b => b.ReservedBy)
                .Include(b => b.LoanedBy)
                .AsNoTracking().ToList();
        }

        //gets a single book with tracking
        public static Book GetBookWithTracking(LibraryContext context, int id)
        {
            return context.Books.Where(b => b.id == id).FirstOrDefault();
        }

        //gets all books that fot added during the last 14 days
        public IList<Book> GetNewBooks()
        {
            var baselineDate = DateTime.Now.AddDays(-14);
            IList<Book> newBooks = db.Books
                .Include(b => b.BookmarkedBy)
                .Include(b => b.ReservedBy)
                .Include(b => b.LoanedBy)
                .AsNoTracking()
                .Where(x => x.DateAdded > baselineDate)
                .OrderByDescending(x => x.DateAdded).ToList();
            return newBooks;
        }

        //gets all reserved books
        public IList<LibraryItem> GetReservedBooks()
        {
            return db.LibraryItems.Where(b => b.Status == Status.Reserved).ToList();

        }
    }
}
