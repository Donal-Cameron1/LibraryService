using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryService.DAL;
using LibraryService.Models;
using Microsoft.AspNet.Identity;

namespace LibraryService.Controllers
{
    public class HomeController : Controller
    {
        private LibraryContext db = new LibraryContext();
        

        public IQueryable<Book> BookTextSearch(IQueryable<Book> query, string searchString)
        {                       
            return query.Where(b => b.Title.Contains(searchString)
                                        || b.Author.Contains(searchString));          
        }

        public IQueryable<Book> BookGenreFilter(IQueryable<Book> query, string genre)
        {
            return query.Where(b => b.BookGenre.ToString().Equals(genre));
        } 
        
        public IQueryable<DVD> DVDTextSearch(IQueryable<DVD> query, string searchString)
        {
            return query.Where(d => d.Title.Contains(searchString)
                                        || d.Director.Contains(searchString));
        }
        
        public IQueryable<DVD> DVDGenreFilter(IQueryable<DVD> query, string genre)
        {
            return query.Where(d => d.DVDGenre.ToString().Equals(genre));
        }

        public IQueryable<Book> BookStatusFilter(IQueryable<Book> query, string status)
        {
            return query.Where(b => b.Status.ToString().Equals(status));
        }

        public IQueryable<DVD> DVDStatusFilter(IQueryable<DVD> query, string status)
        {
            return query.Where(d => d.Status.ToString().Equals(status));
        }

        public IQueryable<Book> BookTypeFilter(IQueryable<Book> query, string type)
        {
            return query.Where(d => d.Type.ToString().Equals(type));
        }

        public IQueryable<DVD> DVDTypeFilter(IQueryable<DVD> query, string type)
        {
            return query.Where(d => d.Type.ToString().Equals(type));
        }



        public ActionResult Searchbar(string searchString, string genre, string status, string type)
        {
            IList<LibraryItem> items = new List<LibraryItem>();
            var bookquery = from b in db.Books select b;
            var dvdquery = from d in db.DVD select d;

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

            foreach (Book book in bookquery.ToList())
            {
                var item = (LibraryItem)book;
                item.Genre = (Genre) Enum.Parse(typeof(Genre),book.BookGenre.ToString());
                items.Add(item);
            }

            foreach (DVD dvd in dvdquery.ToList())
            {
                var item = (LibraryItem)dvd;
                item.Genre = (Genre)Enum.Parse(typeof(Genre), dvd.DVDGenre.ToString());
                items.Add(item);
            }

            return View(items);
            
        }

        public ActionResult Index()
        {
            var baselineDate = DateTime.Now.AddDays(-7);
            IList<LibraryItem> newitems = new List<LibraryItem>();

            IEnumerable<LibraryItem> books = db.Books.Where(x => x.DateAdded > baselineDate).OrderByDescending(x => x.DateAdded).ToList().Cast<LibraryItem>();
            IEnumerable<LibraryItem> dvds = db.DVD.Where(x => x.DateAdded > baselineDate).OrderByDescending(x => x.DateAdded).ToList().Cast<LibraryItem>();

            return View(newitems.Concat(books).Concat(dvds));                 
        }

        public ActionResult About()
        {
            string currentUserId = User.Identity.GetUserId(); 
            currentUserId = "a";
            //ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.UserId == (int)currentUserId);
            
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