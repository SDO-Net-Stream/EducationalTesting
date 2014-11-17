using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using EducationalProject.Models;

namespace EduTesting.Web.Controllers
{
    public class HomeController : EduTestingControllerBase
    {
        public ActionResult RolePermissions()
        {
            //TODO: Depending on the role redirect to right place
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Index()
        { 
            return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
        }
	}
}