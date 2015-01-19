﻿using EduTesting.Model;
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
            new Role { RoleID=2, RoleName = EduTestingConsts.RoleName_Teacher },
            new Role { RoleID=1, RoleName = EduTestingConsts.RoleName_User }
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
    }
}
