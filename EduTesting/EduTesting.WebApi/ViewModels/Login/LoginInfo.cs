using EduTesting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.ViewModels.Login
{
    public class LoginInfo
    {
        public string UserName { get; set; }
        public UserRole[] UserRoles { get; set; }
        public LoginInfo(EduTesting.Model.User currentUser)
        {
            if (currentUser == null)
            {
                UserName = "";
                UserRoles = new UserRole[0];
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(currentUser.UserDomainName))
                {
                    UserName = currentUser.UserDomainName.Split('\\')[1];
                }
                else
                {
                    UserName = (currentUser.UserEmail ?? "anonymous").Split('@')[0];
                }
                UserRoles = currentUser.Roles.Select(r => r.RoleID).ToArray();
            }
        }
    }
}
