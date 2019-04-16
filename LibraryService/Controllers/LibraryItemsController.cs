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
using Microsoft.AspNet.Identity;

namespace LibraryService.Controllers
{
    public class LibraryItemsController : Controller
    {
        private LibraryContext db = new LibraryContext();

        public ActionResult ShowBookmarks()
        {
            // retrieve user
            var currentUser = User.Identity.GetUserId();
            User user = db.Users.Include(u => u.BookmarkedBooks).Where(u => u.UserId == currentUser).FirstOrDefault();

            //show list of bookmarked books
            if (user == null || user.BookmarkedBooks == null || !user.BookmarkedBooks.Any())
            {
                return View(new List<LibraryItem>());
            }
            else
            {
                return View(user.BookmarkedBooks);
            }
        }
    }
}
