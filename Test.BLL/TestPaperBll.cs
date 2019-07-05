using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

using Test.IBLL;
using Test.DALFactory;
using Test.Mongo.Model;
using Test.Json.Model;
using Test.IDAL;
using MongoDB.Bson;
using Test.Redis;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Test.Common;
using MongoDB.Driver;

namespace Test.BLL
{
    /// <summary>
    /// 试卷
    /// </summary>
    public class TestPaperBll : MongoDBBaseService<Paper> , ITestPaperBll
    {
        public override void SetCurrentDal() => base.CurrentDal = DbSession.PaperDal;       

        #region 数据层实例
        /// <summary>
        /// 单选题型的数据层实例
        /// </summary>
        private ISingleChoiceDal SingleChoiceDal => DbSession.SingleChoiceDal;

        /// <summary>
        /// 阅读题型的数据层实例
        /// </summary>
        private IReadingMaterialDal ReadingMaterialDal => DbSession.ReadingMaterialDal;

        /// <summary>
        /// 阅读题中单选题的数据层实例
        /// </summary>
        private IReadingSingleChoiceDal ReadingSingleChoiceDal => DbSession.ReadingSingleChoiceDal;

        /// <summary>
        /// 试卷
        /// </summary>
        private IPaperDal PaperDal => DbSession.PaperDal;

        /// <summary>
        /// 全局变量
        /// </summary>
        private IGlobalVariableDal GlobalVariableDal => DbSession.GlobalVariableDal;
        #endregion

        /// <summary>
        /// 抽取选择题
        /// </summary>
        /// <param name="number">数量</param>
        /// <param name="singleChoiceArray">存储到数据库的单选题集合</param>
        /// <returns></returns>
        public List<SingleChoice> GetSingleChoices(int number,out BsonArray singleChoiceArray)
        {
            singleChoiceArray = new BsonArray();
            List<SingleChoice> singleChoices = new List<SingleChoice>();//最终存放试卷的单选题部分

            List<int> serialNumber = new List<int>();//序号列表,接下来抽题从此抽出序号            
            int length = SingleChoiceDal.Find(u => true).Count();//获取题库中单选题的总数
            while (length-- > 0)
                serialNumber.Add(length);

            /*开始抽序号*/
            Random random = new Random();
            List<int> result = new List<int>();//用于存放序号的抽取结果
            while (number-- > 0)
            {
                if (serialNumber.Count == 0)//无题可抽，则退出
                    break;
                int temp = random.Next(0, serialNumber.Count);//按照序号列表的长度，抽数字下标索引
                int element = serialNumber[temp];
                result.Add(element);//按照数字索引取值
                serialNumber.Remove(element);//移除刚才取出来的值
            }

            /*按照抽到的序号集合，从redis中取出对应题目的id,然后根据id，在mongodb中取数据*/
            var redis = new RedisHelper(1);
            foreach (var item in result)
            {
                ObjectId id = redis.StringGet<ObjectId>("SingleChoice:" + item.ToString());
                SingleChoice model = SingleChoiceDal.Find(u => u._id == id).FirstOrDefault<SingleChoice>();
                singleChoices.Add(model);

                //singleChoiceArray.Add(
                //    new BsonDocument() {
                //        new BsonElement("TopicId", model._id),
                //        new BsonElement("Answer", string.Empty)
                //    });

                singleChoiceArray.Add(model._id);//空白试卷中存储单选题的id
            }

            return singleChoices;
        }

