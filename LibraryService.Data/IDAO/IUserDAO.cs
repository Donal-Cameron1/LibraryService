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
        User GetCurrentUser(string currentUserId);
        void EditUser(User user);
        IList<User> GetUsers();
        User GetUser(string id);
        void CreateUser(User user);
        void DeleteUser(User user);
    }
}
