using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryService.DAL;
using LibraryService.Models;


namespace LibraryService.Data.IDAO
{
    public interface IDVDDAO
    {
        DVD GetDVD(int id);
        void CreateDVD(DVD dvd);
        void EditDVD(DVD dvd);
        void DeleteDVD(DVD dvd);
        void BookmarkDVD(int id, string UserId);
        void ReserveDVD(int id, string UserId);
        IList<DVD> GetNewDVDs();
        IList<DVD> DVDGenreFilter(IList<DVD> query, string genre);
        IList<DVD> DVDStatusFilter(IList<DVD> query, string status);
        IList<DVD> DVDTextSearch(IList<DVD> query, string searchString);
        IList<DVD> DVDTypeFilter(IList<DVD> query, string type);
        IList<DVD> GetDVDs();
        void LoanDVD(int id, string UserId);
        void DeleteReservation(int id, string userId);
        void DeleteBookmark(int id, string currentUserId);
    }
}
