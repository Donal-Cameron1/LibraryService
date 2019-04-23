using LibraryService.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Data.DAL
{
    public class DbUtils
    {
        public static LibraryContext db = new LibraryContext();
    }
}
