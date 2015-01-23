using EduTesting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.ViewModels.User
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }
        public string UserDomainName { get; set; }
        public bool UserActivated { get; set; }
        public RoleCode[] Roles { get; set; }
    }
}
