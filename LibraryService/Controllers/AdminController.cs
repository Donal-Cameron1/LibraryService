using LibraryService.DAL;
using LibraryService.Services.IService;
using LibraryService.Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryService.Controllers
        
{
    [Authorize(Roles = "Admin")]

    public class AdminController : Controller
    {

        private LibraryContext db = new LibraryContext();

        private ILibraryItemService _libraryItemService;
        private IBookService _bookService;
        private IDVDService _dvdService;

        public AdminController()
        {
            _bookService = new BookService();
            _dvdService = new DVDService();
            _libraryItemService = new LibraryItemService();
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

    }

   
}