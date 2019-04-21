using LibraryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Services.IService
{
    public interface IDVDService
    {
        DVD GetDVD(int id);
        DVD GetDVDWithoutTracking(int id);
        DVD CreateDefaultDVD();
        void CreateDVD(DVD dvd);
        void EditDVD(DVD dvd);
        void DeleteDVD(DVD dvd);
        void BookmarkDVD(DVD item, string UserId);
        void DeleteBookmark(int id, string UserId);
        IList<DVD> GetNewDVDs();
        IList<DVD> DVDTextSearch(IList<DVD> query, string searchString);
        IList<DVD> DVDGenreFilter(IList<DVD> query, string genre);
        IList<DVD> DVDStatusFilter(IList<DVD> query, string status);
        IList<DVD> DVDTypeFilter(IList<DVD> query, string type);
        IList<DVD> GetDVDs();
        void Reserve(DVD dvd, string currentUserId);
    }
}
