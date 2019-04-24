using LibraryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
