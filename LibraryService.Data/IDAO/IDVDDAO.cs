using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryService.Models;


namespace LibraryService.Data.IDAO
{
    public interface IDVDDAO
    {
        DVD GetDVD(int id);
        void CreateDVD(DVD dvd);
        void EditDVD(DVD dvd);
        void DeleteDVD(DVD dvd);
    }
}
