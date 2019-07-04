using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Test.IBLL;
using Test.Mongo.Model;

namespace Test.WebApp.Controllers
{
    public class SingleChoiceController : AdminBaseController
    {
        #region IOC
        /// <summary>
        /// 单选题业务层
        /// </summary>
        public ISingleChoiceBll SingleChoiceService { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="singleChoiceService"></param>
        public SingleChoiceController(ISingleChoiceBll singleChoiceService)
        {
            this.SingleChoiceService = singleChoiceService;
        }
        #endregion

        public ActionResult Index()
        {
            ViewData.Model = SingleChoiceService.Find(m => true);
            return View();
        }

        #region 添加
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(SingleChoice model)
        {
            SingleChoiceService.InsertOne(model);
            return Content("ok");
        }
        #endregion
    }
}