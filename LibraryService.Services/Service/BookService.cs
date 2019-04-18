﻿using LibraryService.Data.IDAO;
using LibraryService.Data.DAO;
using LibraryService.Models;
using LibraryService.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryService.Data.DAL;

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

        public void BookmarkBook(Book item, string currentUserId)
        {
            // retrieve user
            User user = _userDAO.GetCurrentUser(currentUserId);

            //get item 
            Book book = _bookDAO.GetBook(item.id);

            //append item to users bookmark list
            user.BookmarkedBooks.Add(book);

            //book.BookmarkedBy.Add(user);
            _userDAO.EditUser(user);
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

        public void CreateBook(Book book)
        {
            _bookDAO.CreateBook(book);
        }

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

        public void DeleteBookmark(int id, string currentUserId)
        {
            User user = _userDAO.GetCurrentUser(currentUserId);
            Book book = _bookDAO.GetBook(id);

            user.BookmarkedBooks.Remove(book);
            _userDAO.EditUser(user);
        }

        public void EditBook(Book book)
        {
            _bookDAO.EditBook(book);
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
