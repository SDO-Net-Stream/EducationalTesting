using EduTesting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.DataProvider
{
    public interface IUserProvider
    {
        User GetUserById(string id);
        User GetUserByDomainName(string domainName);
        User GetUserByEmailPassword(string email, string password);
    }
}
