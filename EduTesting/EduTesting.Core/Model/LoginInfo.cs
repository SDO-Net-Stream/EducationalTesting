using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.Model
{
    public class LoginInfo
    {
        public string UserName { get; set; }
        public LoginInfo(User currentUser)
        {
            if (currentUser == null)
            {
                UserName = "";
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(currentUser.DomainName))
                {
                    UserName = currentUser.DomainName.Split('\\')[1];
                }
                else
                {
                    UserName = (currentUser.Email ?? "anonymous").Split('@')[0];
                }
            }
        }
    }
}
