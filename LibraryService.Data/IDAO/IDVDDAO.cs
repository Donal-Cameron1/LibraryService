﻿using LibraryService.Models;
using System.Collections.Generic;


namespace LibraryService.Data.IDAO
{
    public interface IDVDDAO
    {
        DVD GetDVD(int id);

        void CreateDVD(DVD dvd);

        void UpdateDVD(DVD dvd);

        void DeleteDVD(DVD dvd);

        void ReserveDVD(int id, string UserId);

        IList<DVD> GetNewDVDs();

        IList<DVD> DVDGenreFilter(IList<DVD> query, string genre);

        IList<DVD> DVDStatusFilter(IList<DVD> query, string status);

        IList<DVD> DVDTextSearch(IList<DVD> query, string searchString);

        IList<DVD> DVDTypeFilter(IList<DVD> query, string type);

        IList<DVD> GetDVDs();

        //void LoanDVD(int id, string UserId, DateTime duedate);

        void DeleteReservation(int id, string userId);

        IList<DVD> GetReservedDVDs();
    }
}
