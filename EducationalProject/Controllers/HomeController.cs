using System;

using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using EducationalProject.DataInfo;
using EducationalProject.Models;
using PagedList;

namespace EducationalProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult RolePermissions()
        {
            var roleName = Roles.GetRolesForUser().FirstOrDefault();
            if (roleName != null)
            {
                var roleType =
                    (RoleType) Enum.Parse(typeof (RoleType), roleName);
                switch (roleType)
                {
                    case RoleType.Administrator:
                    {
                        return RedirectToAction("GetUsers", "Admin");
                    }
                    case RoleType.Teacher:
                    {
                        return RedirectToAction("TeacherSpace", "Teacher");
                    }
                    case RoleType.User:
                    {
                         return RedirectToAction("Tests", "Test");
                    }
                    case RoleType.None:
                    {
                        return RedirectToAction("NoneSpace", "None");
                    }
                    default:
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";
            return View();
        }
    }
}
