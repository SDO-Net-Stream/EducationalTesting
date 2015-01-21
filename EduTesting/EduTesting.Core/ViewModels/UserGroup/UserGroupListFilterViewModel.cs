using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.ViewModels.UserGroup
{
    public class UserGroupListFilterViewModel
    {
        public int? UserId { get; set; } 
        public int? TestId { get; set; }
        public string GroupName { get; set; }
        public int? Count { get; set; }
    }
}
