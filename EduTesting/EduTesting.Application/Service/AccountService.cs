using Abp.Domain.Uow;
using EduTesting.DataProvider;
using EduTesting.Model;
using EduTesting.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var user = _userRepository.Register(model.Name, model.Email, model.Password);
        }

        public void ResetPassword(LostPasswordViewModel model)
        {
            var user = _userRepository.GetUserByEmail(model.Email);
            if (user == null)
                throw new BusinessLogicException("Email not found");
            var token = _userRepository.GenerateUserToken(user);
            //UnitOfWorkScope.Current.OnSuccess(() => _notificationService.SendResetPassword(user, token));
            _notificationService.SendResetPassword(user, token);
        }

        public void ResetPasswordConfirm(ResetPasswordViewModel model)
        {
            var user = _userRepository.GetUserByToken(model.Token);
            if (user == null)
                throw new BusinessLogicException("Invalid user token");
            _userRepository.ChangePassword(user, model.Password);
            _userRepository.DeleteUserToken(user, model.Token);
        }
    }
}
