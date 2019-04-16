﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryService.DAL;
using LibraryService.Models;
using Microsoft.AspNet.Identity;

namespace LibraryService.Controllers
{
    public class DVDsController : Controller
    {
        private LibraryContext db = new LibraryContext();

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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DVD dvd = db.DVD.Find(id);
            if (dvd == null)
            {
                return HttpNotFound();
            }
            return View(dvd);
        }

        // GET: DVDs/Create
        public ActionResult Create()
        {
            var model = new DVD()
            {
                Status = Status.Available,
                Type = Models.Type.Book,
                DateAdded = DateTime.Today
            };
            return View(model);
        }

        // POST: DVDs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Director,Duration,DVDGenre,Title,Publisher,AgeRestriction,PublishedAt,Status,Type,Genre,PurchaseValue,DateAdded,LibraryId,UserId,ReturnDate")] DVD dVD)
        {
            if (ModelState.IsValid)
            {
                db.DVD.Add(dVD);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dVD);
        }

        // GET: DVDs/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DVD dVD = db.DVD.Find(id);
            if (dVD == null)
            {
                return HttpNotFound();
            }
            return View(dVD);
        }

        // POST: DVDs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Director,Duration,DVDGenre,Title,Publisher,AgeRestriction,PublishedAt,Status,Type,Genre,PurchaseValue,DateAdded,LibraryId,UserId,ReturnDate")] DVD dVD)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dVD).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dVD);
        }

        // GET: DVDs/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DVD dVD = db.DVD.Find(id);
            if (dVD == null)
            {
                return HttpNotFound();
            }
            return View(dVD);
        }

        // POST: DVDs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DVD dVD = db.DVD.Find(id);
            db.DVD.Remove(dVD);
            db.SaveChanges();
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
        public ActionResult NewDVDs()

        {
            var baselineDate = DateTime.Now.AddDays(-7);
            ViewBag.Message = "New DVDs added this week!";

            return View("Index", db.DVD.Where(x => x.DateAdded > baselineDate).OrderByDescending(x => x.DateAdded).ToList());
        }
        public ActionResult Reserve(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
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

            string currentUserId = User.Identity.GetUserId();

            // retrieve user
            User user = db.Users.Include(u => u.BookmarkedBooks).SingleOrDefault(x => x.UserId == currentUserId);
            //ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.UserId == (int)currentUserId);


            DVD dvd = db.DVD.Find(item.id);
            //append item to users bookmark list
            user.BookmarkedBooks.Add(dvd);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteBookmark(int id)
        {
            var currentUser = User.Identity.GetUserId();
            User user = db.Users.Include(u => u.BookmarkedBooks).Where(u => u.UserId == currentUser).FirstOrDefault();
            DVD dvd = db.DVD.Find(id);

            user.BookmarkedBooks.Remove(dvd);
            db.SaveChanges();

            return RedirectToAction("ShowBookmarks", "LibraryItems");
            //return View(user.BookmarkedBooks);
        }
    }
}

