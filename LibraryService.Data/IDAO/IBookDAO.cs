using LibraryService.DAL;
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

        void UpdateBook(Book book);

        void DeleteBook(Book book);

        void BookmarkBook(int id, string UserId);

        void ReserveBook(int id, string UserId);

        IList<Book> BookTextSearch(IList<Book> query, string searchString);

        IList<Book> BookGenreFilter(IList<Book> query, string genre);

        IList<Book> BookStatusFilter(IList<Book> query, string status);

        IList<Book> BookTypeFilter(IList<Book> query, string type);

        IList<Book> GetBooks();

        IList<Book> GetNewBooks();

        void DeleteReservation(int id, string userId);

        void DeleteBookmark(int id, string userId);

        IList<LibraryItem> GetReservedBooks();

        //void LoanBook(List<int> idList, string currentUserId);

        //List<Book> GetBooksForUserID(string UserID);

        //void ReturnBook(int BookID);

        //void RenewBook(int BookID);
    }
}
