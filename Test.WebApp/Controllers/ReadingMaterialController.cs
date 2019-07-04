using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Test.Common;
using Test.IBLL;
using Test.Mongo.Model;

namespace Test.WebApp.Controllers
{
    public class ReadingMaterialController : AdminBaseController
    {
        #region IOC
        /// <summary>
        /// 阅读材料业务层
        /// </summary>
        public IReadingMaterialBll ReadingMaterialService { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="readingMaterialService"></param>
        public ReadingMaterialController(IReadingMaterialBll readingMaterialService)
        {
            this.ReadingMaterialService = readingMaterialService;
        }
        #endregion 

        // GET: ReadingMaterial
        public ActionResult Index()
        {
            ViewData.Model = ReadingMaterialService.Find(r => true);
            return View();
        }

        #region 添加
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 添加阅读材料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(ReadingMaterial model)
        {
            ObjectId temp = new ObjectId(ObjectIdGenerator.Generate());//手动生成一个id，用于接下来的阅读题的选择题作为外键

            model._id = temp;
            ReadingMaterialService.InsertOne(model);
            return RedirectToActionPermanent("Create", "ReadingSingleChoice", new System.Web.Routing.RouteValueDictionary(new { id = temp.ToString() }));
        }
        #endregion
    }
}