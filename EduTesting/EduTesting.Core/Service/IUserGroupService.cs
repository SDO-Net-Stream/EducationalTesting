using EduTesting.ViewModels.UserGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.Service
{
    public interface IUserGroupService
    {
        UserGroupListItemViewModel[] GetGroups(UserGroupListFilterViewModel filter);
    }
}
