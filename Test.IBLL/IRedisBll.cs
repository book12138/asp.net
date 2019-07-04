using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Driver;
using MongoDB.Bson;
using Test.Mongo.Model;

namespace Test.IBLL
{
    public interface IRedisBll
    {
        IMongoCollection<SingleChoice> SingleChoiceCollection { get; set; }

        IMongoCollection<ReadingMaterial> ReadingMaterialCollection { get; set; }

        /// <summary>
        /// 读取mongodb中所有单选题的id，存储到redis中，并且标识数字编号
        /// </summary>
        void SaveAllSingleChoiceId();

        /// <summary>
        /// 将mongodb数据库中所有阅读题的id，存储到redis中，并标识数字编号
        /// </summary>
        void SaveAllReadingMaterialId();

        /// <summary>
        /// 存储考生每一题的答案       
        /// </summary>
        /// <param name="examineeId">考生id</param>
        /// <param name="topicId">题目id</param>
        /// <param name="answer">答案</param>
        void SaveExamineeAnswer(string paperId, ObjectId examineeId, string topicId, string answer);
    }
}
