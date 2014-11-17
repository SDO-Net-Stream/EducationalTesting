using EduTesting.Model;
using EduTesting.Service;
using EduTesting.WebRequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.Security
{
    public class WebUserManager : IWebUserManager
    {
        const string ContextItem = "WebUser";
        private readonly IHttpContextProvider _httpContextProvider;
        public WebUserManager(IHttpContextProvider httpContextProvider)
        {
            _httpContextProvider = httpContextProvider;
        }
        public User CurrentUser
        {
            get
            {
                if (_httpContextProvider.Current.Items.Contains(ContextItem))
                    return _httpContextProvider.Current.Items[ContextItem] as User;
                return null;
            }
        }

        public void SetCurrent(User user)
        {
            _httpContextProvider.Current.Items[ContextItem] = user;
        }
    }
}
