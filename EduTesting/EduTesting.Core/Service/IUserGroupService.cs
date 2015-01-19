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
        UserGroupViewModel[] GetGroups(UserGroupListFilterViewModel filter);
        UserListItemViewModel[] GetUsers(UserListFilterViewModel filter);

        UserGroupViewModel InsertGroup(UserGroupUpdateViewModel group);
        UserGroupViewModel UpdateGroup(UserGroupUpdateViewModel group);
        void DeleteGroup(UserGroupUpdateViewModel group);
    }
}
