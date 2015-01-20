using EduTesting.ViewModels.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.ViewModels.UserGroup
{
    public class UserGroupViewModel
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public UserListItemViewModel[] Users { get; set; }
        public TestListItemViewModel[] Tests { get; set; }
    }
}
