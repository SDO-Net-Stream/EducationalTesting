using System;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Security;
using Postal;
using EducationalProject.Models;

namespace EducationalProject.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            //if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                return RedirectToLocal(returnUrl);
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            //WebSecurity.Logout();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    /*
                    string confirmationToken = WebSecurity.CreateUserAndAccount(model.UserName, model.Password,
                        new { model.FirstName, model.LastName }, true);
                    SendEmailConfirmation(model.UserName, model.UserName, confirmationToken);

                    if (!Roles.RoleExists("User"))
                        Roles.CreateRole("User");
                    Roles.AddUserToRole(model.UserName, "User");
                    WebSecurity.Login(model.UserName, model.Password);
                    */
                    return RedirectToAction("RolePermissions", "Home");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult RegisterConfirmation(string Id)
        {
            //if (WebSecurity.ConfirmAccount(Id))
            {
                return RedirectToAction("ConfirmationSuccess");
            }
            return RedirectToAction("ConfirmationFailure");
        }

        private void SendEmailConfirmation(string to, string username, string confirmationToken)
        {
            dynamic email = new Email("RegEmail");
            email.To = to;
            email.UserName = username;
            email.ConfirmationToken = confirmationToken;
            email.Send();
        }

        // GET: Account/LostPassword
        [AllowAnonymous]
        public ActionResult LostPassword()
        {
            return View();
        }

        // POST: Account/LostPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LostPassword(LostPasswordModel model)
        {
           if (ModelState.IsValid)
           {
              MembershipUser user;
              using (var context = new UsersContext())
              {
                 var foundUserName = (from u in context.UserProfiles
                                      where u.UserName == model.Email
                                      select u.UserName).FirstOrDefault();
                 if (foundUserName != null)
                 {
                    user = Membership.GetUser(foundUserName.ToString());
                 }
                 else
                 {
                    user = null;
                 }
              }
              if (user != null)
              {
                 // Generae password token that will be used in the email link to authenticate user
                  /*
                 var token = WebSecurity.GeneratePasswordResetToken(user.UserName);

                 // Email stuff
                 string subject = "Reset your password for asdf.com";
                 string body = "To reset your password click on this link: " +
                                Url.Action("ResetPassword", "Account", new {rt = token}, "http");
                 string from = "donotreply@asdf.com";
 
                 MailMessage message = new MailMessage(from, model.Email);
                 message.Subject = subject;
                 message.Body = body;
                 SmtpClient client = new SmtpClient();
 
                 // Attempt to send the email
                 try
                 {
                    client.Send(message);
                 }
                 catch (Exception e)
                 {
                    ModelState.AddModelError("", "Issue sending email: " + e.Message);
                 }
                   * */
              }         
              else // Email not found
              {
                 /* Note: You may not want to provide the following information
                 * since it gives an intruder information as to whether a
                 * certain email address is registered with this website or not.
                 * If you're really concerned about privacy, you may want to
                 * forward to the same "Success" page regardless whether an
                 * user was found or not. This is only for illustration purposes.
                 */
                 ModelState.AddModelError("", "No user found by that email.");
              }
           }
 
           /* You may want to send the user to a "Success" page upon the successful
           * sending of the reset email link. Right now, if we are 100% successful
           * nothing happens on the page. :P
           */
           return View(model);
        }

        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string rt)
        {
            ResetPasswordModel model = new ResetPasswordModel();
            model.ReturnToken = rt;
            return View(model);
        }

        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                /*
                bool resetResponse = WebSecurity.ResetPassword(model.ReturnToken, model.Password);
                if (resetResponse)
                {
                    ViewBag.Message = "Successfully Changed";
                }
                else
                {
                    ViewBag.Message = "Something went horribly wrong!";
                }
                 */
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ConfirmationSuccess()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ConfirmationFailure()
        {
            return View();
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("RolePermissions", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
