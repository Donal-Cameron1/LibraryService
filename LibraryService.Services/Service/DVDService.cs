using LibraryService.Data.DAL;
using LibraryService.Data.DAO;
using LibraryService.Data.IDAO;
using LibraryService.Models;
using LibraryService.Services.IService;
using System;
using System.Collections.Generic;

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

        public void BookmarkDVD(int id, string currentUserId)
        {
            _dvdDAO.BookmarkDVD(id, currentUserId);
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
            _dvdDAO.DeleteBookmark(id, currentUserId);
        }

        public void DeleteDVD(DVD dvd)
        {
            _dvdDAO.DeleteDVD(dvd);
        }

        public IList<DVD> DVDGenreFilter(IList<DVD> query, string genre)
        {
            return _dvdDAO.DVDGenreFilter(query, genre);
        }

        public IList<DVD> DVDStatusFilter(IList<DVD> query, string status)
        {
            return _dvdDAO.DVDStatusFilter(query, status);
        }

        public IList<DVD> DVDTextSearch(IList<DVD> query, string searchString)
        {
            return _dvdDAO.DVDTextSearch(query, searchString);
        }

        public IList<DVD> DVDTypeFilter(IList<DVD> query, string type)
        {
            return _dvdDAO.DVDTypeFilter(query, type);
        }

        public void UpdateDVD(DVD dvd)
        {
            _dvdDAO.UpdateDVD(dvd);
        }

        public DVD GetDVD(int id)
        {
            return _dvdDAO.GetDVD(id);
        }

        public IList<DVD> GetDVDs()
        {
            return _dvdDAO.GetDVDs();
        }

        public IList<DVD> GetNewDVDs()
        {
            return _dvdDAO.GetNewDVDs();
        }

        public void Reserve(int id, string currentUserId)
        {
            DVD dvd = _dvdDAO.GetDVD(id);

            if (dvd.Status == Status.Available)
            {
                dvd.Status = Status.Reserved;
                dvd.ReservedUntil = DateTime.Today.AddDays(5);
                _dvdDAO.UpdateDVD(dvd);
                _dvdDAO.ReserveDVD(id, currentUserId);
            }
            if (dvd.Status == Status.Loaned)
            {
                dvd.ReservedUntil = dvd.ReturnDate.Value.AddDays(5);
                _dvdDAO.UpdateDVD(dvd);
                _dvdDAO.ReserveDVD(id, currentUserId);
            }
        }

        public void DeleteReservation(int id, string currentUserId)
        {
            _dvdDAO.DeleteReservation(id, currentUserId);
        }
    }
}
