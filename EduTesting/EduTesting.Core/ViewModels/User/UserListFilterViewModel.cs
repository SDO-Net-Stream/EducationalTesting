using EduTesting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.ViewModels.User
{
    public class UserListFilterViewModel
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserDomainName { get; set; }
        public bool? UserActivated { get; set; }
        public UserRole[] Roles { get; set; }
        public int? Count { get; set; }
    }
}
