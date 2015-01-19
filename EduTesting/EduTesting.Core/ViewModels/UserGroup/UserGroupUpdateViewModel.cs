using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.ViewModels.UserGroup
{
    public class UserGroupUpdateViewModel
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int[] Users { get; set; }
    }
}
