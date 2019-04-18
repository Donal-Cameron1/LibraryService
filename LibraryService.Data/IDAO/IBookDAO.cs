using LibraryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Data.IDAO
{
    public interface IBookDAO
    {
        Book GetBook(int id);
        void CreateBook(Book book);
        void EditBook(Book book);
        void DeleteBook(Book book);
        IList<Book> BookTextSearch(IList<Book> query, string searchString);
        IList<Book> BookGenreFilter(IList<Book> query, string genre);
        IList<Book> BookStatusFilter(IList<Book> query, string status);
        IList<Book> BookTypeFilter(IList<Book> query, string type);
        IList<Book> GetBooks();
        IList<Book> GetNewBooks();
    }
}
