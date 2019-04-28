using LibraryService.Models;
using System.Collections.Generic;

namespace LibraryService.Data.IDAO
{
    public interface IBookDAO
    {
        Book GetBook(int id);

        void CreateBook(Book book);

        void UpdateBook(Book book);

        void DeleteBook(Book book);

        IList<Book> BookTextSearch(IList<Book> query, string searchString);

        IList<Book> BookGenreFilter(IList<Book> query, string genre);

        IList<Book> BookStatusFilter(IList<Book> query, string status);

        IList<Book> BookTypeFilter(IList<Book> query, string type);

        IList<Book> GetBooks();

        IList<Book> GetNewBooks();

        IList<LibraryItem> GetReservedBooks();

        //List<Book> GetBooksForUserID(string UserID);

        //void ReturnBook(int BookID);

        //void RenewBook(int BookID);
    }
}
