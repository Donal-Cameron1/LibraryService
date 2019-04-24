using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryService.DAL;
using LibraryService.Models;
using LibraryService.Services.IService;
using LibraryService.Services.Service;
using Microsoft.AspNet.Identity;

namespace LibraryService.Controllers
{
    public class HomeController : Controller
    {
        private LibraryContext db = new LibraryContext();
    
        private ILibraryItemService _libraryItemService;
        private IBookService _bookService;
        private IDVDService _dvdService;

        public HomeController()
        {
            _bookService = new BookService();
            _dvdService = new DVDService();
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

            items = items.Concat(CastBooksToLibraryItems(bookquery)).ToList();
            items = items.Concat(CastDVDsToLibraryItems(dvdquery)).ToList();

            return View(items);

        }

        private static List<LibraryItem> CastDVDsToLibraryItems(IList<DVD> dvdquery)
        {
            List<LibraryItem> items = new List<LibraryItem>();
            foreach (DVD dvd in dvdquery.ToList())
            {
                LibraryItem item = (LibraryItem)dvd;
                item.Genre = (Genre)Enum.Parse(typeof(Genre), dvd.DVDGenre.ToString());
                items.Add(item);           
            }
            return items;
        }

        private static List<LibraryItem> CastBooksToLibraryItems(IList<Book> bookquery)
        {
            List<LibraryItem> items = new List<LibraryItem>();
            foreach (Book book in bookquery.ToList())
            {
                LibraryItem item = (LibraryItem)book;
                item.Genre = (Genre)Enum.Parse(typeof(Genre), book.BookGenre.ToString());
                items.Add(item);
            }
            return items;
        }

        public ActionResult Index()
        {
            IList<LibraryItem> newitems = new List<LibraryItem>();
            IEnumerable<LibraryItem> dvds = CastDVDsToLibraryItems(_dvdService.GetNewDVDs());
            IEnumerable<LibraryItem> books = CastBooksToLibraryItems(_bookService.GetNewBooks());          

            return View(newitems.Concat(books).Concat(dvds));                 
        }

        public ActionResult BookmarkBook(int id)
        {
            _bookService.BookmarkBook(id, User.Identity.GetUserId());
            return RedirectToAction("Searchbar", "Home");
        }

        public ActionResult BookmarkDVD(int id)
        {
            _dvdService.BookmarkDVD(id, User.Identity.GetUserId());
            return RedirectToAction("Searchbar", "Home");
        }

        public ActionResult BookmarkNewBook(int id)
        {
            _bookService.BookmarkBook(id, User.Identity.GetUserId());
            return RedirectToAction("Index", "Home");
        }

        public ActionResult BookmarkNewDVD(int id)
        {
            _dvdService.BookmarkDVD(id, User.Identity.GetUserId());
            return RedirectToAction("Index", "Home");
        }

        // GET: Books/Reserve/5
        public ActionResult ReserveNewDVD(int id)
        {
            DVD dvd = _dvdService.GetDVD(id);
            if (dvd == null)
            {
                return HttpNotFound();
            }
            return View(dvd);
        }

        // POST: DVDs/Reserve/5
        [HttpPost, ActionName("ReserveNewDVD")]
        [ValidateAntiForgeryToken]
        public ActionResult ReserveConfirmed(int id)
        {
            _dvdService.Reserve(id, User.Identity.GetUserId());
            return RedirectToAction("Index");
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