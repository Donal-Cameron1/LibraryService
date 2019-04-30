using LibraryService.Models;
using LibraryService.Services.IService;
using LibraryService.Services.Service;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LibraryService.Controllers
{
    public class BooksController : Controller
    {
        private IBookService _bookService;
        private IUserService _userService;
        private ILibraryService _libraryService;

        public BooksController()
        {
            _bookService = new BookService();
            _userService = new UserService();
            _libraryService = new Services.Service.LibraryService();
        }

        public IList<Book> BookTextSearch(IList<Book> query, string searchString)
        {
            return _bookService.BookTextSearch(query, searchString);
        }

        public IList<Book> BookGenreFilter(IList<Book> query, string genre)
        {
            return _bookService.BookGenreFilter(query, genre);
        }

        public IList<Book> BookStatusFilter(IList<Book> query, string status)
        {
            return _bookService.BookStatusFilter(query, status);
        }

        public IList<Book> BookLibraryFilter(IList<Book> query, string library)
        {
            return _bookService.BookLibraryFilter(query, library);
        }


        // GET: Books
        //gets all the books and filters them by the entered searchString, Genre and Status 
        public ActionResult Index(string searchString, string genre, string status, string library, int? page)
        {
            IList<Book> bookquery = _bookService.GetBooks();
            var pageNumber = page ?? 1;

            if (!String.IsNullOrEmpty(searchString))
            {
                bookquery = BookTextSearch(bookquery, searchString);
            }
            if (!String.IsNullOrEmpty(genre))
            {
                bookquery = BookGenreFilter(bookquery, genre);
            }
            if (!String.IsNullOrEmpty(status))
            {
                bookquery = BookStatusFilter(bookquery, status);
            }
            if (!String.IsNullOrEmpty(library))
            {
                bookquery = BookLibraryFilter(bookquery, library);
            }
           

            //show 10 Books on one page
            var onePageOfBooks = bookquery.ToPagedList(pageNumber, 10);
            ViewBag.OnePageOfBooks = onePageOfBooks;

            return View();
        }

        // GET: Books/Details/5
        //returns a view of the selected book and its details 
        public ActionResult Details(int id)
        {
            Book book = _bookService.GetBook(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        //returns a view with a form to add a book
        public ActionResult Create()
        {
            return View(_bookService.CreateDefaultBook());
        }

        // POST: Books/Create
        //saves the attributes of the new book to the database
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Author,Publisher,Pages,Title,BookGenre,LibraryId,Status,UserId,AgeRestriction,PurchaseValue,ReturnDate,DateAdded,Type,PublishedAt")] Book book)
        {
            if (ModelState.IsValid)
            {
                book.DateAdded = DateTime.Today;
                _bookService.CreateBook(book);
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Books/Edit/5
        //returns a view of a form with the attributes of the book in it and the possibility to edit them 
        public ActionResult Edit(int id)
        {
            Book book = _bookService.GetBook(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        //saves the edited book to the database
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Author,id,Publisher,Pages,Title,Genre,LibraryId,Status,UserId,AgeRestriction,PurchaseValue,ReturnDate,DateAdded,PublishedAt,Type")] Book book)
        {
            if (ModelState.IsValid)
            {
                _bookService.UpdateBook(book);
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Books/Delete/5
        //returns a delete confirmation page of the seletced book with its attributes
        public ActionResult Delete(int id)
        {
            Book book = _bookService.GetBook(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        //removes the selected book from the database
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = _bookService.GetBook(id);
            _bookService.DeleteBook(book);
            return RedirectToAction("Index");
        }

    }
}

