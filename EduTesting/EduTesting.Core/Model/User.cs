using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string UserDomainName { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public bool UserIsActive { get; set; }
    }
}
