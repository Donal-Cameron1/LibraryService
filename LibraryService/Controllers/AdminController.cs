using LibraryService.DAL;
using LibraryService.Data.Utils;
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

        //Returns a list of loaned library items for the selected user in Staff/Admin View
        public ActionResult GetReservedLibraryItemsOfUser(string id)
        {
            IList<LibraryItem> reservedItems = LibraryItemService.AssignCorrectGenre(_libraryItemService.GetReservedLibraryItemsOfUser(id));
            return View(reservedItems);
        }

        //Returns a list of loaned library items for the selected user in Staff/Admin View
        public ActionResult GetLoanedLibraryItemsOfUser(string id)
        {
            IList<LibraryItem> loanedItems = LibraryItemService.AssignCorrectGenre(_libraryItemService.GetLoanedLibraryItemsOfUser(id));
            return View(loanedItems);
        }

        //Gets all loaned items and filters them by the entered searchstring
        public ActionResult GetLoanedLibraryItems(string searchString)
        {
            IList<LibraryItem> loanedItems = LibraryItemService.AssignCorrectGenre(_libraryItemService.GetLoanedLibraryItems());
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

        //gets all overdue items and filters them by the entered searchString
        public ActionResult GetOverdueLibraryItems(string searchString)
        {
            IList<LibraryItem> overdueItems = LibraryItemService.AssignCorrectGenre(_libraryItemService.GetOverdueLibraryItems());
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

        //Loans an item for the user that has reserved it
        public ActionResult LoanItem(int id, string userId)
        {
            _libraryItemService.LoanLibraryItem(id);
            return RedirectToAction("GetReservedLibraryItemsOfUser", "Admin", new { id = userId });
        }

        public ActionResult ReturnLibraryItem(int id)
        {
            _libraryItemService.ReturnLibraryItem(id);
            return RedirectToAction("GetLoanedLibraryItems");
        }
    }
}