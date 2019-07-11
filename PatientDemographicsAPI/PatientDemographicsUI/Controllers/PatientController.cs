using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientDemographicsUI.Controllers
{
    public class PatientController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}