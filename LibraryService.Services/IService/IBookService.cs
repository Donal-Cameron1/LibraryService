using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryService.Models;

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

        void BookmarkBook(int id, string UserId);

        void DeleteBook(Book book);

        void DeleteBookmark(int id, string UserId);

        IList<Book> BookTextSearch(IList<Book> query, string searchString);

        IList<Book> BookGenreFilter(IList<Book> query, string genre);

        IList<Book> BookStatusFilter(IList<Book> query, string status);

        IList<Book> BookTypeFilter(IList<Book> query, string type);

        void Reserve(int id, string currentUserId);

        void DeleteReservation(int id, string UserId);

        void LoanBook(List<int> idList, string currentUserId);

        List<Book> GetBooksForUserID(string UserID);

        void ReturnBook(int BookID);

        void RenewBook(int BookID);

    }
}
