using EduTesting.Model;
using EduTesting.Service;
using Postal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EduTesting.Web.Models
{
    public class NotificationService : INotificationService
    {
        private readonly EmailService _emailService;
        private readonly string _urlRoot;
        private readonly string _fromEmail;
        public NotificationService()
        {
            var viewsPath = Path.Combine(HttpRuntime.AppDomainAppPath, ConfigurationManager.AppSettings["Email.TemplatesPath"]);
            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));
            _emailService = new EmailService(engines);
            _urlRoot = ConfigurationManager.AppSettings["Email.WebsiteUrl"];
            _fromEmail = ConfigurationManager.AppSettings["Email.From"];
        }


        public void SendResetPassword(User user, string token)
        {
            var email = new ResetPasswordEmailModel
            {
                User = user,
                ResetUrl = _urlRoot + "#/account/resetPassword/" + token,

                ViewName = "ChangePassword",
                From = _fromEmail
            };
            _emailService.Send(email);
        }
    }
}