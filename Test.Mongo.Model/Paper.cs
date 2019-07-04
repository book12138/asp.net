using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;

namespace Test.Mongo.Model
{
    public class Paper
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Paper() => Grade = string.Empty;

        /// <summary>
        /// id
        /// </summary>
        public ObjectId _id { get; set; }

        /// <summary>
        /// 考生id
        /// </summary>
        public ObjectId ExamineeId { get; set; }

        /// <summary>
        /// 考试结束时间
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 单选题集
        /// </summary>
        public BsonArray SingleChocies { get; set; }

        /// <summary>
        /// 阅读题集
        /// </summary>
        public BsonArray ReadingMaterials { get; set; }

        /// <summary>
        /// 分数(因为有可能需要用到分数字段来判别是否考试完毕，所以前期用一个empty来知道未考完)
        /// </summary>
        public string Grade { get; set; }
    }
}