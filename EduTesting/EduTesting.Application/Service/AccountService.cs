using Abp.Domain.Uow;
using EduTesting.DataProvider;
using EduTesting.Model;
using EduTesting.Security;
using EduTesting.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduTesting.Service
{
    public class AccountService : EduTestingAppServiceBase, IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly INotificationService _notificationService;
        public AccountService(IUserRepository userRepository, INotificationService notificationService)
        {
            _userRepository = userRepository;
            _notificationService = notificationService;
        }
        public void RegisterByEmail(RegisterUserViewModel model)
        {
            var user = new User();
            user.UserFirstName = model.Name;
            user.UserEmail = model.Email;
            user.UserPasswordSalt = PasswordEncription.GenerateSaltValue();
            user.UserPassword = PasswordEncription.HashPassword(model.Password, user.UserPasswordSalt);
            user.UserPasswordVerificationToken = null;
            _userRepository.InsertUser(user);
            //var user = _userRepository.Register(model.Name, model.Email, model.Password);
        }

        public void ResetPassword(LostPasswordViewModel model)
        {
            var user = _userRepository.GetUserByEmail(model.Email);
            if (user == null)
                throw new BusinessLogicException("Email not found");
            string token;
            var encoding = Encoding.Unicode;
            while (true)
            {
                token = BitConverter.ToString(encoding.GetBytes(PasswordEncription.GenerateSaltValue()));
                if (_userRepository.GetUserByToken(token) == null)
                    break;
            }
            user.UserPasswordVerificationToken = token;
            _userRepository.UpdateUser(user);
            //UnitOfWorkScope.Current.OnSuccess(() => _notificationService.SendResetPassword(user, token));
            _notificationService.SendResetPassword(user, token);
        }

        public void ResetPasswordConfirm(ResetPasswordViewModel model)
        {
            var user = _userRepository.GetUserByToken(model.Token);
            if (user == null)
                throw new BusinessLogicException("Invalid user token");
            user.UserPasswordSalt = PasswordEncription.GenerateSaltValue();
            user.UserPassword = PasswordEncription.HashPassword(model.Password, user.UserPasswordSalt);
            user.UserPasswordVerificationToken = null;
            _userRepository.UpdateUser(user);
        }

    }
}
