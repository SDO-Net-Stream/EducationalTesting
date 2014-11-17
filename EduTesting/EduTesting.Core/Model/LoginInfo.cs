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
            UserName = currentUser.DomainName + " " + currentUser.Email;
        }
    }
}
