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
        Book CreateDefaultBook();
        void CreateBook(Book book);
        void EditBook(Book book);
        void BookmarkBook(Book item, string UserId);
        void DeleteBook(Book book);
        void DeleteBookmark(int id, string UserId);
    }
}
