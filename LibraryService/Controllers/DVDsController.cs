using LibraryService.DAL;
using LibraryService.Models;
using LibraryService.Services.IService;
using LibraryService.Services.Service;
using PagedList;
using System;
using System.Collections.Generic;
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

        // GET: DVDs
        public ActionResult Index(string searchString, string genre, string status, int? page)
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

            //show 10 DVDs on one page
            var onePageOfDVDs = dvdquery.ToPagedList(pageNumber, 10);
            ViewBag.onePageOfDVDs = onePageOfDVDs;
            return View();
        }

        //Displays all the details about the selected DVD
        // GET: DVDs/Details/5
        public ActionResult Details(int id)
        {
            DVD dvd = _dvdService.GetDVD(id);
            if (dvd == null)
            {
                return HttpNotFound();
            }
            return View(dvd);
        }

        //Allows the staff or admin to add a new DVD into the library system
        // GET: DVDs/Create
        public ActionResult Create()
        {
            return View(_dvdService.CreateDefaultDVD());
        }

        // POST: DVDs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Director,Duration,DVDGenre,Title,Publisher,AgeRestriction,PublishedAt,Status,Type,Genre,PurchaseValue,DateAdded,LibraryId,UserId,ReturnDate")] DVD dvd)
        {
            if (ModelState.IsValid)
            {
                _dvdService.CreateDVD(dvd);
                return RedirectToAction("Index");
            }
            return View(dvd);
        }

        //Allow a staff or admin to edit any incorrect fields in the DVD table
        // GET: DVDs/Edit/5
        public ActionResult Edit(int id)
        {
            DVD dvd = _dvdService.GetDVD(id);
            if (dvd == null)
            {
                return HttpNotFound();
            }
            return View(dvd);
        }

        //Posts the changes that have been made to the DVD
        // POST: DVDs/Edit/5
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

        //Ability to delete a specifc DVD from the database
        // GET: DVDs/Delete/5
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

