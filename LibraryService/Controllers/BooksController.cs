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
using LibraryService.ViewModels;
using Microsoft.AspNet.Identity;
using PagedList;

namespace LibraryService.Controllers
{
    public class BooksController : Controller
    {

        //private LibraryContext db = new LibraryContext();

        private IBookService _bookService;
        private IUserService _userService;
        private ILibraryService _libraryService;

        public BooksController()
        {
            _bookService = new BookService();
            _userService = new UserService();
            _libraryService = new LibrarySiteService();
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

        public ActionResult Index2()
        {
            var viewModel = new List<BooksIndexVIewModel>();
            IList<Book> books = _bookService.GetBooks();

            foreach (var book in books)
            {
                int libraryid = book.LibraryId;
                var thislibrary = _libraryService.GetLibrary(book.LibraryId);
                var indexentry = new BooksIndexVIewModel()
                {
                    book = book,
                    libraryname = _libraryService.GetLibrary(book.LibraryId).Name
                };
                viewModel.Add(indexentry);
            }
            return View(viewModel);
        }


        // GET: Books
        public ActionResult Index(string searchString, string genre, string status, int? page)
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
            if (!bookquery.Any())
            {
                ViewBag.message = "Sorry, we can't find any books";
            }

            var onePageOfBooks = bookquery.ToPagedList(pageNumber, 10);
            ViewBag.OnePageOfBooks = onePageOfBooks;

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
                _bookService.UpdateBook(book);
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
            return RedirectToAction("ShowItemsOfUser", "LibraryItems");
        }

        public ActionResult Bookmark(int id)
        {
            _bookService.BookmarkBook(id, User.Identity.GetUserId());
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
            return View(newBooks);
        }


        
        public ActionResult CheckoutBook()
        {
            IList<Book> bookquery = _bookService.GetBooks();

            ViewBag.Customer = Session["UserId"].ToString();

            return View(bookquery);
        }

        public ActionResult CheckoutAnotherBook(string message)
        {
            IList<Book> bookquery = _bookService.GetBooks();

            ViewBag.Customer = Session["UserId"].ToString();
            ViewBag.message = message;

            return View("CheckoutBook", bookquery);
        }

        public ActionResult SelectBook(int id)
        {
            var book = _bookService.GetBook(id);
            var duedate = DateTime.Now.AddDays(14);
            var listofitems = new Dictionary<Book, DateTime>();

            if (Session["ItemList"] != null)
            {
                listofitems = (Dictionary<Book, DateTime>)Session["ItemList"];
            }

            listofitems.Add(book, duedate);
            Session["ItemList"] = listofitems;

            var message = book.Title + " added to checkout list.";

            return RedirectToAction("CheckoutAnotherBook", new { message = message });

        }

        public ActionResult ShowBasket()
        {
            var itemlist = (Dictionary<Book, DateTime>)Session["ItemList"];

            var viewlist = new Dictionary<string, DateTime>();

            foreach (KeyValuePair<Book, DateTime> item in itemlist)
            {
                viewlist.Add(item.Key.Title, item.Value);
            }

            return View(viewlist);
        }

        public ActionResult Checkout()
        {
            // get the items from session and convert to list of IDs
            var itemlist = (Dictionary<Book, DateTime>)Session["ItemList"];

            var idList = new List<int>();
            foreach (KeyValuePair<Book, DateTime> book in itemlist)
            {
                idList.Add(book.Key.id);
            }

            // get the customer details
            var customerid = Session["UserId"].ToString();
            var thiscustomer = _userService.GetUser(customerid);

            // add the customer ID to each of the books:
            _bookService.LoanBook(idList, customerid);


            //update the customer list of loaned Books
            var currentlist = new Dictionary<Book, DateTime>();

            // if the customer already has books out, get the current list
            if (thiscustomer.LoanedBooks != null)
            {
                currentlist = thiscustomer.LoanedBooks;
            }

            // now add each of the requested books to the list
            foreach (KeyValuePair<Book, DateTime> book in itemlist)
            {
                currentlist.Add(book.Key, book.Value);
            }

            // update the list in the database
            thiscustomer.LoanedBooks = currentlist;
            _userService.EditUser(thiscustomer);

            return View();
        }

        public ActionResult ReturnBooks()
        {
            var customerid = Session["UserId"].ToString();
            var BookList = _bookService.GetBooksForUserID(customerid);
            ViewBag.CustomerID = customerid;
            return View(BookList);
        }

        public ActionResult ReturnBook(int BookID)
        {
            _bookService.ReturnBook(BookID);
            ViewBag.Message = _bookService.GetBook(BookID).Title + " Has Been Returned";
            return RedirectToAction("ReturnBooks");


        }


    }

}
