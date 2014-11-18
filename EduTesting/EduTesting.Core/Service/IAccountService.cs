using EduTesting.Model;
using EduTesting.Model.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.Service
{
    public interface IAccountService
    {
        void RegisterByEmail(RegisterUserModel model);
        void ResetPassword(LostPasswordModel model);
        void ResetPasswordConfirm(ResetPasswordModel model);
    }
}
