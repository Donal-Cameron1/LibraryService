using LibraryService.Models;
using System.Collections.Generic;

namespace LibraryService.Services.IService
{
    public interface IDVDService
    {
        DVD GetDVD(int id);

        IList<DVD> GetDVDs();

        IList<DVD> GetNewDVDs();

        DVD CreateDefaultDVD();

        void CreateDVD(DVD dvd);

        void UpdateDVD(DVD dvd);

        void DeleteDVD(DVD dvd);

        IList<DVD> DVDTextSearch(IList<DVD> query, string searchString);

        IList<DVD> DVDGenreFilter(IList<DVD> query, string genre);

        IList<DVD> DVDStatusFilter(IList<DVD> query, string status);

        IList<DVD> DVDTypeFilter(IList<DVD> query, string type);

        IList<DVD> DVDLibraryFilter(IList<DVD> query, string library);
    }
}
