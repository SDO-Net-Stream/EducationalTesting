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
                Id = domainName,
                DomainName = domainName,
                Email = domainName.Split('\\')[1] + "@eleks.com"
            };
        }

        public User GetUserByEmailPassword(string email, string password)
        {
            if (email == password)
            {
                return new User
                {
                    Id = email,
                    Email = email
                };
            }
            else
            {
                return null;
            }
        }

        public User GetUserById(string id)
        {
            var name = id;
            if (name.Contains('@'))
                name = name.Split('@')[0];
            if (name.Contains('\\'))
                name = name.Split('\\')[1];
            return new User
            {
                Id = id,
                DomainName = "domain\\" + name,
                Email = name + "@email.email"
            };
        }
    }
}
