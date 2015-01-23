﻿using EduTesting.DataProvider;
using EduTesting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.Repositories
{
    public partial class EduTestingRepository : IUserRepository
    {
        public User GetUserById(int userId)
        {
            return SelectById<User>(userId);
        }

        public User GetUserByDomainName(string domainName)
        {
            return GetDBContext().Users.FirstOrDefault(user => domainName.Equals(user.UserDomainName, StringComparison.InvariantCultureIgnoreCase));
        }

        public User GetUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;
            else
                return GetDBContext().Users.FirstOrDefault(user => email.Equals(user.UserEmail, StringComparison.InvariantCultureIgnoreCase));
        }

        public void InsertUser(User user)
        {
            Insert(user, true);
        }

        public void UpdateUser(User user)
        {
            Update(user, true);
        }

        public User GetUserByToken(string token)
        {
            return GetDBContext().Users.FirstOrDefault(u => u.UserPasswordVerificationToken == token);
        }

        public void AddRoleToUser(User user, RoleCode role)
        {
            var db = GetDBContext();
            if (user.Roles == null)
                user.Roles = new List<Role>();
            user.Roles.Add(SelectById<Role>((int)role));
            Update(user, true);
        }

        public void RemoveRoleFromUser(User user, RoleCode role)
        {
            user.Roles.Remove(user.Roles.First(r => r.RoleID == (int)role));
            Update(user, true);
        }

        public void DeleteUser(User user)
        {
            Delete<User>(user.UserId, true);
        }

        public IQueryable<User> GetUsers()
        {
            return SelectAll<User>(u => u.Roles);
        }
    }
}
