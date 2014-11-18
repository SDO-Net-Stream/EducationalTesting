using EduTesting.DataProvider;
using EduTesting.Model;
using EduTesting.Model.Parameters;
using EduTesting.Security;
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
        private readonly IUserProvider _userProvider;
        private readonly ISessionManager _sessionManager;
        private readonly IHttpContextProvider _httpContext;
        private readonly IWebUserManager _webUser;
        public LoginService(IUserProvider userProvider, ISessionManager sessionManager, IHttpContextProvider httpContext, IWebUserManager webUser)
        {
            _userProvider = userProvider;
            _sessionManager = sessionManager;
            _httpContext = httpContext;
            _webUser = webUser;
        }
        public LoginInfo Login(LoginByEmailModel login)
        {
            var user = _userProvider.GetUserByEmailPassword(login.Email, login.Password);
            if (user == null)
                return null;
            _sessionManager.UpdateSession(user);
            return new LoginInfo(user);
        }

        public LoginInfo NtlmLogin()
        {
            var identity = _httpContext.Current.User.Identity as WindowsIdentity;
            if (identity != null && identity.IsAuthenticated)
            {
                var user = _userProvider.GetUserByDomainName(identity.Name);
                if (user != null)
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

        public LoginInfo LogOff()
        {
            _sessionManager.TerminateSession();
            _webUser.SetCurrent(null);
            return new LoginInfo(null);
        }

        public LoginInfo GetUserInfo()
        {
            return new LoginInfo(_webUser.CurrentUser);
        }
    }
}
