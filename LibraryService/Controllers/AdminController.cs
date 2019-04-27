using LibraryService.DAL;
using LibraryService.Models;
using LibraryService.Services.IService;
using LibraryService.Services.Service;
using LibraryService.utils;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LibraryService.Controllers

{
    [Authorize(Roles = CustomRoles.AdminOrStaff)]
    //[Authorize(Roles = "Admin")]

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

        public ActionResult GetReservedItems(string id)
        {
            IList<LibraryItem> reservedItems = _libraryItemService.GetReservedItems(id);
            return View(reservedItems);
        }

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

        public ActionResult LoanItem(int id, string ReservedBy)
        {
            _libraryItemService.LoanLibraryItem(id);
            return RedirectToAction("GetReservedItems", "Admin", new { id = ReservedBy });
        }



    }


}