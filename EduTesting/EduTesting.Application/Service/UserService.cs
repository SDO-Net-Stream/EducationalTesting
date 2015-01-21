using EduTesting.DataProvider;
using EduTesting.Model;
using EduTesting.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.Service
{
    public class UserService : EduTestingAppServiceBase, IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public UserViewModel InsertUser(UserViewModel user)
        {
            var entity = new User();
            UpdateUserFromViewModel(user, entity);
            _repository.InsertUser(entity);
            UpdateUserRolesFromViewModel(user, entity);
            return ToViewModel(entity);
        }

        public void UpdateUser(UserViewModel user)
        {
            var entity = _repository.GetUserById(user.UserId);
            UpdateUserFromViewModel(user, entity);
            _repository.UpdateUser(entity);
            UpdateUserRolesFromViewModel(user, entity);
        }

        public void DeleteUser(UserViewModel user)
        {
            var entity = _repository.GetUserById(user.UserId);
            _repository.DeleteUser(entity);
        }

        public UserViewModel GetUser(UserViewModel key)
        {
            var entity = _repository.GetUserById(key.UserId);
            return ToViewModel(entity);
        }

        public UserViewModel[] GetUsers(UserListFilterViewModel filter)
        {
            var users = _repository.GetUsers();
            if (filter.UserActivated.HasValue)
                users = users.Where(u => u.UserActivated == filter.UserActivated.Value);
            if (!string.IsNullOrWhiteSpace(filter.UserDomainName))
            {
                var nameFilter = filter.UserDomainName.ToLower();
                users = users.Where(u => u.UserDomainName != null && u.UserDomainName.ToLower().Contains(nameFilter));
            }
            if (!string.IsNullOrWhiteSpace(filter.UserEmail))
            {
                var emailFilter = filter.UserEmail.ToLower();
                users = users.Where(u => u.UserEmail != null && u.UserEmail.ToLower().Contains(emailFilter));
            }
            if (!string.IsNullOrWhiteSpace(filter.UserName))
            {
                var nameFilter = filter.UserName.ToLower();
                users = users.Where(u => string.Concat(u.UserFirstName, " ", u.UserLastName).ToLower().Contains(nameFilter));
            }
            if (filter.Roles != null)
            {
                foreach (var role in filter.Roles)
                    users = users.Where(u => u.Roles.Any(r => r.RoleID == role));
            }
            return users.OrderBy(u => u.UserFirstName).ThenBy(u => u.UserLastName).Take(filter.Count ?? 20)
                .Select(ToViewModel).ToArray();
        }


        private void UpdateUserFromViewModel(UserViewModel model, User entity)
        {
            entity.UserFirstName = model.UserFirstName;
            entity.UserLastName = model.UserLastName;
            entity.UserEmail = model.UserEmail;
            entity.UserDomainName = model.UserDomainName;
        }
        private void UpdateUserRolesFromViewModel(UserViewModel user, User entity)
        {
            var toDelete = (entity.Roles ?? new Role[0]).ToDictionary(r => r.RoleID);
            foreach(var role in user.Roles)
            {
                if (toDelete.ContainsKey(role))
                    toDelete.Remove(role);
                else
                    _repository.AddRoleToUser(entity, role);
            }
            foreach (var role in toDelete)
                _repository.RemoveRoleFromUser(entity, role.Key);
        }

        private UserViewModel ToViewModel(User user)
        {
            var model = new UserViewModel
            {
                UserId = user.UserId,
                UserFirstName = user.UserFirstName,
                UserLastName = user.UserLastName,
                UserEmail = user.UserEmail,
                UserDomainName = user.UserDomainName,
                UserActivated = user.UserActivated
            };
            if (user.Roles == null)
                model.Roles = new UserRole[0];
            else
                model.Roles = user.Roles.Select(r => r.RoleID).ToArray();
            return model;
        }
    }
}
