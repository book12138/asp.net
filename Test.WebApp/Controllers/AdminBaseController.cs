using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Test.Mongo.Model;
using Test.Enum;

namespace Test.WebApp.Controllers
{
    public class AdminBaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (Session["user"] != null)
            {
                Test.Mongo.Model.User user = (Test.Mongo.Model.User)Session["user"];
                if (user.Rank != UserRank.admin)                    
                    filterContext.Result = Redirect("~/User/Login");
            }
            else
                filterContext.Result = Redirect("~/User/Login");
        }
    }
}