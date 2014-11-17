using EduTesting.Model;
using EduTesting.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.Security
{
    public class WebUserManager : IWebUserManager
    {
        private User _current;
        public User CurrentUser
        {
            get { return _current; }
        }

        public void SetCurrent(User user)
        {
            _current = user;
        }
    }
}
