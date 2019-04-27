using LibraryService.Models;
using System.Linq;

namespace LibraryService.Services.IService
{
    public interface ILibraryService
    {
        Library GetLibrary(int id);

        Library CreateDefaultLibrary();

        void CreateLibrary(Library library);

        void DeleteLibrary(Library library);

        void EditLibrary(Library library);

        IQueryable<Library> GetLibraries();

        IQueryable<Library> SearchLibraries(IQueryable<Library> libraries, string searchString);
    }
}
