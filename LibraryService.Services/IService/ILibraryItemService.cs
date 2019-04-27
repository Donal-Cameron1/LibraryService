using LibraryService.Models;
using System.Collections.Generic;

namespace LibraryService.Services.IService
{
    public interface ILibraryItemService
    {
        IList<LibraryItem> GetReservedItems(string id);
        void LoanLibraryItem(int id);
    }
}
