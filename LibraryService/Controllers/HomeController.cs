using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryService.DAL;
using LibraryService.Models;

namespace LibraryService.Controllers
{
    public class HomeController : Controller
    {
        private LibraryContext db = new LibraryContext();

        public List<Book> BookTextSearch(string searchString)
        {
            var bookItems = from b in db.Books select b;
            bookItems = bookItems.Where(b => b.Title.Contains(searchString)
                                        || b.Author.Contains(searchString));
            return bookItems.ToList();
        }

        public List<Book> BookFilterSearch(string genre)
        {
            var books = from b in db.Books select b;
            books = books.Where(b => b.BookGenre.ToString().Equals(genre));
            return books.ToList();
        } 
        
        public List<DVD> DVDTextSearch(string searchString)
        {
            var dvdItems = from d in db.DVD select d;
            dvdItems = dvdItems.Where(d => d.Title.Contains(searchString)
                                       || d.Director.Contains(searchString));
            return dvdItems.ToList();
        }
        
        public List<DVD> DVDFilterSearch(string genre)
        {
            var dvds = from d in db.DVD select d;
            dvds = dvds.Where(d => d.DVDGenre.ToString().Equals(genre));
            return dvds.ToList();
        }


        public ActionResult Index(string searchString, string genre)
        {
            IList<LibraryItem> items = new List<LibraryItem>();
           
            if (String.IsNullOrEmpty(searchString) && String.IsNullOrEmpty(genre))
            {
                return View(items);
            }
            else if (String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(genre))
            {
                return View(items
                    .Concat(BookFilterSearch(genre).Cast<LibraryItem>())
                    .Concat(DVDFilterSearch(genre).Cast<LibraryItem>())
                    .ToList());
            }
            else if (!String.IsNullOrEmpty(searchString) && String.IsNullOrEmpty(genre))
            {
                return View(items
                    .Concat(BookTextSearch(searchString).Cast<LibraryItem>())
                    .Concat(DVDTextSearch(searchString).Cast<LibraryItem>())
                    .ToList());
            }
            else
            {
                return View(items
                    .Concat((BookTextSearch(searchString).Intersect(BookFilterSearch(genre))).Cast<LibraryItem>())
                    .Concat((DVDTextSearch(searchString).Intersect(DVDFilterSearch(genre))).Cast<LibraryItem>())
                    .ToList());
            }
        }

        public ActionResult About()
        {
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