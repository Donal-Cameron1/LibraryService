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
    public class DVDService : IDVDService 
    {
        private IDVDDAO _dvdDAO;
        private IUserDAO _userDAO;
        private DbUtils _dbUtils;

        public DVDService()
        {
            _dvdDAO = new DVDDAO();
            _userDAO = new UserDAO();
            _dbUtils = new DbUtils();
        }

        public void BookmarkDVD(DVD item, string currentUserId)
        {
            // retrieve user
            User user = _userDAO.GetUser(currentUserId);
            //ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.UserId == (int)currentUserId);


            //get item 
            DVD dvd = _dvdDAO.GetDVD(item.id);
            //append item to users bookmark list
            user.BookmarkedBooks.Add(dvd);


            //book.BookmarkedBy.Add(user);
            _userDAO.EditUser(user);
        }

        public DVD CreateDefaultDVD()
        {
            return new DVD()
            {
                Status = Status.Available,
                Type = Models.Type.DVD,
                DateAdded = DateTime.Today
            };
        }

        public void CreateDVD(DVD dvd)
        {
            _dvdDAO.CreateDVD(dvd);
        }

        public void DeleteBookmark(int id, string currentUserId)
        {
            User user = _userDAO.GetUser(currentUserId);
            DVD dvd = _dvdDAO.GetDVD(id);

            user.BookmarkedBooks.Remove(dvd);
            _userDAO.EditUser(user);

        }

        public void DeleteDVD(DVD dvd)
        {
            _dvdDAO.DeleteDVD(dvd);
        }

        public void EditDVD(DVD dvd)
        {
            _dvdDAO.EditDVD(dvd);
        }

        public DVD GetDVD(int id)
        {
            return _dvdDAO.GetDVD(id);

        }
    }
}
