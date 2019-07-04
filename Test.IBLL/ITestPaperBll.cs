using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

using Test.Mongo.Model;
using MongoDB.Bson;

namespace Test.IBLL
{
    public interface ITestPaperBll : IMongoDBBaseService<Paper>
    {
        /// <summary>
        /// 抽取选择题
        /// </summary>
        /// <param name="number">数量</param>
        /// <param name="singleChoiceArray">存储到数据库的单选题集合</param>
        /// <returns></returns>
        List<SingleChoice> GetSingleChoices(int number, out BsonArray singleChoiceArray);

        /// <summary>
        /// 抽取阅读题
        /// </summary>
        /// <param name="number"></param>
        /// <param name="readingSingleChoiceArray">存储到数据库的阅读部分选择题集合</param>
        /// <returns></returns>
        List<ReadingMaterial> GetReadingMaterials(int number, out BsonArray readingSingleChoiceArray);

        /// <summary>
        /// 存储整张试卷
        /// </summary>
        /// <param name="startTime">考试开始时间</param>
        /// <param name="examineeId">考生id</param>
        /// <param name="choices">单选题</param>
        /// <param name="readings">阅读部分单选题</param>
        void SavePaper(ObjectId paperId, ObjectId examineeId, BsonArray choices, BsonArray readings);

        /// <summary>
        /// 批阅试卷
        /// </summary>
        /// <param name="paperId">试卷id</param>
        /// <param name="examineeId">考生id</param>
        Task MarkTestPaper(ObjectId paperId, ObjectId examineeId);

        /// <summary>
        /// 修改单篇文档
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="bSingleChoiceArray"></param>
        /// <param name="bReadingSingleChoiceArray"></param>
        /// <param name="Grade"></param>
        /// <returns></returns>
        bool UpdateOne(Expression<Func<Paper, bool>> filter, BsonArray bSingleChoiceArray, BsonArray bReadingSingleChoiceArray, int Grade);

        /// <summary>
        /// 修改考试结束时间
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        bool UpdateOne(Expression<Func<Paper, bool>> filter, DateTime endTime);

        /// <summary>
        /// 恢复正在进行的试卷
        /// </summary>
        /// <returns></returns>
        void RecoverPaper(ObjectId paperId, ObjectId examineeId, out StringBuilder singleChoicesString, out StringBuilder readingsString);
    }
}
