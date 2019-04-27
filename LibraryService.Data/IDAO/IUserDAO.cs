using LibraryService.Models;
using System.Collections.Generic;

namespace LibraryService.Data.IDAO
{
    public interface IUserDAO
    {
        User GetCurrentUser(string currentUserId);

        void EditUser(User user);

        IList<User> GetUsers();

        User GetUser(string id);

        User GetUserByUsername(string username);

        void CreateUser(User user);

        void DeleteUser(User user);
    }
}
