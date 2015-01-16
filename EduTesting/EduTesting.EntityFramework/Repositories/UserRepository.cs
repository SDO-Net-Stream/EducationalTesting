using EduTesting.DataProvider;
using EduTesting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return DBContext.Users.FirstOrDefault(user => domainName.Equals(user.UserDomainName, StringComparison.InvariantCultureIgnoreCase));
        }

        public User GetUserByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;
            else
                return DBContext.Users.FirstOrDefault(user => email.Equals(user.UserEmail, StringComparison.InvariantCultureIgnoreCase));
        }

        public User GetUserByEmailPassword(string email, string password)
        {
            throw new NotImplementedException();
        }

        public User GetUserByToken(string token)
        {
            return DBContext.Users.FirstOrDefault(user => user.UserPasswordVerificationToken == token);
        }

        public User Register(string name, string email, string password)
        {
            throw new NotImplementedException();
        }

        public void ChangePassword(User user, string password)
        {
            throw new NotImplementedException();
        }

        public string GenerateUserToken(User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUserToken(User user, string token)
        {
            throw new NotImplementedException();
        }
    }
}
