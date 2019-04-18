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
    public class LibraryItemService : ILibraryItemService
    {
        private ILibraryItemDAO _libraryItemDAO;
        private IUserDAO _userDAO;
        private DbUtils _dbUtils;

        public LibraryItemService()
        {
            _libraryItemDAO = new LibraryItemDAO();
            _userDAO = new UserDAO();
            _dbUtils = new DbUtils();
        }

        public User GetUser(string currentUserId)
        {
            User user = _userDAO.GetCurrentUser(currentUserId);
            return user;
        }
    }
}
