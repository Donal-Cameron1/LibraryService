﻿using LibraryService.Models;
using LibraryService.Services.IService;
using System;
using System.Linq;
using System.Web.Mvc;

namespace LibraryService.Controllers
{
    public class LibraryController : Controller
    {
        private ILibraryService _libraryService;
        public LibraryController()
        {
            _libraryService = new Services.Service.LibraryService();
        }

        // GET: Library
        //gets all the libraries and filters them by the entered searchString
        public ActionResult Index(string searchString)
        {
            IQueryable<Library> libraries = _libraryService.GetLibraries();

            if (!String.IsNullOrEmpty(searchString))
            {
                libraries = _libraryService.SearchLibraries(libraries, searchString);
            }

            return View(libraries.ToList());
        }

        // GET: Library/Details/5
        public ActionResult Details(int id)
        {
            Library library = _libraryService.GetLibrary(id);
            if (library == null)
            {
                return HttpNotFound();
            }
            return View(library);
        }

        //returns the partial view for the map display
        public ActionResult Detailspartial()
        {
            return View();
        }

        // GET: Library/Create
        public ActionResult Create()
        {
            return View(_libraryService.CreateDefaultLibrary());
        }

        // POST: Library/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Address,LibraryId,PostCode,Name,Capacity,Phone,OpenHours")] Library library)
        {
            if (ModelState.IsValid)
            {
                _libraryService.CreateLibrary(library);
                return RedirectToAction("Index");
            }

            return View(library);
        }

        // GET: Library/Edit/5
        public ActionResult Edit(int id)
        {
            Library library = _libraryService.GetLibrary(id);
            if (library == null)
            {
                return HttpNotFound();
            }
            return View(library);
        }

        // POST: Library/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Address,LibraryId,PostCode,Name,Capacity,Phone,OpenHours")] Library library)
        {
            if (ModelState.IsValid)
            {
                _libraryService.EditLibrary(library);
                return RedirectToAction("Index");
            }
            return View(library);
        }

        // GET: Library/Delete/5
        public ActionResult Delete(int id)
        {
            Library library = _libraryService.GetLibrary(id);
            if (library == null)
            {
                return HttpNotFound();
            }
            return View(library);
        }

        // POST: Library/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Library library = _libraryService.GetLibrary(id);
            _libraryService.DeleteLibrary(library);
            return RedirectToAction("Index");
        }
    }
}