        /// <summary>
        /// 抽取阅读题
        /// </summary>
        /// <param name="number"></param>
        /// <param name="readingSingleChoiceArray">存储到数据库的阅读部分选择题集合</param>
        /// <returns></returns>
        public List<ReadingMaterial> GetReadingMaterials(int number,out BsonArray readingArray)
        {
            readingArray = new BsonArray();
            List<ReadingMaterial> readingMaterials = new List<ReadingMaterial>();//最终存放试卷的单选题部分

            List<int> serialNumber = new List<int>();//序号列表,接下来抽题从此抽出序号            
            int length = ReadingMaterialDal.Find(u => true).Count();//获取题库中单选题的总数
            while (length-- > 0)
                serialNumber.Add(length);

            /*开始抽序号*/
            Random random = new Random();
            List<int> result = new List<int>();//用于存放序号的抽取结果
            while (number-- > 0)
            {
                if (serialNumber.Count == 0)//无题可抽，则退出
                    break;
                int temp = random.Next(0, serialNumber.Count);//按照序号列表的长度，抽数字下标索引
                int element = serialNumber[temp];
                result.Add(element);//按照数字索引取值
                serialNumber.Remove(element);//移除刚才取出来的值
            }

            /*按照抽到的序号集合，从redis中取出对应题目的id,然后根据id，在mongodb中取数据*/
            var redis = new RedisHelper(1);
            foreach (var item in result)
            {
                ObjectId id = redis.StringGet<ObjectId>("ReadingMaterial:" + item.ToString());
                ReadingMaterial model = ReadingMaterialDal.Find(u => u._id == id).FirstOrDefault<ReadingMaterial>();
                readingMaterials.Add(model);

                //foreach (var choice in ReadingSingleChoiceDal.Find(u=>u.ReadingId == model._id))
                //{
                //    readingSingleChoiceArray.Add(
                //        new BsonDocument() {
                //            new BsonElement("TopicId", choice._id),
                //            new BsonElement("Answer", string.Empty),
                //            new BsonElement("ReadingId", model._id)
                //        }
                //   );
                //}

                readingArray.Add(model._id);//空白试卷中存储阅读材料id
            }

            return readingMaterials;
        }

        /// <summary>
        /// 存储整张试卷
        /// </summary>
        /// <param name="startTime">考试开始时间</param>
        /// <param name="examineeId">考生id</param>
        /// <param name="choices">单选题</param>
        /// <param name="readings">阅读部分单选题</param>
        public void SavePaper(ObjectId paperId, ObjectId examineeId, BsonArray choices, BsonArray readings)
        {
            IPaperDal paperDal = new Test.DAL.PaperDal();
            paperDal.InsertOne(new Paper
            {
                _id = paperId,
                EndTime = DateTime.Now.AddMinutes(10).ToString(),
                ExamineeId = examineeId,
                SingleChocies = choices,
                ReadingMaterials = readings
            });
        }

        /// <summary>
        /// 批阅试卷
        /// </summary>
        /// <param name="paperId">试卷id</param>
        /// <param name="examineeId">考生id</param>
        public async Task MarkTestPaper(ObjectId paperId, ObjectId examineeId)
        {
            int grade = 0;//最终试卷分数

            var redis = new RedisHelper(2);

            int singleChoiceScore = int.Parse((GlobalVariableDal.Find(u => u.Name == "singleChoiceScore").FirstOrDefault().Value));//每一道单选的分值
            int readingSinlgeChoiceScore = int.Parse((GlobalVariableDal.Find(u => u.Name == "readingSinlgeChoiceScore").FirstOrDefault().Value));//每一道阅读题下每一个单选题的分值

            Paper paper = PaperDal.Find(u => u._id == paperId).FirstOrDefault();//从数据库拿到考生的空白试卷
            if(paper != null)
            {
                /*按照空白卷的题号，依次从redis中取出考生的答案，重组试卷，并计算得分*/

                /*--1.单选题--*/
                BsonArray bSingleChoiceArray = new BsonArray();
                foreach (var item in paper.SingleChocies)
                {
                    string answer = redis.StringGet(examineeId.ToString() + ":" + paperId.ToString() + ":" + item.ToString());//从redis中取出对应试卷的对应题号的考生答案
                    redis.KeyDelete(examineeId.ToString() + ":" + paperId.ToString() + ":" + item.ToString());//取完答案就删了他
                    SingleChoice choice = SingleChoiceDal.Find(u => u._id == item).FirstOrDefault();
                    if (choice != null)
                    {
                        if (!string.IsNullOrEmpty(answer))//考生做了这题，没有留白
                        {
                            /*取正确答案与考生答案进行对比，计分*/                            
                            if (choice.Answer.Trim() == answer.Trim())
                                grade += singleChoiceScore;

                            /*重组该题*/
                            bSingleChoiceArray.Add(
                                new BsonDocument() {
                                new BsonElement("_id", choice._id),
                                new BsonElement("Answer", answer) });
                        }
                        else
                            bSingleChoiceArray.Add(
                                new BsonDocument() {
                                new BsonElement("_id", choice._id),
                                new BsonElement("Answer", string.Empty) });
                    }
                }

                /*--2.阅读题的单选题--*/
                BsonArray bReadingSingleChoiceArray = new BsonArray();
                foreach (var item in paper.ReadingMaterials)
                {
                    foreach (var choice in ReadingSingleChoiceDal.Find(u => u.ReadingId == item))
                    {
                        string answer = redis.StringGet(examineeId.ToString() + ":" + paperId.ToString() + ":" + choice._id.ToString());//从redis中取出对应试卷的对应题号的考生答案
                        redis.KeyDelete(examineeId.ToString() + ":" + paperId.ToString() + ":" + choice._id.ToString());//取完答案就删了他
                        if (!string.IsNullOrEmpty(answer))//考生做了这题，没有留白
                        {
                            /*取正确答案与考生答案进行对比，计分*/
                            if (choice.Answer.Trim() == answer.Trim())
                                grade += readingSinlgeChoiceScore;

                            /*重组该题*/
                            bReadingSingleChoiceArray.Add(
                                new BsonDocument() {
                                new BsonElement("_id", choice._id),
                                new BsonElement("ReadingId",choice.ReadingId),
                                new BsonElement("Answer", answer) });
                        }
                        else
                            bReadingSingleChoiceArray.Add(
                                new BsonDocument() {
                                new BsonElement("_id", choice._id),
                                new BsonElement("ReadingId",choice.ReadingId),
                                new BsonElement("Answer", string.Empty) });
                    }
                }

                UpdateOne(u => u._id == paperId, bSingleChoiceArray, bReadingSingleChoiceArray, grade);//一张完整的试卷，替换原空白卷，重新存入数据库
                await redis.KeyDeleteAsync(examineeId.ToString() + ":" + paperId.ToString());//把redis中的试卷记录删了
            }
        }

