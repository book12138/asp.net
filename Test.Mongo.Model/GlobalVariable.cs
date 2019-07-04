using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Mongo.Model
{
    /// <summary>
    /// 全局变量
    /// </summary>
    public class GlobalVariable
    {
        /// <summary>
        /// id
        /// </summary>
        public ObjectId _id { get; set; }

        /// <summary>
        /// 键
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "不可为空")]
        public string Name { get; set; }

        /// <summary>
        ///值
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "不可为空")]
        public string Value { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "不可为空")]
        public string Describle { get; set; }
    }
}
