using LibraryService.DAL;
using LibraryService.Models;
using LibraryService.Services.IService;
using LibraryService.Services.Service;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LibraryService.Controllers
{
    public class DVDsController : Controller
    {
        private LibraryContext db = new LibraryContext();

        private IDVDService _dvdService;
        public DVDsController()
        {
            _dvdService = new DVDService();
        }

        //This provides a search bar allowing the user the option to filter items by specific fields
        public IList<DVD> DVDTextSearch(IList<DVD> query, string searchString)
        {
            return _dvdService.DVDTextSearch(query, searchString);
        }

        public IList<DVD> DVDGenreFilter(IList<DVD> query, string genre)
        {
            return _dvdService.DVDGenreFilter(query, genre);
        }

        public IList<DVD> DVDStatusFilter(IList<DVD> query, string status)
        {
            return _dvdService.DVDStatusFilter(query, status);
        }

        public IList<DVD> DVDLibraryFilter(IList<DVD> query, string library)
        {
            return _dvdService.DVDLibraryFilter(query, library);
        }

        // GET: DVDs
        //gets all the books and filters them by the entered searchString, Genre and Status 
        public ActionResult Index(string searchString, string genre, string status, string library, int? page)
        {
            IList<DVD> dvdquery = _dvdService.GetDVDs();
            var pageNumber = page ?? 1;

            if (!String.IsNullOrEmpty(searchString))
            {
                dvdquery = DVDTextSearch(dvdquery, searchString);
            }
            if (!String.IsNullOrEmpty(genre))
            {
                dvdquery = DVDGenreFilter(dvdquery, genre);
            }
            if (!String.IsNullOrEmpty(status))
            {
                dvdquery = DVDStatusFilter(dvdquery, status);
            }
            if (!String.IsNullOrEmpty(library))
            {
                dvdquery = DVDLibraryFilter(dvdquery, library);
            }


            //show 10 DVDs on one page
            var onePageOfDVDs = dvdquery.ToPagedList(pageNumber, 10);
            ViewBag.onePageOfDVDs = onePageOfDVDs;
            return View();
        }

        // GET: DVDs/Details/
        //Displays all the details about the selected DVD
        public ActionResult Details(int id)
        {
            DVD dvd = _dvdService.GetDVD(id);
            if (dvd == null)
            {
                return HttpNotFound();
            }
            return View(dvd);
        }


        // GET: DVDs/Create
        //returns a view with a form to add a book
        public ActionResult Create()
        {
            return View(_dvdService.CreateDefaultDVD());
        }

        // POST: DVDs/Create
        //saves the attributes of the new book to the database
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Director,Duration,DVDGenre,Title,Publisher,AgeRestriction,PublishedAt,Status,Type,Genre,PurchaseValue,DateAdded,LibraryId,UserId,ReturnDate")] DVD dvd)
        {
            if (ModelState.IsValid)
            {
                dvd.DateAdded = DateTime.Today;
                _dvdService.CreateDVD(dvd);
                return RedirectToAction("Index");
            }
            return View(dvd);
        }

        // GET: DVDs/Edit/5
        //returns a view of a form with the attributes of the book in it and the possibility to edit them 
        public ActionResult Edit(int id)
        {
            DVD dvd = _dvdService.GetDVD(id);
            if (dvd == null)
            {
                return HttpNotFound();
            }
            return View(dvd);
        }

        // POST: DVDs/Edit/5
        //saves the edited book to the database
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Director,Duration,DVDGenre,Title,Publisher,AgeRestriction,PublishedAt,Status,Type,Genre,PurchaseValue,DateAdded,LibraryId,UserId,ReturnDate")] DVD dvd)
        {
            if (ModelState.IsValid)
            {
                _dvdService.UpdateDVD(dvd);
                return RedirectToAction("Index");
            }
            return View(dvd);
        }

        // GET: DVDs/Delete/5
        //returns a delete confirmation page of the seletced book with its attributes
        public ActionResult Delete(int id)
        {
            DVD dvd = _dvdService.GetDVD(id);
            if (dvd == null)
            {
                return HttpNotFound();
            }
            return View(dvd);
        }

        // POST: DVDs/Delete/5
        //removes the selected book from the database
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DVD dvd = _dvdService.GetDVD(id);
            _dvdService.DeleteDVD(dvd);
            return RedirectToAction("Index");
        }

    }
}

