using EduTesting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.Service
{
    public interface IAccountService
    {
        LoginInfo Login(string email, string password);
        LoginInfo NtlmLogin();
        void LogOff();
    }
}
