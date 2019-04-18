using LibraryService.DAL;
using LibraryService.Data.DAL;
using LibraryService.Data.IDAO;
using LibraryService.Models;
using System;
using System.Collections.Generic;
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

        public Library GetLibrary(string id)
        {
            return db.Libraries.Find(id);
        }
    }
}
