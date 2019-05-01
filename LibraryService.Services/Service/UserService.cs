using LibraryService.Data.DAL;
using LibraryService.Data.DAO;
using LibraryService.Data.IDAO;
using LibraryService.Models;
using LibraryService.Services.IService;
using System.Collections.Generic;

namespace LibraryService.Services.Service
{
    public class UserService : IUserService
    {
        private IUserDAO _userDAO;
        private DbUtils _dbUtils;

        public UserService()
        {
            _userDAO = new UserDAO();
            _dbUtils = new DbUtils();
        }

        public User GetUser(string id)
        {
            return _userDAO.GetUser(id);
        }

        public IList<User> GetUsers()
        {
            return _userDAO.GetUsers();
        }

        public User CreateDefaultUser()
        {
            return new User();
        }

        public void CreateUser(User user)
        {
            _userDAO.CreateUser(user);
        }

        public void DeleteUser(User user)
        {
            _userDAO.DeleteUser(user);
        }

        public void EditUser(User user)
        {
            _userDAO.EditUser(user);
        }
    }
}
