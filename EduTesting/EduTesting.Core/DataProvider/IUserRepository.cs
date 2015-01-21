using EduTesting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.DataProvider
{
    public interface IUserRepository
    {
        User GetUserById(int userId);
        User GetUserByDomainName(string domainName);
        User GetUserByEmail(string email);
        User GetUserByToken(string token);

        void InsertUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        void AddRoleToUser(User user, UserRole role);

        // TODO: replace by filtering method
        IQueryable<User> GetUsers();
    }
}
