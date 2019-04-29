using LibraryService.DAL;
using LibraryService.Data.DAL;
using LibraryService.Data.IDAO;
using LibraryService.Models;
using System.Data.Entity;
using System.Linq;

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
            db.Libraries.Attach(library);
            db.Entry(library).State = EntityState.Modified;
            db.SaveChanges();
        }

        //gets all libraries from the database
        public IQueryable<Library> GetLibraries()
        {
            IQueryable<Library> libraries = from s
                                            in db.Libraries
                                            select s;
            return libraries;
        }

        //gets a single library from the database
        public Library GetLibrary(int id)
        {
            IQueryable<Library> libraries = from s
                                            in db.Libraries
                                            where s.LibraryId == id
                                            select s;
            return libraries.First();
        }

        //filters a list of libraries by the entered searchstring
        public IQueryable<Library> SearchLibraries(IQueryable<Library> libraries, string searchString)
        {
            libraries = libraries.Where(s => s.Name.Contains(searchString)
                                       || s.PostCode.Contains(searchString));
            return libraries;
        }
    }
}
