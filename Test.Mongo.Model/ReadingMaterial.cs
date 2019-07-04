using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Test.Mongo.Model
{
    /// <summary>
    /// 阅读题阅读材料
    /// </summary>
    public class ReadingMaterial
    {
        public ObjectId _id { get; set; }

        /// <summary>
        /// 阅读材料内容
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "不可为空")]
        public string Content { get; set; }
    }
}
