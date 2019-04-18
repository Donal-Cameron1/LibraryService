using LibraryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Services.IService
{
    public interface IDVDService
    {
        DVD GetDVD(int id);
        DVD CreateDefaultDVD();
        void CreateDVD(DVD dvd);
        void EditDVD(DVD dvd);
        void DeleteDVD(DVD dvd);
        void BookmarkDVD(DVD item, string UserId);
        void DeleteBookmark(int id, string UserId);
    }
}
