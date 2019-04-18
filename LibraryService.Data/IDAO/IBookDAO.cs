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
    }
}
