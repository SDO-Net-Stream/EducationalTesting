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
        private readonly Role[] _fakeRoles = new Role[]{
            new Role { RoleID=UserRole.Teacher, RoleName = "Teacher" },
            new Role { RoleID=UserRole.User, RoleName = "User" }
        };
        private User FakeUser(string key)
        {
            var name = key;
            if (name.Contains('@'))
                name = name.Split('@')[0];
            if (name.Contains('\\'))
                name = name.Split('\\')[1];
            return new User
            {
                UserId = 1,
                UserFirstName = name,
                UserLastName = name, //!! split first & last name
                UserDomainName = "domain\\" + name,
                UserEmail = name + "@email.email",
                UserActivated = true,
                Roles = _fakeRoles
            };
        }

        public User GetUserByDomainName(string domainName)
        {
            return FakeUser(domainName);
        }

        public User GetUserById(int id)
        {
            return FakeUser(id.ToString());
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

        public void InsertUser(User user)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }


        public void AddRoleToUser(User user, UserRole role)
        {
            throw new NotImplementedException();
        }


        public void DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> GetUsers()
        {
            throw new NotImplementedException();
        }


        public void RemoveRoleFromUser(User entity, UserRole role)
        {
            throw new NotImplementedException();
        }
    }
}
