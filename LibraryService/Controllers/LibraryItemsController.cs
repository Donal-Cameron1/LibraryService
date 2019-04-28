﻿using LibraryService.DAL;
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
    public class LibraryItemsController : Controller
    {
        private LibraryContext db = new LibraryContext();

        private ILibraryItemService _libraryItemService;
        private IUserService _userService;
        private IBookService _bookService;
        private IDVDService _dvdService;

        public LibraryItemsController()
        {
            _bookService = new BookService();
            _dvdService = new DVDService();
            _libraryItemService = new LibraryItemService();
            _userService = new UserService();
        }

        public ActionResult ShowBookmarks(string currentUser)
        {
            // retrieve user
            User user = _userService.GetUser(User.Identity.GetUserId());

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
            User user = _userService.GetUser(User.Identity.GetUserId());

            if (user == null || user.ReservedLibraryItems == null || !user.ReservedLibraryItems.Any())
            {
                return View(new List<LibraryItem>());
            }
            else
            {
                return View(user.ReservedLibraryItems);
            }
        }

        public ActionResult ShowLoanedItemsOfUser(string currentUser, string searchString)
        {
            User user = _userService.GetUser(User.Identity.GetUserId());

            if (user == null || user.LoanedLibraryItems == null || !user.LoanedLibraryItems.Any())
            {
                return View(new List<LibraryItem>());
            }
            else
            {
                return View(user.LoanedLibraryItems);
            }
        }


        // GET: Books/Details/5
        public ActionResult DetailsLibraryItem(int id, string method, string contr)
        {
            LibraryItem libraryItem = _libraryItemService.GetLibraryItem(id);
            if (libraryItem == null)
            {
                return HttpNotFound();
            }

            ViewBag.method = method;
            ViewBag.contr = contr;
            ViewBag.id = id;

            if (libraryItem.Type == Models.Type.Book)
            {
                return View("DetailsLibraryItemBook", (Book)libraryItem);
            }
            else
            {
                return View("DetailsLibraryItemDVD", (DVD)libraryItem);
            }
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
        }

        // GET: Books/Reserve/5
        public ActionResult ReserveLibraryItem(int id, string method, string contr)
        {
            LibraryItem libraryItem = _libraryItemService.GetLibraryItem(id);
            if (libraryItem == null)
            {
                return HttpNotFound();
            }

            ViewBag.method = method;
            ViewBag.contr = contr;
            ViewBag.id = id;

            if (libraryItem.Type == Models.Type.Book)
            {
                return View("ReserveLibraryItemBook", (Book)libraryItem);
            }
            else
            {
                return View("ReserveLibraryItemDVD", (DVD)libraryItem);
            }
        }


        // POST: DVDs/Reserve/5
        //[HttpPost, ActionName("ReserveLibraryItem")]
        //[ValidateAntiForgeryToken]
        public ActionResult ReserveLibraryItemConfirmed(int id, string method, string contr)
        {
            _libraryItemService.ReserveLibraryItem(id, User.Identity.GetUserId());
            return RedirectToAction(method, contr);
        }

        public ActionResult DeleteReservation(int id, string method, string contr)
        {
            _libraryItemService.DeleteReservation(id, User.Identity.GetUserId());
            return RedirectToAction(method, contr);
        }

        public ActionResult BookmarkLibraryItem(int id, string method, string contr)
        {
            _libraryItemService.BookmarkLibraryItem(id, User.Identity.GetUserId());
            return RedirectToAction(method, contr);
        }

        public ActionResult DeleteBookmark(int id, string method, string contr)
        {
            _libraryItemService.DeleteBookmark(id, User.Identity.GetUserId());
            return RedirectToAction(method, contr);
        }

    }
}
