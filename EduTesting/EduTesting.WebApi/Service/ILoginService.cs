using EduTesting.Model;
using EduTesting.Model.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.Service
{
    public interface ILoginService
    {
        LoginInfo GetUserInfo();
        LoginInfo Login(LoginByEmailModel user);
        LoginInfo NtlmLogin();
        LoginInfo LogOff();
    }
}
