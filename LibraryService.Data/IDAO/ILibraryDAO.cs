using LibraryService.Models;
using System.Linq;

namespace LibraryService.Data.IDAO
{
    public interface ILibraryDAO
    {
        Library GetLibrary(int id);

        void CreateLibrary(Library library);

        void DeleteLibrary(Library library);

        void EditLibrary(Library library);

        IQueryable<Library> GetLibraries();

        IQueryable<Library> SearchLibraries(IQueryable<Library> libraries, string searchString);
    }
}
