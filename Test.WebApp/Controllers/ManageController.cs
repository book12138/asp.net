using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Test.WebApp.Controllers
{
    public class ManageController : AdminBaseController
    {
        // GET: Manage
        public ActionResult Index()
        {
            return View();
        }
    }
}