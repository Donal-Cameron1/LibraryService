using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryService.Models;

namespace LibraryService.Data.IDAO
{
    public interface ILibraryDAO
    {
        Library GetLibrary(string id);
        void CreateLibrary(Library library);
        void DeleteLibrary(Library library);
        void EditLibrary(Library library);
        IQueryable<Library> GetLibraries();
        IQueryable<Library> SearchLibraries(IQueryable<Library> libraries, string searchString);
    }
}
