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
        User GetUserById(string id);
        User GetUserByDomainName(string domainName);
        User GetUserByEmail(string email);
        User GetUserByEmailPassword(string email, string password);
        User GetUserByToken(string token);

        User Register(string name, string email, string password);
        void ChangePassword(User user, string password);

        string GenerateUserToken(User user);
        void DeleteUserToken(User user, string token);
    }
}
