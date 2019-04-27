using LibraryService.Models;
using System.Collections.Generic;

namespace LibraryService.Services.IService
{
    public interface IUserService
    {
        IList<User> GetUsers();

        User GetUser(string id);

        User CreateDefaultUser();

        void CreateUser(User user);

        void EditUser(User user);

        void DeleteUser(User user);
    }
}
