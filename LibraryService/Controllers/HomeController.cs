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

        //gets all items that got added during the last 14 days
        public ActionResult Index()
        {
            IList<LibraryItem> newitems = new List<LibraryItem>();
            IEnumerable<LibraryItem> dvds = LibraryItemService.CastDVDsToLibraryItems(_dvdService.GetNewDVDs());
            IEnumerable<LibraryItem> books = LibraryItemService.CastBooksToLibraryItems(_bookService.GetNewBooks());

            return View(newitems.Concat(books).Concat(dvds));
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

        public IList<Book> BookLibraryFilter(IList<Book> query, string library)
        {
            return _bookService.BookLibraryFilter(query, library);
        }

        public IList<DVD> DVDLibraryFilter(IList<DVD> query, string library)
        {
            return _dvdService.DVDLibraryFilter(query, library);
        }

        //gets all the books and dvds from the database, filters them by the entered searchString, genre, status and type 
        public ActionResult Searchbar(string searchString, string genre, string status, string type, string library)
        {
            IList<LibraryItem> items = new List<LibraryItem>();
            IList<Book> bookquery = _bookService.GetBooks();
            IList<DVD> dvdquery = _dvdService.GetDVDs();


            if (String.IsNullOrEmpty(searchString) && String.IsNullOrEmpty(genre) && String.IsNullOrEmpty(status) && String.IsNullOrEmpty(type) && String.IsNullOrEmpty(library))
            {
                items = _libraryItemService.GetLibraryItems();
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
            if (!String.IsNullOrEmpty(library))
            {
                bookquery = BookLibraryFilter(bookquery, library);
                dvdquery = DVDLibraryFilter(dvdquery, library);
            }

            //concatenate bookquery and dvdquery to one list of LibraryItems
            items = items.Concat(LibraryItemService.CastBooksToLibraryItems(bookquery)).Concat(LibraryItemService.CastDVDsToLibraryItems(dvdquery)).ToList();
            return View(items);

        }

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