using LibraryService.Data.DAL;
using LibraryService.Data.DAO;
using LibraryService.Data.IDAO;
using LibraryService.Models;
using LibraryService.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Services.Service
{
    public class LibrarySiteService : ILibraryService
    {
        private ILibraryDAO _libraryDAO;
        private IUserDAO _userDAO;
        private DbUtils _dbUtils;

        public LibrarySiteService()
        {
            _libraryDAO = new LibraryDAO();
            _userDAO = new UserDAO();
            _dbUtils = new DbUtils();
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

        public IQueryable<Library> GetLibraries()
        {
            return _libraryDAO.GetLibraries();
        }

        public Library GetLibrary(int id)
        {
            return _libraryDAO.GetLibrary(id);
        }

        /*public IQueryable<Library> SearchLibraries(IQueryable<Library> libraries, string searchString)
        {
            return _libraryDAO.SearchLibraries(libraries, searchString);
        }*/
    }
}
