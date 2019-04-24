using LibraryService.DAL;
using LibraryService.Data.DAL;
using LibraryService.Data.IDAO;
using LibraryService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Data.DAO
{
    public class LibraryDAO : ILibraryDAO
    {
        private LibraryContext db = DbUtils.db;

        public void CreateLibrary(Library library)
        {
            db.Libraries.Add(library);
            db.SaveChanges();
        }

        public void DeleteLibrary(Library library)
        {
            db.Libraries.Remove(library);
            db.SaveChanges();
        }

        public void EditLibrary(Library library)
        {
            db.Entry(library).State = EntityState.Modified;
            db.SaveChanges();
        }

        public IQueryable<Library> GetLibraries()
        {
            IQueryable<Library> libraries = from s 
                                            in db.Libraries
                                            select s;
            return libraries;
        }

        public Library GetLibrary(string id)
        {
            return db.Libraries.Find(id);
        }

        public IQueryable<Library> SearchLibraries(IQueryable<Library> libraries, string searchString)
        {
            libraries = libraries.Where(s => s.Name.Contains(searchString)
                                       || s.PostCode.Contains(searchString));
            return libraries;
        }

        /*public IQueryable<Library> SearchLibraries(IQueryable<Library> libraries, string searchString)
        {
            libraries = libraries.Where(s => s.Name.Contains(searchString)
                                       || s.PostCode.Contains(searchString));
            return libraries;
        }*/
    }
}
