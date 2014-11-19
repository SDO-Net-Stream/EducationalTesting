using EduTesting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.DataProvider
{
    public class FakeUserProvider : IUserRepository
    {
        private User FakeUser(string key)
        {
            var name = key;
            if (name.Contains('@'))
                name = name.Split('@')[0];
            if (name.Contains('\\'))
                name = name.Split('\\')[1];
            return new User
            {
                Id = key,
                Name = name,
                DomainName = "domain\\" + name,
                Email = name + "@email.email",
                IsActive = true
            };
        }

        public User GetUserByDomainName(string domainName)
        {
            return FakeUser(domainName);
        }

        public User GetUserByEmailPassword(string email, string password)
        {
            if (email == password)
            {
                return GetUserByEmail(email);
            }
            else
            {
                return null;
            }
        }

        public User GetUserById(string id)
        {
            return FakeUser(id);
        }

        public User GetUserByEmail(string email)
        {
            return FakeUser(email);
        }

        public User GetUserByToken(string token)
        {
            if (!token.Contains(' '))
                return null;
            return FakeUser(token.Split(' ')[0]);
        }

        public User Register(string name, string email, string password)
        {
            if (password != email)
                throw new BusinessLogicException("Email and password must match");
            return FakeUser(email);
        }

        public void ChangePassword(User user, string password)
        {
            //
        }

        public string GenerateUserToken(User user)
        {
            var key = string.IsNullOrWhiteSpace(user.Email) ? user.DomainName : user.Email;
            return key + " " + Guid.NewGuid().ToString();
        }

        public void DeleteUserToken(User user, string token)
        {
            //
        }
    }
}
