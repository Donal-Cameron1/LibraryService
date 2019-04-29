using LibraryService.DAL;
using LibraryService.Models;
using LibraryService.Services.IService;
using LibraryService.Services.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LibraryService.Controllers
{
    public class HomeController : Controller
    {
        private LibraryContext db = new LibraryContext();

        private ILibraryItemService _libraryItemService;
        private IBookService _bookService;
        private IDVDService _dvdService;
        private IUserService _userService;

        public HomeController()
        {
            _bookService = new BookService();
            _dvdService = new DVDService();
            _userService = new UserService();
            _libraryItemService = new LibraryItemService();
        }

        public IList<Book> BookTextSearch(IList<Book> query, string searchString)
        {
            return _bookService.BookTextSearch(query, searchString);
        }

        public IList<Book> BookGenreFilter(IList<Book> query, string genre)
        {
            return _bookService.BookGenreFilter(query, genre);
        }

        public IList<DVD> DVDTextSearch(IList<DVD> query, string searchString)
        {
            return _dvdService.DVDTextSearch(query, searchString);
        }

        public IList<DVD> DVDGenreFilter(IList<DVD> query, string genre)
        {
            return _dvdService.DVDGenreFilter(query, genre);
        }

        public IList<Book> BookStatusFilter(IList<Book> query, string status)
        {
            return _bookService.BookStatusFilter(query, status);
        }

        public IList<DVD> DVDStatusFilter(IList<DVD> query, string status)
        {
            return _dvdService.DVDStatusFilter(query, status);
        }

        public IList<Book> BookTypeFilter(IList<Book> query, string type)
        {
            return _bookService.BookTypeFilter(query, type);
        }

        public IList<DVD> DVDTypeFilter(IList<DVD> query, string type)
        {
            return _dvdService.DVDTypeFilter(query, type);
        }


        //gets all the books and dvds from the database, filters them by the entered searchString, genre, status and type 
        public ActionResult Searchbar(string searchString, string genre, string status, string type)
        {
            IList<LibraryItem> items = new List<LibraryItem>();
            IList<Book> bookquery = _bookService.GetBooks();
            IList<DVD> dvdquery = _dvdService.GetDVDs();


            if (String.IsNullOrEmpty(searchString) && String.IsNullOrEmpty(genre) && String.IsNullOrEmpty(status) && String.IsNullOrEmpty(type))
            {
                return View(items);
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                bookquery = BookTextSearch(bookquery, searchString);
                dvdquery = DVDTextSearch(dvdquery, searchString);
            }
            if (!String.IsNullOrEmpty(genre))
            {
                bookquery = BookGenreFilter(bookquery, genre);
                dvdquery = DVDGenreFilter(dvdquery, genre);
            }
            if (!String.IsNullOrEmpty(status))
            {
                bookquery = BookStatusFilter(bookquery, status);
                dvdquery = DVDStatusFilter(dvdquery, status);
            }
            if (!String.IsNullOrEmpty(type))
            {
                bookquery = BookTypeFilter(bookquery, type);
                dvdquery = DVDTypeFilter(dvdquery, type);
            }

            //concatenate bookquery and dvdquery to one list of LibraryItems
            items = items.Concat(CastBooksToLibraryItems(bookquery)).Concat(CastDVDsToLibraryItems(dvdquery)).ToList();
            return View(items);

        }

        //casts the dvd to a libraryitem
        private static List<LibraryItem> CastDVDsToLibraryItems(IList<DVD> dvdquery)
        {
            List<LibraryItem> items = new List<LibraryItem>();
            foreach (DVD dvd in dvdquery.ToList())
            {
                LibraryItem item = dvd;
                item.Genre = (Genre)Enum.Parse(typeof(Genre), dvd.DVDGenre.ToString());
                items.Add(item);
            }
            return items;
        }

        //casts a book to libraryitem 
        private static List<LibraryItem> CastBooksToLibraryItems(IList<Book> bookquery)
        {
            List<LibraryItem> items = new List<LibraryItem>();
            foreach (Book book in bookquery.ToList())
            {
                LibraryItem item = book;
                item.Genre = (Genre)Enum.Parse(typeof(Genre), book.BookGenre.ToString());
                items.Add(item);
            }
            return items;
        }

        //gets all items that got added during the last 7 days
        public ActionResult Index()
        {
            IList<LibraryItem> newitems = new List<LibraryItem>();
            IEnumerable<LibraryItem> dvds = CastDVDsToLibraryItems(_dvdService.GetNewDVDs());
            IEnumerable<LibraryItem> books = CastBooksToLibraryItems(_bookService.GetNewBooks());

            return View(newitems.Concat(books).Concat(dvds));
        }

        /*// GET: Books/Details/5
        public ActionResult DetailsBook(int id)
        {
            Book book = _bookService.GetBook(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: DVDs/Details/5
        public ActionResult DetailsDVD(int id)
        {
            DVD dvd = _dvdService.GetDVD(id);
            if (dvd == null)
            {
                return HttpNotFound();
            }
            return View(dvd);
        }*/

        public ActionResult About()
        {
            string currentUserId = User.Identity.GetUserId();
            currentUserId = "a";

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}