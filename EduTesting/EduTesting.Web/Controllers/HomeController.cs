﻿using System.Web.Mvc;

namespace EduTesting.Web.Controllers
{
    public class HomeController : EduTestingControllerBase
    {
        public ActionResult Index()
        { 
            return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
        }
	}
}