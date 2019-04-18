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

        // GET: DVDs
        public ActionResult Index()
        {
            ViewBag.message = "Full List of DVDs";
            ViewBag.UserId = User.Identity.GetUserId();
            ViewBag.Return = DateTime.Today.AddDays(14).ToString("dd/MM/yyyy");
            string uid = User.Identity.GetUserId();
            return View(db.DVD.ToList());
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
            ViewBag.Message = "New DVDs added this week!";
            return View("Index", newDVDs);
        }

        public ActionResult Reserve(int id)
        {
            DVD dVD = db.DVD.Find(id);
            if (dVD == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = User.Identity.GetUserId();
            ViewBag.Return = DateTime.Today.AddDays(14).ToString("dd/MM/yyyy");
            return View(dVD);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reserve([Bind(Include = "id,Director,Duration,DVDGenre,Title,Publisher,AgeRestriction,PublishedAt,Status,Type,Genre,PurchaseValue,DateAdded,LibraryId,UserId,ReturnDate")] DVD dVD)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dVD).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dVD);
        }

        public ActionResult MyDVDs()
        {
            string uid = User.Identity.GetUserId();
            return View("Index", db.DVD.Where(d => d.UserId == uid));

        }

        public ActionResult Bookmark(DVD item)
        {
            _dvdService.BookmarkDVD(item, User.Identity.GetUserId());
            return RedirectToAction("Index");
        }

        public ActionResult DeleteBookmark(int id)
        {
            _dvdService.DeleteBookmark(id, User.Identity.GetUserId());
            return RedirectToAction("ShowBookmarks", "LibraryItems");
        }
    }
}

