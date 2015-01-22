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
        public RoleCode[] UserRoles { get; set; }
        public LoginInfo(EduTesting.Model.User currentUser)
        {
            if (currentUser == null)
            {
                UserName = "";
                UserRoles = new RoleCode[0];
            }
            else
            {
                UserName = string.Concat(currentUser.UserFirstName, " ", currentUser.UserLastName);
                UserRoles = currentUser.Roles.Select(r => (RoleCode)r.RoleID).ToArray();
            }
        }
    }
}
