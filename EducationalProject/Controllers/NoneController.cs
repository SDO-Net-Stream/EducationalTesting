using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EducationalProject.Controllers
{
    public class NoneController : Controller
    {
        // GET: None
         [Authorize(Roles = "None")]
        public ActionResult NoneSpace()
        {
            return View();
        }
    }
}