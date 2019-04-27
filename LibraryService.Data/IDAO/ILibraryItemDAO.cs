using LibraryService.Models;
using System.Collections.Generic;

namespace LibraryService.Services.Service
{
    public interface ILibraryItemDAO
    {
        IList<LibraryItem> GetReservedLibraryItems(string id);
        void LoanLibraryItem(int id);
        IList<LibraryItem> GetLoanedLibraryItems();
        IList<LibraryItem> GetOverdueLibraryItems();
        IList<LibraryItem> GetLibraryItems();
        IList<LibraryItem> TextSearch(IList<LibraryItem> loanedItems, string searchString);
    }
}