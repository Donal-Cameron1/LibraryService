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
        public ActionResult Index(string searchString)
        {
            var bookItems = from s in db.Books select s;
            var dvdItems = from d in db.DVD select d;

            if (!String.IsNullOrEmpty(searchString))
            {
                bookItems = bookItems.Where(b => b.Title.Contains(searchString)
                                       || b.Author.Contains(searchString));
                dvdItems = dvdItems.Where(d => d.Title.Contains(searchString)
                                       || d.Director.Contains(searchString));

                IList<LibraryItem> items = new List<LibraryItem>();

                foreach (Book b in bookItems)
                {
                    items.Add((LibraryItem)b);
                }
                foreach (DVD d in dvdItems)
                {
                    items.Add((LibraryItem)d);
                }

                return View(items);
            }
            return View(new List<LibraryItem>());
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