        /// <summary>
        /// 修改单篇文档
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="bSingleChoiceArray"></param>
        /// <param name="bReadingSingleChoiceArray"></param>
        /// <param name="Grade"></param>
        /// <returns></returns>
        public bool UpdateOne(Expression<Func<Paper, bool>> filter, BsonArray bSingleChoiceArray,BsonArray bReadingSingleChoiceArray,int Grade)
        {
            var update = Builders<Paper>.Update.Set("SingleChocies", bSingleChoiceArray).Set("ReadingMaterials", bReadingSingleChoiceArray).Set("Grade", Grade);
            return UpdateOne(filter, update);
        }

        /// <summary>
        /// 修改考试结束时间
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public bool UpdateOne(Expression<Func<Paper, bool>> filter,DateTime endTime)
        {
            var update = Builders<Paper>.Update.Set("EndTime", endTime.ToString());
            return UpdateOne(filter, update);
        }

        /// <summary>
        /// 恢复正在进行的试卷
        /// </summary>
        /// <returns></returns>
        public void RecoverPaper(ObjectId paperId, ObjectId examineeId,out StringBuilder singleChoicesString,out StringBuilder readingsString)
        {
            readingsString = new StringBuilder();
            singleChoicesString = new StringBuilder();
            var redis = new RedisHelper(2);

            Paper paper = PaperDal.Find(u => u._id == paperId).FirstOrDefault();//从数据库拿到考生的空白试卷
            if (paper != null)
            {
                /*按照空白卷的题号，依次从redis中取出考生的答案，重组试卷，并计算得分*/

                /*--1.单选题--*/
                foreach (var item in paper.SingleChocies)
                {
                    SingleChoice model = SingleChoiceDal.Find(u => u._id == new ObjectId(item.ToString())).FirstOrDefault();
                    string answer = redis.StringGet(examineeId.ToString() + ":" + paperId.ToString() + ":" + item.ToString());//从redis中取出对应试卷的对应题号的考生答案
                    #region 考生做了这题，没有留白
                    if (!string.IsNullOrEmpty(answer))
                    {
                        /*前缀*/
                        singleChoicesString.AppendFormat("<div class='form-group'>" +
                        "<div class='col-md-offset-1'>" +
                            "<p><strong>{0}</strong></p>" +
                            "<div _id='{1}' >",model.Question,model.ToString());

                        /*四选项*/
                        if (model.A.Trim() == answer.Trim())//考生选中的是此选项
                            singleChoicesString.AppendFormat("<div class='radio'>" +
                                        "<label>" +
                                            "<input type='radio' name='{0}' value='{1}' checked='checked' />A、{1}" +
                                          "</label>" +
                                    "</div>", model._id.ToString(), model.A);
                        else
                            singleChoicesString.AppendFormat("<div class='radio'>" +
                                        "<label>" +
                                            "<input type = 'radio' name='{0}' value='{1}' />A、{1}" +
                                          "</label>" +
                                    "</div>",model._id.ToString(),model.A);

                        if (model.B.Trim() == answer.Trim())//考生选中的是此选项
                            singleChoicesString.AppendFormat("<div class='radio'>" +
                                        "<label>" +
                                            "<input type = 'radio' name='{0}' value='{1}' checked= 'checked' />B、{1}" +
                                          "</label>" +
                                    "</div>", model._id.ToString(), model.B);
                        else
                            singleChoicesString.AppendFormat("<div class='radio'>" +
                                        "<label>" +
                                            "<input type = 'radio' name='{0}' value='{1}' />B、{1}" +
                                          "</label>" +
                                    "</div>", model._id.ToString(), model.B);

                        if (model.C.Trim() == answer.Trim())//考生选中的是此选项
                            singleChoicesString.AppendFormat("<div class='radio'>" +
                                        "<label>" +
                                            "<input type = 'radio' name='{0}' value='{1}' checked= 'checked' />C、{1}" +
                                          "</label>" +
                                    "</div>", model._id.ToString(), model.C);
                        else
                            singleChoicesString.AppendFormat("<div class='radio'>" +
                                        "<label>" +
                                            "<input type = 'radio' name='{0}' value='{1}' />C、{1}" +
                                          "</label>" +
                                    "</div>", model._id.ToString(), model.C);

                        if (model.D.Trim() == answer.Trim())//考生选中的是此选项
                            singleChoicesString.AppendFormat("<div class='radio'>" +
                                        "<label>" +
                                            "<input type = 'radio' name='{0}' value='{1}' checked= 'checked' />D、{1}" +
                                          "</label>" +
                                    "</div>", model._id.ToString(), model.D);
                        else
                            singleChoicesString.AppendFormat("<div class='radio'>" +
                                        "<label>" +
                                            "<input type = 'radio' name='{0}' value='{1}' />D、{1}" +
                                          "</label>" +
                                    "</div>", model._id.ToString(), model.D);


                        /*后缀*/
                        singleChoicesString.Append("</div>" +
                        "</div>" +
                    "</div>" +
                    "<hr />");
                    }
                    #endregion
                    #region 该题考生没做
                    else
                        singleChoicesString.AppendFormat("<div class='form-group'>" +
                        "<div class='col-md-offset-1'>" +
                            "<p><strong>{0}</strong></p>" +
                            "<div _id = '{1}' >" +
                                "<div class='radio'>" +
                                    "<label>" +
                                        "<input type = 'radio' name='{1}' value='{2}' />A、{2}" +
                                      "</label>" +
                                "</div>" +
                                "<div class='radio'>" +
                                    "<label>" +
                                        "<input type = 'radio' name='{1}' value='{3}' />B、{3}" +
                                      "</label>" +
                                "</div>" +
                                "<div class='radio'>" +
                                    "<label>" +
                                        "<input type = 'radio' name='{1}' value='{4}' />C、{4}" +
                                      "</label>" +
                                "</div>" +
                                "<div class='radio'>" +
                                    "<label>" +
                                        "<input type = 'radio' name='{1}' value='{5}' />D、{5}" +
                                      "</label>" +
                                "</div>" +
                            "</div>" +
                        "</div>" +
                    "</div>" +
                    "<hr />", model.Question, model._id.ToString(), model.A, model.B, model.C, model.D);
                    #endregion
                }

                /*--2.阅读题的单选题--*/
                foreach (var item in paper.ReadingMaterials)
                {
                    readingsString.AppendFormat("<p style='text-indent:2em; width: 1000px; margin: auto; color:#494848;line-height:28px;'>{0}</p><br />", (ReadingMaterialDal.Find(u => u._id == item).FirstOrDefault()).Content);
                    foreach (var model in ReadingSingleChoiceDal.Find(u => u.ReadingId == new ObjectId(item.ToString())))
                    {
                        string answer = redis.StringGet(examineeId.ToString() + ":" + paperId.ToString() + ":" + model._id.ToString());//从redis中取出对应试卷的对应题号的考生答案
                        #region 考生做了这题，没有留白
                        if (!string.IsNullOrEmpty(answer))
                        {
                            /*前缀*/
                            readingsString.AppendFormat("<div class='form-group'>" +
                            "<div class='col-md-offset-1'>" +
                                "<p><strong>{0}</strong></p>" +
                                "<div _id = '{1}' >", model.Question, model.ToString());

                            /*四选项*/
                            if (model.A.Trim() == answer.Trim())//考生选中的是此选项
                                readingsString.AppendFormat("<div class='radio'>" +
                                            "<label>" +
                                                "<input type = 'radio' name='{0}' value='{1}' checked= 'checked' />A、{1}" +
                                              "</label>" +
                                        "</div>", model._id.ToString(), model.A);
                            else
                                readingsString.AppendFormat("<div class='radio'>" +
                                            "<label>" +
                                                "<input type = 'radio' name='{0}' value='{1}' />A、{1}" +
                                              "</label>" +
                                        "</div>", model._id.ToString(), model.A);

                            if (model.B.Trim() == answer.Trim())//考生选中的是此选项
                                readingsString.AppendFormat("<div class='radio'>" +
                                            "<label>" +
                                                "<input type = 'radio' name='{0}' value='{1}' checked= 'checked' />B、{1}" +
                                              "</label>" +
                                        "</div>", model._id.ToString(), model.B);
                            else
                                readingsString.AppendFormat("<div class='radio'>" +
                                            "<label>" +
                                                "<input type = 'radio' name='{0}' value='{1}' />B、{1}" +
                                              "</label>" +
                                        "</div>", model._id.ToString(), model.B);

                            if (model.C.Trim() == answer.Trim())//考生选中的是此选项
                                readingsString.AppendFormat("<div class='radio'>" +
                                            "<label>" +
                                                "<input type = 'radio' name='{0}' value='{1}' checked= 'checked' />C、{1}" +
                                              "</label>" +
                                        "</div>", model._id.ToString(), model.C);
                            else
                                readingsString.AppendFormat("<div class='radio'>" +
                                            "<label>" +
                                                "<input type = 'radio' name='{0}' value='{1}' />C、{1}" +
                                              "</label>" +
                                        "</div>", model._id.ToString(), model.C);

                            if (model.D.Trim() == answer.Trim())//考生选中的是此选项
                                readingsString.AppendFormat("<div class='radio'>" +
                                            "<label>" +
                                                "<input type = 'radio' name='{0}' value='{1}' checked= 'checked' />D、{1}" +
                                              "</label>" +
                                        "</div>", model._id.ToString(), model.D);
                            else
                                readingsString.AppendFormat("<div class='radio'>" +
                                            "<label>" +
                                                "<input type = 'radio' name='{0}' value='{1}' />D、{1}" +
                                              "</label>" +
                                        "</div>", model._id.ToString(), model.D);


                            /*后缀*/
                            readingsString.Append("</div>" +
                            "</div>" +
                        "</div>" +
                        "<hr />");
                        }
                        #endregion
                        #region 该考生没做
                        else
                            readingsString.AppendFormat("<div class='form-group'>" +
                          "<div class='col-md-offset-1'>" +
                              "<p><strong>{0}</strong></p>" +
                              "<div _id = '{1}' >" +
                                  "<div class='radio'>" +
                                      "<label>" +
                                          "<input type = 'radio' name='{1}' value='{2}' />A、{2}" +
                                        "</label>" +
                                  "</div>" +
                                  "<div class='radio'>" +
                                      "<label>" +
                                          "<input type = 'radio' name='{1}' value='{3}' />B、{3}" +
                                        "</label>" +
                                  "</div>" +
                                  "<div class='radio'>" +
                                      "<label>" +
                                          "<input type = 'radio' name='{1}' value='{4}' />C、{4}" +
                                        "</label>" +
                                  "</div>" +
                                  "<div class='radio'>" +
                                      "<label>" +
                                          "<input type = 'radio' name='{1}' value='{5}' />D、{5}" +
                                        "</label>" +
                                  "</div>" +
                              "</div>" +
                          "</div>" +
                      "</div>" +
                      "<hr />", model.Question, model._id.ToString(), model.A, model.B, model.C, model.D);
                        #endregion
                    }
                }
            }
        }
    }
}
