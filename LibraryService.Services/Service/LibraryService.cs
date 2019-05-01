using LibraryService.Data.DAL;
using LibraryService.Data.DAO;
using LibraryService.Data.IDAO;
using LibraryService.Models;
using LibraryService.Services.IService;
using System.Linq;

namespace LibraryService.Services.Service
{
    public class LibraryService : ILibraryService
    {
        private ILibraryDAO _libraryDAO;
        private IUserDAO _userDAO;
        private DbUtils _dbUtils;

        public LibraryService()
        {
            _libraryDAO = new LibraryDAO();
            _userDAO = new UserDAO();
            _dbUtils = new DbUtils();
        }

        public IQueryable<Library> GetLibraries()
        {
            return _libraryDAO.GetLibrariesQueryable();
        }

        public Library GetLibrary(int id)
        {
            return _libraryDAO.GetLibrary(id);
        }

        public Library CreateDefaultLibrary()
        {
            return new Library();
        }

        public void CreateLibrary(Library library)
        {
            _libraryDAO.CreateLibrary(library);
        }

        public void DeleteLibrary(Library library)
        {
            _libraryDAO.DeleteLibrary(library);
        }

        public void EditLibrary(Library library)
        {
            _libraryDAO.EditLibrary(library);
        }

        public IQueryable<Library> SearchLibraries(IQueryable<Library> libraries, string searchString)
        {
            return _libraryDAO.SearchLibraries(libraries, searchString);
        }
    }
}
