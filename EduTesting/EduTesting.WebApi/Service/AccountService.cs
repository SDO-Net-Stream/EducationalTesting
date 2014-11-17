using EduTesting.DataProvider;
using EduTesting.Model;
using EduTesting.WebRequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.Service
{
    public class AccountService : IAccountService
    {
        private readonly IUserProvider _userProvider;
        private readonly ISessionManager _sessionManager;
        private readonly IHttpContextProvider _httpContext;
        public AccountService(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }
        public LoginInfo Login(string email, string password)
        {
            var user = _userProvider.GetUserByEmailPassword(email, password);
            if (user == null)
                return null;
            _sessionManager.UpdateSession(user);
            return new LoginInfo(user);
        }

        public LoginInfo NtlmLogin()
        {
            var identity = _httpContext.Current.User.Identity as WindowsIdentity;
            if (identity != null)
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

        public void LogOff()
        {
            _sessionManager.TerminateSession();
        }
    }
}
