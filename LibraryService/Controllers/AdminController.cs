﻿using LibraryService.DAL;
using LibraryService.Models;
using LibraryService.Services.IService;
using LibraryService.Services.Service;
using LibraryService.utils;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LibraryService.Controllers

{
    //Will only allow admins to access these actions.
    [Authorize(Roles = CustomRoles.AdminOrStaff)]

    public class AdminController : Controller
    {

        private LibraryContext db = new LibraryContext();

        private ILibraryItemService _libraryItemService;
        private IBookService _bookService;
        private IDVDService _dvdService;
        private IUserService _userService;

        public AdminController()
        {
            _bookService = new BookService();
            _dvdService = new DVDService();
            _libraryItemService = new LibraryItemService();
            _userService = new UserService();
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        
        //will get a list of the items that have been reserved by a specifc user.
        public ActionResult GetReservedLibraryItemsOfUser(string id)
        {
            IList<LibraryItem> reservedItems = _libraryItemService.GetReservedLibraryItemsOfUser(id);
            return View(reservedItems);
        }

        //Returns all the loned library items by a specifc user
        public ActionResult GetLoanedLibraryItemsOfUser(string id)
        {
            IList<LibraryItem> loanedItems = _libraryItemService.GetLoanedLibraryItemsOfUser(id);
            return View(loanedItems);
        }

        //Gets all the items loaned by any user.
        public ActionResult GetLoanedLibraryItems(string searchString)
        {
            IList<LibraryItem> loanedItems = _libraryItemService.GetLoanedLibraryItems();
            IList<LibraryItem> items = new List<LibraryItem>();

            if (String.IsNullOrEmpty(searchString))
            {
                return View(loanedItems);
            }
            else
            {
                items = _libraryItemService.TextSearch(loanedItems, searchString);
                return View(items);
            }

        }

        //Gets all the items where the returndate has expired
        public ActionResult GetOverdueLibraryItems(string searchString)
        {
            IList<LibraryItem> overdueItems = _libraryItemService.GetOverdueLibraryItems();
            IList<LibraryItem> items = new List<LibraryItem>();

            if (String.IsNullOrEmpty(searchString))
            {
                return View(overdueItems);
            }
            else
            {
                items = _libraryItemService.TextSearch(overdueItems, searchString);
                return View(items);
            }
        }

        //Loans an item for the user that has reserved it.
        public ActionResult LoanItem(int id, string ReservedBy)
        {
            _libraryItemService.LoanLibraryItem(id);
            return RedirectToAction("Index", "UsersAdmin", new { id = ReservedBy });
        }

        public ActionResult ReturnLibraryItem(int id)
        {
            _libraryItemService.ReturnLibraryItem(id);
            return RedirectToAction("GetLoanedLibraryItems");
        }
    }
}