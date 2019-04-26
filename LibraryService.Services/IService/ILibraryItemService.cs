using LibraryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Services.IService
{
    public interface ILibraryItemService
    {
        IList<LibraryItem> GetReservedItems(string id);
        void LoanLibraryItem(int id);
    }
}
