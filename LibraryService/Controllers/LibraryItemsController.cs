﻿using LibraryService.DAL;
using LibraryService.Models;
using LibraryService.Services.IService;
using LibraryService.Services.Service;
using Microsoft.AspNet.Identity;
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

        public ActionResult ShowLoanedItemsOfUser(string currentUser)
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

        // GET: Books/Reserve/5
        public ActionResult ReserveDVD(int id)
        {
            DVD dvd = _dvdService.GetDVD(id);
            if (dvd == null)
            {
                return HttpNotFound();
            }
            return View(dvd);
        }

        // POST: DVDs/Reserve/5
        [HttpPost, ActionName("ReserveDVD")]
        [ValidateAntiForgeryToken]
        public ActionResult ReserveDVDConfirmed(int id)
        {
            _dvdService.Reserve(id, User.Identity.GetUserId());
            return RedirectToAction("Searchbar", "Home");
        }

        // GET: Books/Reserve/5
        public ActionResult ReserveBook(int id)
        {
            Book book = _bookService.GetBook(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Reserve/5
        [HttpPost, ActionName("ReserveBook")]
        [ValidateAntiForgeryToken]
        public ActionResult ReserveBookConfirmed(int id)
        {
            _bookService.Reserve(id, User.Identity.GetUserId());
            return RedirectToAction("Searchbar", "Home");
        }

    }
}
