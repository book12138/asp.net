using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

using Test.IBLL;
using MongoDB.Driver;
using Test.Mongo.Model;
using MongoDB.Bson;
using Test.Redis;

namespace Test.BLL
{
    public class RedisBll : IRedisBll
    {
        /// <summary>
        /// 单选题集合
        /// </summary>
        public IMongoCollection<SingleChoice> SingleChoiceCollection { get; set; }

        /// <summary>
        /// 阅读题阅读材料集合
        /// </summary>
        public IMongoCollection<ReadingMaterial> ReadingMaterialCollection { get; set; }

        public IReadingSingleChoiceBll ReadingSingleChoiceService = new ReadingSingleChoiceBll();

        /// <summary>
        /// 构造函数（初始化mongodb数据库的集合）
        /// </summary>
        public RedisBll()
        {
            var client = new MongoClient(ConfigurationManager.ConnectionStrings["MongoDBConstr"].ConnectionString);//连接mongo服务器
            var db = client.GetDatabase("EnglishItemPool");//连接数据库(如果服务器上不存在该数据库，则首次使用时将自动创建该数据库)
            var collection = db.GetCollection<SingleChoice>("SingleChoice");//对接MultipleChoice集合
            this.SingleChoiceCollection = collection;
            var readingMaterialCollection = db.GetCollection<ReadingMaterial>("ReadingMaterial");//对接ReadingMaterial集合
            this.ReadingMaterialCollection = readingMaterialCollection;
        }

        /// <summary>
        /// 读取mongodb中所有单选题的id，存储到redis中，并且标识数字编号
        /// </summary>
        public void SaveAllSingleChoiceId()
        {
            /*获取所有单选题的id*/
            var ids = from m in SingleChoiceCollection.AsQueryable()
                      select m._id;

            var redis = new RedisHelper(1);
            bool result = redis.KeyDelete("SingleChoice");
            

            int temp = 0;
            foreach (var item in ids)
            {
                redis.StringSet<ObjectId>("SingleChoice:" + temp.ToString(), item);
                temp++;
            }
        }

        /// <summary>
        /// 将mongodb数据库中所有阅读题的id，存储到redis中，并标识数字编号
        /// </summary>
        public void SaveAllReadingMaterialId()
        {
            /*获取所有阅读题的阅读材料id*/
            var ids = from m in ReadingMaterialCollection.AsQueryable()
                      select m._id;

            var redis = new RedisHelper(1);
            bool result = redis.KeyDelete("ReadingMaterial");

            int temp = 0;
            foreach (var item in ids)
            {
                redis.StringSet<ObjectId>("ReadingMaterial:" + temp.ToString(), item);
                temp++;
            }
        }

        /// <summary>
        /// 存储考生每一题的答案       
        /// </summary>
        /// <param name="examineeId">考生id</param>
        /// <param name="topicId">题目id</param>
        /// <param name="answer">答案</param>
        public void SaveExamineeAnswer(string paperId,ObjectId examineeId,string topicId,string answer)
        {
            var redis = new RedisHelper(2);

            redis.StringSet(examineeId.ToString() + ":" + paperId + ":" + topicId, answer);
            //redis.ListRightPush(examineeId.ToString() + ":" + paperId, topicId + "&" + answer);
        }
    }
}
