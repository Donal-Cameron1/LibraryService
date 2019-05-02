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

        //gets all bookmarked items of an user and shows it to the user
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

        //gets all reserved items of an user and shows it to the user
        public ActionResult ShowReservedItemsOfUser(string currentUser)
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

        //gets all laoned items of an user and shows it to the user
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
        //opens a view which displays the selected item with its details
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

        // GET: Books/Reserve/5
        //after the user clicks on reserve a confirmation pagewith the selected item shows up 
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
        //after the reservation got confirmed its changes and saves the item and the user 
        public ActionResult ReserveLibraryItemConfirmed(int id, string method, string contr)
        {
            _libraryItemService.ReserveLibraryItem(id, User.Identity.GetUserId());
            return RedirectToAction(method, contr);
        }

        //deletes a reservation if a user clicks on cancel reservation
        public ActionResult DeleteReservation(int id, string method, string contr)
        {
            _libraryItemService.DeleteReservation(id, User.Identity.GetUserId());
            return RedirectToAction(method, contr);
        }

        //saves an item to the favourites list of an user
        public ActionResult BookmarkLibraryItem(int id, string method, string contr)
        {
            _libraryItemService.BookmarkLibraryItem(id, User.Identity.GetUserId());
            return RedirectToAction(method, contr);
        }

        //removes an item of the favourites list of an user
        public ActionResult DeleteBookmark(int id, string method, string contr)
        {
            _libraryItemService.DeleteBookmark(id, User.Identity.GetUserId());
            return RedirectToAction(method, contr);
        }

        //extends the loaning time by 7 days
        public ActionResult ExtendLoan(int id)
        {
            _libraryItemService.ExtendLoan(id);
            return RedirectToAction("ShowLoanedItemsOfUser");
        }

        //resets the item to available if a book gets returned to the library
        public ActionResult ReturnLibraryItem(int id)
        {
            _libraryItemService.ReturnLibraryItem(id);
            return RedirectToAction("GetLoanedLibraryItems");
        }

    }
}
