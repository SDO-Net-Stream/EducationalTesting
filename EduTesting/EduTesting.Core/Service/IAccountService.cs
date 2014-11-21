using EduTesting.Model;
using EduTesting.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.Service
{
    public interface IAccountService
    {
        void RegisterByEmail(RegisterUserViewModel model);
        void ResetPassword(LostPasswordViewModel model);
        void ResetPasswordConfirm(ResetPasswordViewModel model);
    }
}
