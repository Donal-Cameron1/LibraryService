using LibraryService.Models;
using System.Collections.Generic;

namespace LibraryService.Services.Service
{
    public interface ILibraryItemDAO
    {
        IList<LibraryItem> GetReservedLibraryItems(string id);
        void LoanLibraryItem(int id);
        void ExtendLoan(int id);
        void ReturnLibraryItem(int id);
        IList<LibraryItem> GetLoanedLibraryItems();
        IList<LibraryItem> GetOverdueLibraryItems();
        IList<LibraryItem> GetLibraryItems();
        IList<LibraryItem> TextSearch(IList<LibraryItem> loanedItems, string searchString);
    }
}