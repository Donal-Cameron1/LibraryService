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

namespace LibraryService.Controllers
{
    public class BooksController : Controller
    {

        private LibraryContext db = new LibraryContext();

        private IBookService _bookService;
        public BooksController()
        {
            _bookService = new BookService();
        }

        // GET: Books
        public ActionResult Index(string searchString)
        {
            var books = from s in db.Books
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.Contains(searchString)
                                       || s.Author.Contains(searchString));
            }
            ViewBag.message = "Full Book List";
            string uid = User.Identity.GetUserId();
            ViewBag.UserId = User.Identity.GetUserId();
            ViewBag.Return = DateTime.Today.AddDays(14).ToString("dd/MM/yyyy");
            return View(books.ToList());
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Reserve(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = User.Identity.GetUserId();
            ViewBag.Return = DateTime.Today.AddDays(14).ToString("dd/MM/yyyy");
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reserve([Bind(Include = "Author,id,Publisher,Pages,Title,Genre,LibraryId,Status,UserId,AgeRestriction,PurchaseValue,ReturnDate,DateAdded")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        public ActionResult MyBooks()
        {
            string uid = User.Identity.GetUserId();
            return View("Index", db.Books.Where(d => d.UserId == uid));

        }
      
        public ActionResult Bookmark(Book item)
        {
            _bookService.BookmarkBook(item, User.Identity.GetUserId()); 
            return RedirectToAction("Index");
        }


        public ActionResult DeleteBookmark(int id)
        {
            _bookService.DeleteBookmark(id, User.Identity.GetUserId());
            return RedirectToAction("ShowBookmarks", "LibraryItems");
        }


        public ActionResult GetNewBooks()
        {
            IList<Book> newBooks = _bookService.GetNewBooks();
            ViewBag.Message = "New Books added this week!";
            return View("Index", newBooks);
        }

    }
}
