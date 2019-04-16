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
using Microsoft.AspNet.Identity;

namespace LibraryService.Controllers
{
    public class BooksController : Controller
    {
        private LibraryContext db = new LibraryContext();

        // GET: Books
        public ActionResult Index(string searchString)
        {
            var books = from s in db.Books
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.Contains(searchString)
                                       || s.Author.Contains(searchString));
            }
            ViewBag.message = "Full Book List";
            return View(books.ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            var model = new Book()
            {
                Status = Status.Available,
                Type = Models.Type.Book,
                DateAdded = DateTime.Today
            };
            return View(model);
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Author,Publisher,Pages,Title,BookGenre,LibraryId,Status,UserId,AgeRestriction,PurchaseValue,ReturnDate,DateAdded,Type,PublishedAt")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Author,id,Publisher,Pages,Title,Genre,LibraryId,Status,UserId,AgeRestriction,PurchaseValue,ReturnDate,DateAdded")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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

        public ActionResult Bookmark(Book item)
        {
           
            string currentUserId = User.Identity.GetUserId();

            // retrieve user
            User user = db.Users.Include(u => u.BookmarkedBooks).SingleOrDefault(x => x.UserId == currentUserId);
            //ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.UserId == (int)currentUserId);

          
            //get item 
            Book book = db.Books.Find(item.id);
            //append item to users bookmark list
            user.BookmarkedBooks.Add(book);
            
                     
            //book.BookmarkedBy.Add(user);
            db.SaveChanges();          
            return RedirectToAction("Index");
        }


        public ActionResult DeleteBookmark(int id)
        {
            var currentUser = User.Identity.GetUserId();
            User user = db.Users.Include(u => u.BookmarkedBooks).Where(u => u.UserId == currentUser).FirstOrDefault();
            Book book = db.Books.Find(id);

            user.BookmarkedBooks.Remove(book);
            db.SaveChanges();

            return RedirectToAction("ShowBookmarks", "LibraryItems");
            //return View(user.BookmarkedBooks);
        }


        public ActionResult NewBooks()
        {
            var baselineDate = DateTime.Now.AddDays(-7);

            return View("Index",db.Books.Where(x => x.DateAdded > baselineDate).OrderByDescending(x => x.DateAdded).ToList());
        }

    }
}
