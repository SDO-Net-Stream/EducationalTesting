using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationalProject.Models
{

    public class UsersWrapper
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string Address { get; set; }
    }
}