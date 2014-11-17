using EduTesting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.DataProvider
{
    public class FakeUserProvider : IUserProvider
    {
        public User GetUserByDomainName(string domainName)
        {
            return new User
            {
                DomainName = domainName,
                Email = domainName.Split('\'')[1] + "@eleks.com"
            };
        }

        public User GetUserByEmailPassword(string email, string password)
        {
            if (email == password)
            {
                return new User
                {
                    Email = email
                };
            }
            else
            {
                return null;
            }
        }
    }
}
