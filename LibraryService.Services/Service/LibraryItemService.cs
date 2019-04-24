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
        private IBookDAO _bookDAO;
        private IDVDDAO _dvdDAO;

        public LibraryItemService()
        {
            //_libraryItemDAO = new LibraryItemDAO();
            _userDAO = new UserDAO();
            _dbUtils = new DbUtils();
            _bookDAO = new BookDAO();
            _dvdDAO = new DVDDAO();
        }

        public User GetUser(string currentUserId)
        {
            User user = _userDAO.GetCurrentUser(currentUserId);
            return user;
        }

        public void UpdateStatus()
        {
            //IList<User> users = _userDAO.GetUsers();
            foreach(User user in _userDAO.GetUsers())
            {
                if (user.ReservedLibraryItems != null)
                {
                    //ICollection<LibraryItem> items = user.ReservedLibraryItems;
                    foreach (LibraryItem item in user.ReservedLibraryItems.ToList())
                    {
                        if (item.ReservedUntil != null && (item.ReservedUntil.Value.AddDays(1).CompareTo(DateTime.Today) <= 0))
                        {

                            item.ReservedUntil = null;
                            item.Status = Status.Available;
                            if (item.Type == Models.Type.Book)
                            {
                                _bookDAO.EditBook((Book)item);
                                _bookDAO.DeleteReservation(item.id, user.UserId);
                            }
                            if (item.Type == Models.Type.DVD)
                            {
                                _dvdDAO.EditDVD((DVD)item);
                                _dvdDAO.DeleteReservation(item.id, user.UserId);
                            }
                        }
                    }
                }
            }
        }
    }
}
