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
    public class LibraryItemsController : Controller
    {
        private LibraryContext db = new LibraryContext();

        private ILibraryItemService _libraryItemService;
        public LibraryItemsController()
        {
            _libraryItemService = new LibraryItemService();
        }

        public ActionResult ShowBookmarks(string currentUser)
        {
            // retrieve user
            User user = _libraryItemService.GetUser(User.Identity.GetUserId());

            //show list of bookmarked books
            if (user == null || user.BookmarkedLibraryItems == null || !user.BookmarkedLibraryItems.Any())
            {
                return View(new List<LibraryItem>());
            }
            else
            {
                return View(user.BookmarkedLibraryItems);
            }
        }

        public ActionResult ShowItemsOfUser(string currentUser)
        {
            // retrieve user
            User user = _libraryItemService.GetUser(User.Identity.GetUserId());

            if(user == null || user.ReservedLibraryItems == null || !user.ReservedLibraryItems.Any())
            {
                return View(new List<LibraryItem>());
            }
            else
            {
                return View(user.ReservedLibraryItems);
            }
        }
    }
}
