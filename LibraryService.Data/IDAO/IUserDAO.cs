using LibraryService.Models;
using System.Collections.Generic;

namespace LibraryService.Data.IDAO
{
    public interface IUserDAO
    {
        User GetUser(string id);

        User GetCurrentUser(string currentUserId);

        IList<User> GetUsers();

        void CreateUser(User user);

        void EditUser(User user);

        void DeleteUser(User user);
    }
}
