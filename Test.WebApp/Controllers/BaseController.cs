using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Test.WebApp.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 执行控制器的方法之前执行此方法
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (Session["user"] == null)
                filterContext.Result = Redirect("~/User/Login");//用户未登录，默认跳转至登陆页面
        }
    }
}