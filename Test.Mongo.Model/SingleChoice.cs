using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Test.Mongo.Model
{
    /// <summary>
    /// 单选题
    /// </summary>
    public class SingleChoice
    {
        /// <summary>
        /// id
        /// </summary>
        public ObjectId _id { get; set; }
        /// <summary>
        /// 题目问题
        /// </summary>
        [Required(AllowEmptyStrings = false,ErrorMessage = "不可为空")]
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
