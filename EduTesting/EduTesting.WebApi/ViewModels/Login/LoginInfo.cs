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
        public string[] UserRoles { get; set; }
        public LoginInfo(User currentUser)
        {
            if (currentUser == null)
            {
                UserName = "";
                UserRoles = new string[0];
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(currentUser.DomainName))
                {
                    UserName = currentUser.DomainName.Split('\\')[1];
                }
                else
                {
                    UserName = (currentUser.UserEmail ?? "anonymous").Split('@')[0];
                }
                UserRoles = currentUser.Roles.Select(r => r.RoleName).ToArray();
            }
        }
    }
}
