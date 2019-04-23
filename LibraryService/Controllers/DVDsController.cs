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
using PagedList;

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
            //IQueryable<DVD> dvds = _dvdService.GetDVDs().AsQueryable<DVD>();
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

            //ViewBag.UserId = User.Identity.GetUserId();
            //ViewBag.Return = DateTime.Today.AddDays(14).ToString("dd/MM/yyyy");
            //string uid = User.Identity.GetUserId();

            var onePageOfDVDs = dvdquery.ToPagedList(pageNumber, 10);
            ViewBag.onePageOfDVDs = onePageOfDVDs;
            return View(dvdquery);
        }

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

        // POST: DVDs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Director,Duration,DVDGenre,Title,Publisher,AgeRestriction,PublishedAt,Status,Type,Genre,PurchaseValue,DateAdded,LibraryId,UserId,ReturnDate")] DVD dvd)
        {
            if (ModelState.IsValid)
            {
                _dvdService.EditDVD(dvd);
                return RedirectToAction("Index");
            }
            return View(dvd);
        }

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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult GetNewDVDs()
        {
            IList<DVD> newDVDs = _dvdService.GetNewDVDs();
            return View(newDVDs);
        }

        // GET: Books/Reserve/5
        public ActionResult Reserve(int id)
        {
            DVD dvd = _dvdService.GetDVD(id);
            if (dvd == null)
            {
                return HttpNotFound();
            }
            return View(dvd);
        }

        // POST: DVDs/Reserve/5
        [HttpPost, ActionName("Reserve")]
        [ValidateAntiForgeryToken]
        public ActionResult ReserveConfirmed(int id)
        {
            _dvdService.Reserve(id, User.Identity.GetUserId());
            return RedirectToAction("Index");
        }

        public ActionResult Bookmark(int id)
        {
            _dvdService.BookmarkDVD(id, User.Identity.GetUserId());
            return RedirectToAction("Index");
        }

        public ActionResult DeleteBookmark(int id)
        {
            _dvdService.DeleteBookmark(id, User.Identity.GetUserId());
            return RedirectToAction("ShowBookmarks", "LibraryItems");
        }
    }
}

