using EduTesting.Model;
using Postal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduTesting.Web.Models
{
    public class ResetPasswordEmailModel : Email
    {
        public User User { get; set; }
        public string ResetUrl { get; set; }

        public string From { get; set; }
    }
}