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
        private LibraryContext db = DbUtils.db;

        public void CreateBook(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
        }

        public void DeleteBook(Book book)
        {
            db.Books.Remove(book);
            db.SaveChanges();
        }

        public void EditBook(Book book)
        {
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();
        }

        public Book GetBook(int id)
        {
            return db.Books.AsNoTracking().Where(b => b.id == id).FirstOrDefault();
        }
    }
}
