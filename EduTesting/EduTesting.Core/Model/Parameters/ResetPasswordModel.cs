using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduTesting.Model.Parameters
{
    public class ResetPasswordModel
    {
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
