using LibraryService.Models;
using System.Collections.Generic;

namespace LibraryService.Services.IService
{
    public interface IBookService
    {
        Book GetBook(int id);

        IList<Book> GetBooks();

        IList<Book> GetNewBooks();

        Book CreateDefaultBook();

        void CreateBook(Book book);

        void UpdateBook(Book book);

        void DeleteBook(Book book);

        IList<Book> BookTextSearch(IList<Book> query, string searchString);

        IList<Book> BookGenreFilter(IList<Book> query, string genre);

        IList<Book> BookStatusFilter(IList<Book> query, string status);

        IList<Book> BookTypeFilter(IList<Book> query, string type);
    }
}
