using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryService.DAL;
using LibraryService.Models;
using LibraryService.Services.IService;
using LibraryService.Services.Service;
using Microsoft.AspNet.Identity;
using PagedList;

namespace LibraryService.Controllers
{
    public class BooksController : Controller
    {

        //private LibraryContext db = new LibraryContext();

        private IBookService _bookService;
        public BooksController()
        {
            _bookService = new BookService();
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



        // GET: Books
        public ActionResult Index(string searchString, string genre, string status, int? page)
        {
            //IQueryable<Book> books = _bookService.GetBooks().AsQueryable<Book>();
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
            if (!bookquery.Any())
            {
                ViewBag.message = "Sorry, we can't find any books";
            }

            //string uid = User.Identity.GetUserId();
            //ViewBag.UserId = User.Identity.GetUserId();
            //ViewBag.Return = DateTime.Today.AddDays(14).ToString("dd/MM/yyyy");

            var onePageOfProducts = bookquery.ToPagedList(pageNumber, 2);
            ViewBag.OnePageOfProducts = onePageOfProducts;

            return View();
        }

        // GET: Books/Details/5
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
        public ActionResult Create()
        {
            return View(_bookService.CreateDefaultBook());
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Author,Publisher,Pages,Title,BookGenre,LibraryId,Status,UserId,AgeRestriction,PurchaseValue,ReturnDate,DateAdded,Type,PublishedAt")] Book book)
        {
            if (ModelState.IsValid)
            {
                _bookService.CreateBook(book);
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Books/Edit/5
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Author,id,Publisher,Pages,Title,Genre,LibraryId,Status,UserId,AgeRestriction,PurchaseValue,ReturnDate,DateAdded,PublishedAt,Type")] Book book)
        {
            if (ModelState.IsValid)
            {
                _bookService.EditBook(book);
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Books/Delete/5
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = _bookService.GetBook(id);
            _bookService.DeleteBook(book);
            return RedirectToAction("Index");
        }

        /*protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/

        // GET: Books/Reserve/5
        public ActionResult Reserve(int id)
        {
            Book book = _bookService.GetBook(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: DVDs/Reserve/5
        [HttpPost, ActionName("Reserve")]
        [ValidateAntiForgeryToken]
        public ActionResult ReserveConfirmed(int id)
        {
            _bookService.Reserve(id, User.Identity.GetUserId());
            return RedirectToAction("Index");
        }

        public ActionResult DeleteReservation(int id)
        {
            _bookService.DeleteReservation(id, User.Identity.GetUserId());
            return RedirectToAction("ShowItemsOfUser");
        }
      
        public ActionResult Bookmark(int id)
        {
            _bookService.BookmarkBook(id, User.Identity.GetUserId()); 
            return RedirectToAction("Index");
        }

        public ActionResult BookmarkNewBook(int id)
        {
            _bookService.BookmarkBook(id, User.Identity.GetUserId());
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeleteBookmark(int id)
        {
            _bookService.DeleteBookmark(id, User.Identity.GetUserId());
            return RedirectToAction("ShowBookmarks", "LibraryItems");
        }

        public ActionResult GetNewBooks()
        {
            IList<Book> newBooks = _bookService.GetNewBooks();
            return View(newBooks);
        }

    }
}
