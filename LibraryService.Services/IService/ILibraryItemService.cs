using LibraryService.Models;
using System.Collections.Generic;

namespace LibraryService.Services.IService
{
    public interface ILibraryItemService
    {
        LibraryItem GetLibraryItem(int id);

        IList<LibraryItem> GetLibraryItems();

        IList<LibraryItem> GetLoanedLibraryItems();

        IList<LibraryItem> GetOverdueLibraryItems();

        IList<LibraryItem> GetReservedLibraryItemsOfUser(string id);

        IList<LibraryItem> GetLoanedLibraryItemsOfUser(string id);

        void LoanLibraryItem(int id);

        void ExtendLoan(int id);

        void ReturnLibraryItem(int id);

        void BookmarkLibraryItem(int id, string currentUserId);

        void DeleteBookmark(int id, string currentUserId);

        void ReserveLibraryItem(int id, string currentUserId);

        void DeleteReservation(int id, string currentUserId);

        IList<LibraryItem> TextSearch(IList<LibraryItem> loanedItems, string searchString);

    }
}
