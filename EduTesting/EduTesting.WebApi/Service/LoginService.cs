using Abp.Domain.Uow;
using EduTesting.DataProvider;
using EduTesting.Model;
using EduTesting.Security;
using EduTesting.ViewModels.Account;
using EduTesting.ViewModels.Login;
using EduTesting.WebRequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.Service
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userProvider;
        private readonly ISessionManager _sessionManager;
        private readonly IHttpContextProvider _httpContext;
        private readonly IWebUserManager _webUser;
        public LoginService(IUserRepository userProvider, ISessionManager sessionManager, IHttpContextProvider httpContext, IWebUserManager webUser)
        {
            _userProvider = userProvider;
            _sessionManager = sessionManager;
            _httpContext = httpContext;
            _webUser = webUser;
        }

        [UnitOfWork(false)]
        public LoginInfo Login(LoginByEmailModel login)
        {
            var user = _userProvider.GetUserByEmailPassword(login.Email, login.Password);
            if (user == null || !user.IsActive)
                return null;
            _sessionManager.UpdateSession(user);
            return new LoginInfo(user);
        }

        [UnitOfWork(false)]
        public LoginInfo NtlmLogin()
        {
            var identity = _httpContext.Current.User.Identity as WindowsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                var user = _userProvider.GetUserByDomainName(identity.Name);
                if (user != null || !user.IsActive)
                {
                    _sessionManager.UpdateSession(user);
                    return new LoginInfo(user);
                }
            }
            var response = _httpContext.Current.Response;
            response.Expires = 0;
            response.Cache.SetNoStore();
            response.AppendHeader("Pragma", "no-cache");
            response.Buffer = true;
            response.StatusCode = 401;
            response.StatusDescription = "Unauthorized";
            response.AddHeader("WWW-Authenticate", "NTLM");
            response.Write("Unauthorized");
            response.End();
            return null;
        }

        [UnitOfWork(IsDisabled = true)]
        public LoginInfo LogOff()
        {
            _sessionManager.TerminateSession();
            _webUser.SetCurrent(null);
            return new LoginInfo(null);
        }

        [UnitOfWork(IsDisabled = true)]
        public LoginInfo GetUserInfo()
        {
            return new LoginInfo(_webUser.CurrentUser);
        }
    }
}
