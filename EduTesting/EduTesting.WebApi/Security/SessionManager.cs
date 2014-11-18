using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduTesting.Model;
using EduTesting.Service;
using System.Web;
using System.Web.Security;
using EduTesting.DataProvider;
using EduTesting.WebRequestParameters;
namespace EduTesting.Security
{
    public class SessionManager : ISessionManager
    {
        const int CookieExpirationTimeoutMinutes = 1000000;
        private readonly IUserProvider _userProvider;
        private readonly IHttpContextProvider _httpContext;
        public SessionManager(IUserProvider userProvider, IHttpContextProvider httpContext)
        {
            _userProvider = userProvider;
            _httpContext = httpContext;
        }


        public void UpdateSession(User user)
        {
            var context = _httpContext.Current;
            var ticket = new FormsAuthenticationTicket(user.Id, false, CookieExpirationTimeoutMinutes);
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(EduTestingConsts.AUTH_COOKIE_NAME, encryptedTicket)
            {
                HttpOnly = true,
                Path = context.Request.ApplicationPath,
                Secure = context.Request.IsSecureConnection
            };
            context.Response.Cookies.Add(cookie);
        }

        public void TerminateSession()
        {
            var context = _httpContext.Current;
            var cookie = new HttpCookie(EduTestingConsts.AUTH_COOKIE_NAME, "")
            {
                HttpOnly = true,
                Path = context.Request.ApplicationPath,
                Secure = context.Request.IsSecureConnection
            };
            context.Response.Cookies.Add(cookie);
        }

        public User GetUserFromSession()
        {
            var cookie = _httpContext.Current.Request.Cookies.Get(EduTestingConsts.AUTH_COOKIE_NAME);
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                var userId = ticket.Name;
                return _userProvider.GetUserById(userId);
            }
            return null;
        }
    }

}
