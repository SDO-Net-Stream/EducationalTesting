using EduTesting.Interfaces;
using EduTesting.Model;
using EduTesting.ViewModels.UserGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.Service
{
    public class UserGroupService : EduTestingAppServiceBase, IUserGroupService
    {
        private readonly IEduTestingRepository _repository;
        public UserGroupService(IEduTestingRepository repository)
        {
            _repository = repository;
        }

        public UserGroupListItemViewModel[] GetGroups(UserGroupListFilterViewModel filter)
        {
            var groups = string.IsNullOrWhiteSpace(filter.GroupName) ?
                _repository.SelectAll<UserGroup>() :
                _repository.SelectAll<UserGroup>(g => g.GroupName.Contains(filter.GroupName));
            return groups.OrderBy(g => g.GroupName).Take(filter.Count ?? 10)
                .Select(g => new UserGroupListItemViewModel { GroupId = g.GroupID, GroupName = g.GroupName })
                .ToArray();
        }
    }
}
