﻿using EduTesting.Interfaces;
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

        public UserGroupViewModel[] GetGroups(UserGroupListFilterViewModel filter)
        {
            var groups = string.IsNullOrWhiteSpace(filter.GroupName) ?
                _repository.SelectAll<UserGroup>() :
                _repository.SelectAll<UserGroup>().Where(g => g.GroupName.Contains(filter.GroupName));
            return groups.OrderBy(g => g.GroupName).Take(filter.Count ?? 10)
                .Select(g => ToViewModel(g))
                .ToArray();
        }

        public UserListItemViewModel[] GetUsers(UserListFilterViewModel filter)
        {
            var users = _repository.SelectAll<User>();
            if (!string.IsNullOrWhiteSpace(filter.UserName))
            {
                var nameFilter = filter.UserName.ToLowerInvariant();
                users = users.Where(u => string.Concat(u.UserFirstName, " ", u.UserLastName).ToLowerInvariant().Contains(nameFilter));
            }
            return users.OrderBy(u => u.UserFirstName).ThenBy(u => u.UserLastName).Take(filter.Count ?? 20)
                .Select(u => new UserListItemViewModel
                {
                    UserId = u.UserId,
                    UserFirstName = u.UserFirstName,
                    UserLastName = u.UserLastName
                }).ToArray();
        }


        private UserGroupViewModel ToViewModel(UserGroup entity)
        {
            var model = new UserGroupViewModel
            {
                GroupId = entity.GroupID,
                GroupName = entity.GroupName
            };
            if (entity.Users == null)
                model.Users = new UserListItemViewModel[0];
            else
                model.Users = entity.Users.OrderBy(u => u.UserFirstName).ThenBy(u => u.UserLastName)
                    .Select(u => new UserListItemViewModel
                    {
                        UserId = u.UserId,
                        UserFirstName = u.UserFirstName,
                        UserLastName = u.UserLastName
                    }).ToArray();
            return model;
        }

        #region Editing
        public UserGroupViewModel InsertGroup(UserGroupUpdateViewModel group)
        {
            var entity = new UserGroup();
            UpdateGroupFromViewModel(group, entity);
            entity = _repository.Insert(entity);
            UpdateGroupUsersFromViewModel(group, entity);
            _repository.Update(entity); // TODO: replace by SaveChanges
            return ToViewModel(entity);
        }


        private void UpdateGroupUsersFromViewModel(UserGroupUpdateViewModel group, UserGroup entity)
        {
            var toRemove = (entity.Users ?? new User[0]).ToDictionary(u => u.UserId);
            foreach (var userId in group.Users)
            {
                if (toRemove.ContainsKey(userId))
                {
                    toRemove.Remove(userId);
                }
                else
                {
                    var newUser = _repository.GetUserById(userId);
                    if (entity.Users == null)
                        entity.Users = new List<User>();
                    entity.Users.Add(newUser);
                }
            }
            foreach (var user in toRemove)
            {
                entity.Users.Remove(user.Value);
            }
        }

        private void UpdateGroupFromViewModel(UserGroupUpdateViewModel group, UserGroup entity)
        {
            entity.GroupName = group.GroupName;
        }
        public UserGroupViewModel UpdateGroup(UserGroupUpdateViewModel group)
        {
            var entity = _repository.SelectById<UserGroup>(group.GroupId);
            UpdateGroupFromViewModel(group, entity);
            UpdateGroupUsersFromViewModel(group, entity);
            _repository.Update(entity); // TODO: replace by SaveChanges
            return ToViewModel(entity);
        }
        public void DeleteGroup(UserGroupUpdateViewModel group)
        {
            _repository.Delete<UserGroup>(group.GroupId);
        }
        #endregion
    }
}