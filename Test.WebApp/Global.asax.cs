using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Test.Redis;
using Test.IBLL;
using log4net;
using Test.BLL;

namespace Test.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            log4net.Config.XmlConfigurator.Configure();//读取在config文件中有关log4net 的配置信息

            //开启一个线程，用来将错误信息写入log4net的错误日志中
            ThreadPool.QueueUserWorkItem(state =>
            {
                var redis = new RedisHelper(0);
                while (true)//一个永不停止的线程
                {                    
                    if (redis.ListLength("exception") > 0)//队列中有待写入日志中的错误信息
                    {
                        Exception ex = redis.ListLeftPop<Exception>("exception");//出队
                        ILog log = LogManager.GetLogger("errorMsg");
                        log.Error(ex.ToString());
                    }
                    else
                        Thread.Sleep(3000);//没有待写入日志的错误信息，则线程休息3秒钟
                }
            });

            /*将题库中的一些题型的所有题的id存储一份redis中，为后面随机抽题做铺垫*/
            //IRedisService redisService = new RedisService();
            //redisService.SaveAllSingleChoiceId();//单选题
            //redisService.SaveAllReadingMaterialId();//阅读题
        }
    }
}
