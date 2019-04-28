using LibraryService.Models;
using System.Collections.Generic;

namespace LibraryService.Services.IService
{
    public interface ILibraryItemService
    {
        IList<LibraryItem> GetReservedItems(string id);
        void LoanLibraryItem(int id);
        void ExtendLoan(int id);
        void ReturnLibraryItem(int id);
        IList<LibraryItem> GetLoanedLibraryItems();
        IList<LibraryItem> GetOverdueLibraryItems();
        IList<LibraryItem> GetLibraryItems();
        IList<LibraryItem> TextSearch(IList<LibraryItem> loanedItems, string searchString);
        void BookmarkLibraryItem(int id, string currentUserId);
        void DeleteBookmark(int id, string currentUserId);
        void ReserveLibraryItem(int id, string currentUserId);
        void DeleteReservation(int id, string currentUserId);
        LibraryItem GetLibraryItem(int id);
    }
}
