using LibraryService.Models;
using System.Collections.Generic;


namespace LibraryService.Data.IDAO
{
    public interface IDVDDAO
    {
        DVD GetDVD(int id);

        void CreateDVD(DVD dvd);

        void UpdateDVD(DVD dvd);

        void DeleteDVD(DVD dvd);

        IList<DVD> GetNewDVDs();

        IList<DVD> DVDGenreFilter(IList<DVD> query, string genre);

        IList<DVD> DVDStatusFilter(IList<DVD> query, string status);

        IList<DVD> DVDTextSearch(IList<DVD> query, string searchString);

        IList<DVD> DVDTypeFilter(IList<DVD> query, string type);

        IList<DVD> GetDVDs();

        IList<DVD> GetReservedDVDs();
        IList<DVD> DVDLibraryFilter(IList<DVD> query, string library);
    }
}
