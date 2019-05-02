using LibraryService.Models;
using System.Collections.Generic;

namespace LibraryService.Services.Service
{
    public interface ILibraryItemDAO
    {
        LibraryItem GetLibaryItem(int id);

        IList<LibraryItem> GetLibraryItems();

        IList<LibraryItem> GetLoanedLibraryItems();

        IList<LibraryItem> GetOverdueLibraryItems();

        IList<LibraryItem> GetReservedLibraryItemsOfUser(string id);

        IList<LibraryItem> GetLoanedLibraryItemsOfUser(string id);

        void UpdateLibraryItem(LibraryItem item);

        void LoanLibraryItem(int id);

        void ExtendLoan(int id);

        void ReturnLibraryItem(int id);

        IList<LibraryItem> TextSearch(IList<LibraryItem> loanedItems, string searchString);

        void BookmarkLibraryItem(int id, string currentUserId);

        void DeleteBookmark(int id, string currentUserId);

        void ReserveLibraryItem(int id, string currentUserId);

        void DeleteReservation(int id, string currentUserId);

    }
}