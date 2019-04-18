using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryService.Models;

namespace LibraryService.Data.IDAO
{
    public interface IUserDAO
    {
        User GetUser(string currentUserId);

        void EditUser(User user);
    }
}
