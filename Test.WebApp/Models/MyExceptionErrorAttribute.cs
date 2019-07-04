using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Test.Redis;

namespace Test.WebApp.Models
{
    public class MyExceptionErrorAttribute : HandleErrorAttribute
    {
        /// <summary>
        /// 当整个网站在发生错误时
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            /*使用redis的队列*/
            var redis = new RedisHelper(0);
            redis.ListRightPush<Exception>("exception", filterContext.Exception);//入队

            filterContext.HttpContext.Response.Redirect("~/error.html");//跳转至错误页面
        }
    }
}