using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Test.IBLL;
using Test.Mongo.Model;

namespace Test.WebApp.Controllers
{
    public class GlobalVariableController : AdminBaseController
    {
        #region IOC
        public IGlobalVariableBll GlobalVariableService { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="globalVariableService"></param>
        public GlobalVariableController(IGlobalVariableBll globalVariableService) => GlobalVariableService = globalVariableService;
        #endregion

        // GET: GlobalVariable
        public ActionResult Index()
        {
            ViewData.Model = GlobalVariableService.Find(u => true);
            return View();
        }

        // GET: GlobalVariable/Details/5
        public ActionResult Details(string id)
        {
            ViewData.Model = GlobalVariableService.Find(u => u._id == new ObjectId(id)).FirstOrDefault();
            return View();
        }

        // GET: GlobalVariable/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GlobalVariable/Create
        [HttpPost]
        public ActionResult Create(GlobalVariable model)
        {
            GlobalVariableService.InsertOne(model);
            return RedirectToAction("Index");
        }

        // GET: GlobalVariable/Edit/5
        public ActionResult Edit(string id)
        {
            ViewData.Model = GlobalVariableService.Find(u => u._id == new ObjectId(id)).FirstOrDefault();
            return View();
        }

        // POST: GlobalVariable/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, GlobalVariable model)
        {
            GlobalVariableService.UpdateOne(u => u._id == new ObjectId(id), model);
            return RedirectToAction("Index");
        }

        // GET: GlobalVariable/Delete/5
        public ActionResult Delete(string id)
        {
            GlobalVariableService.DeleteOne(u => u._id == new ObjectId(id));
            return RedirectToAction("Index");
        }

        // POST: GlobalVariable/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, GlobalVariable model)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
