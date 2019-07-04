using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Text;

using Test.IBLL;
using Test.Mongo.Model;
using Test.Common;

namespace Test.WebApp.Controllers
{
    public class TestPaperController : AdminBaseController
    {
        #region IOC
        /// <summary>
        /// 单选题业务层
        /// </summary>
        public ISingleChoiceBll SingleChoiceService { get; set; }

        /// <summary>
        /// 试卷业务层
        /// </summary>
        public ITestPaperBll TestPaperService { get; set; }

        /// <summary>
        /// redis业务层
        /// </summary>
        public IRedisBll RedisService { get; set; }

        /// <summary>
        /// 全局变量
        /// </summary>
        public IGlobalVariableBll GlobalVariableService { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="singleChoiceService"></param>
        /// <param name="testPaperService"></param>
        /// <param name="redisService"></param>
        /// <param name="globalVariableService"></param>
        public TestPaperController(ISingleChoiceBll singleChoiceService, ITestPaperBll testPaperService, IRedisBll redisService,IGlobalVariableBll globalVariableService)
        {
            this.SingleChoiceService = singleChoiceService;
            this.TestPaperService = testPaperService;
            RedisService = redisService;
            GlobalVariableService = globalVariableService;
        }
        #endregion

        #region 正常情况下（即不恶意刷新页面，不在考试期间直接关闭网站）        
        /// <summary>
        /// 试卷加载
        /// </summary>
        /// <returns></returns>
        public ActionResult Test()
        {
            #region 考试故障检查
            if (Session["paperId"] != null)//已经存在一套正在考试的试卷，即为恶意破坏，跳转至 特殊情况处理页
                return RedirectToAction("Paper");

            Paper paper = TestPaperService.Find(u => u.ExamineeId == ((User)Session["user"])._id).OrderByDescending<Paper,string>(u => u.EndTime).FirstOrDefault();
            if(paper != null)
            {
                /*--考试结束时间早于当前时间，此情况即为考试期间关闭了网站--*/
                if (DateTime.Compare(Convert.ToDateTime(paper.EndTime), DateTime.Now) > 0)
                {
                    Session["paperId"] = paper._id;//打上标记
                    return RedirectToAction("Paper");
                }

                /*--时间已经过了考试终止时间，但是试卷未批阅--*/
                if (string.IsNullOrEmpty(paper.Grade))
                {
                    TestPaperService.MarkTestPaper((ObjectId)Session["paperId"], ((User)Session["user"])._id);//去批阅试卷

                    Session["paper"] = paper._id;
                    return RedirectToAction("TestResult");//前往显示试卷的分数
                }
            }
            #endregion

            BsonArray singleChoiceArray;
            List<SingleChoice> list = TestPaperService.GetSingleChoices(5,out singleChoiceArray);//随机生成单选题
            ViewData["SingleChoice"] = list;

            BsonArray readingMaterialArray;
            List<ReadingMaterial> readings = TestPaperService.GetReadingMaterials(2,out readingMaterialArray);//随机生成阅读题
            ViewData["readings"] = readings;

            ObjectId paperId = new ObjectId(ObjectIdGenerator.Generate());
            Session["paperId"] = paperId;
            SavePaper(paperId, singleChoiceArray, readingMaterialArray);//将试卷存储到mongdb中

            return View();
        }

        /// <summary>
        /// 开始考试，存储开始考试时间
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> BeginTestAsync()
        {
            int temp = int.Parse((GlobalVariableService.Find(u => u.Name == "testTime").FirstOrDefault()).Value);//获取考试可允许的时长
            DateTime testEndTime = DateTime.Now.AddMinutes(temp);
            await SaveTestEndTimeAsync(testEndTime, (ObjectId)Session["paperId"]);
            return Content(testEndTime.ToString());
        }

        /// <summary>
        /// 通过异步的方式，将考试结束时间写入试卷中
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private async Task SaveTestEndTimeAsync(DateTime time, ObjectId paperId)
        {
            await Task.Run(() =>
            {
                TestPaperService.UpdateOne(u => u._id == paperId, time);
            });
        }

        /// <summary>
        /// 开启一个后台线程，此线程将试卷存储到数据库中
        /// </summary>
        /// <param name="choices"></param>
        /// <param name="readings"></param>
        private void SavePaper(ObjectId paperId, BsonArray choices, BsonArray readings)
        {
            ThreadPool.QueueUserWorkItem((state) =>
            {                
                TestPaperService.SavePaper(paperId, ((User)Session["user"])._id, choices, readings);
            });
        }

        /// <summary>
        /// 存储考生的操作
        /// </summary>
        /// <param name="name">题号</param>
        /// <param name="value">答案</param>
        /// <returns></returns>
        public async Task<ActionResult> SaveExamineePlayAsync(string name, string value)
        {
            await SaveAnswer(Session["paperId"].ToString(),name, value);
            return Content("");
        }

        /// <summary>
        /// 使用异步的方式，将考生的答案存储到内存中
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private async Task SaveAnswer(string paperId, string name, string value)
        {
            await Task.Run(() =>
            {
                RedisService.SaveExamineeAnswer(paperId, ((User)Session["user"])._id, name, value);
            });
        }    

        /// <summary>
        /// 处理试卷
        /// </summary>
        /// <returns></returns>
        public ActionResult ToDealWithTestPaper()
        {
            TestPaperService.MarkTestPaper((ObjectId)Session["paperId"], ((User)Session["user"])._id);

            /*paperId改paper,值是没有变的，但是意思不一样：paperId是考试时用的，paper指的是考完了*/
            Session["paper"] = Session["paperId"];
            Session["paperId"] = null;
            return Content("");
        }

        /// <summary>
        /// 显示考试结果
        /// </summary>
        /// <returns></returns>
        public ActionResult TestResult()
        {
            Paper model = (TestPaperService.Find(u => u._id == (ObjectId)Session["paper"]).OrderByDescending(u => u.EndTime).FirstOrDefault());            
            if (!string.IsNullOrEmpty(model.Grade))
                ViewData["Grade"] = model.Grade;
            else
                ViewData["Grade"] = 0;

            return View();
        }
        #endregion

        #region 专治特殊情况
        /// <summary>
        /// 恢复试卷
        /// </summary>
        /// <returns></returns>
        public ActionResult Paper()
        {
            //if(Session["paperId"] == null)           
            //{
            //    Paper paper = TestPaperService.Find(u => u.ExamineeId == (ObjectId)Session["user"]).OrderByDescending(u => u.EndTime).FirstOrDefault();
            //    if(DateTime.Compare(Convert.ToDateTime(paper.EndTime),DateTime.Now) > 0)//考试时间早于当前时间，此情况即为考试期间关闭了网站
            //        Session["paperId"] = paper._id;//打上标记
            //    else//考试已结束
            //    {
            //        if (string.IsNullOrEmpty(paper.Grade))
            //            TestPaperService.MarkTestPaper((ObjectId)Session["paperId"], ((User)Session["user"])._id);//去批阅试卷

            //        Session["paper"] = paper._id;
            //        return RedirectToAction("TestResult");
            //    }
            //}

            StringBuilder singleChoicesString;
            StringBuilder readingsString;
            TestPaperService.RecoverPaper((ObjectId)Session["paperId"], ((User)Session["user"])._id, out singleChoicesString, out readingsString);
            ViewData["singleChoicesString"] = singleChoicesString;
            ViewData["readingsString"] = readingsString;

            return View();
        }

        /// <summary>
        /// 获取考试结束时间
        /// </summary>
        /// <returns></returns>
        public ActionResult GetEndTime()
        {
            BsonDateTime temp = Convert.ToDateTime(TestPaperService.Find(u => u._id == (ObjectId)Session["paperId"]).Select(u => u.EndTime).FirstOrDefault());
            return Content(temp.ToString());
        }
        #endregion
    }
}