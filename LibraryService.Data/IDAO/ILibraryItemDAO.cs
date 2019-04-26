using System.Collections.Generic;
using LibraryService.Models;

namespace LibraryService.Services.Service
{
    public interface ILibraryItemDAO
    {
        IList<LibraryItem> GetReservedLibraryItems(string id);
        void LoanLibraryItem(int id);
    }
}