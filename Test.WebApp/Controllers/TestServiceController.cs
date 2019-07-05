using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Test.WebApp.Controllers
{
    public class TestServiceController : Controller
    {
        // GET: TestService
        public ActionResult Index()
        {
            UserServiceReference.UserServiceClient client = new UserServiceReference.UserServiceClient();
            return Content(client.Find());
        }
    }
}