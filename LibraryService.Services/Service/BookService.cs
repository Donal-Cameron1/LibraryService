using LibraryService.Data.DAL;
using LibraryService.Data.DAO;
using LibraryService.Data.IDAO;
using LibraryService.Models;
using LibraryService.Services.IService;
using System;
using System.Collections.Generic;

namespace LibraryService.Services.Service
{
    public class BookService : IBookService
    {
        private IBookDAO _bookDAO;
        private IUserDAO _userDAO;
        private DbUtils _dbUtils;

        public BookService()
        {
            _bookDAO = new BookDAO();
            _userDAO = new UserDAO();
            _dbUtils = new DbUtils();
        }

        public IList<Book> BookGenreFilter(IList<Book> query, string genre)
        {
            return _bookDAO.BookGenreFilter(query, genre);
        }

        public IList<Book> BookStatusFilter(IList<Book> query, string status)
        {
            return _bookDAO.BookStatusFilter(query, status);
        }

        public IList<Book> BookTextSearch(IList<Book> query, string searchString)
        {
            return _bookDAO.BookTextSearch(query, searchString);
        }

        public IList<Book> BookTypeFilter(IList<Book> query, string type)
        {
            return _bookDAO.BookTypeFilter(query, type);
        }

        public IList<Book> BookLibraryFilter(IList<Book> query, string library)
        {
            return _bookDAO.BookLibraryFilter(query, library);
        }

        public void CreateBook(Book book)
        {
            _bookDAO.CreateBook(book);
        }

        // Pre set information for when a book is added.
        public Book CreateDefaultBook()
        {
            return new Book()
            {
                Status = Status.Available,
                Type = Models.Type.Book,
                DateAdded = DateTime.Today
            };
        }

        public void DeleteBook(Book book)
        {
            _bookDAO.DeleteBook(book);
        }

        public void UpdateBook(Book book)
        {
            _bookDAO.UpdateBook(book);
        }

        public Book GetBook(int id)
        {
            return _bookDAO.GetBook(id);
        }

        public IList<Book> GetBooks()
        {
            return _bookDAO.GetBooks();
        }

        public IList<Book> GetNewBooks()
        {
            return _bookDAO.GetNewBooks();
        }

       
    }
}
