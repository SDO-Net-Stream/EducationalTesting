using EduTesting.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.Service
{
    public interface IUserService
    {
        UserViewModel InsertUser(UserViewModel user);
        void UpdateUser(UserViewModel user);
        void DeleteUser(UserViewModel user);
        UserViewModel GetUser(UserViewModel key);
        UserViewModel[] GetUsers(UserListFilterViewModel filter);
    }
}
