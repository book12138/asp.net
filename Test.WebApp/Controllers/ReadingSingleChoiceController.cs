using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Test.IBLL;
using Test.Mongo.Model;
using MongoDB.Bson;

namespace Test.WebApp.Controllers
{
    public class ReadingSingleChoiceController : AdminBaseController
    {
        #region IOC
        /// <summary>
        /// 阅读部分单选题业务层
        /// </summary>
        public IReadingSingleChoiceBll ReadingSingleChoiceService { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="readingSingleChoiceService"></param>
        public ReadingSingleChoiceController(IReadingSingleChoiceBll readingSingleChoiceService)
        {
            ReadingSingleChoiceService = readingSingleChoiceService;
        }
        #endregion

        /// <summary>
        /// 展示所有的阅读团体部分的单选题
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewData.Model = ReadingSingleChoiceService.Find(r => true);
            return View();
        }

        #region 添加
        public ActionResult Create(string id)
        {
            return View();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(string id, ReadingSingleChoice model)
        {
            model.ReadingId = new ObjectId(id);
            ReadingSingleChoiceService.InsertOne(model);
            return RedirectToActionPermanent("Create", new System.Web.Routing.RouteValueDictionary(new { id = id }));
        }
        #endregion
    }
}