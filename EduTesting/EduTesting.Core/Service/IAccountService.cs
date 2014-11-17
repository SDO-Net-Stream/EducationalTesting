using EduTesting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.Service
{
    public interface ILoginService
    {
        LoginInfo Login(LoginByEmailViewModel user);
        LoginInfo NtlmLogin();
        void LogOff();
    }
}
