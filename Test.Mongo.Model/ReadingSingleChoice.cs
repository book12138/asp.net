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
    /// 对应每一篇阅读材料的
    /// </summary>
    public class ReadingSingleChoice
    {
        /// <summary>
        /// id
        /// </summary>
        public ObjectId _id { get; set; }

        /// <summary>
        /// 阅读材料id
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "不可为空")]
        public ObjectId ReadingId { get; set; }

        /// <summary>
        /// 题目问题
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "不可为空")]
        public string Question { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "不可为空")]
        public string A { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "不可为空")]
        public string B { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "不可为空")]
        public string C { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "不可为空")]
        public string D { get; set; }

        /// <summary>
        /// 正确答案
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "不可为空")]
        public string Answer { get; set; }
    }
}
