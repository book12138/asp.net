using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;

namespace Test.Json.Model
{
    public class PaperReadingSingleChoice
    {
        /// <summary>
        /// 题目id
        /// </summary>
        [JsonProperty("_id")]
        public string _id { get; set; }

        /// <summary>
        /// 答案
        /// </summary>
        [JsonProperty("Answer")]
        public string Answer { get; set; }

        /// <summary>
        /// 阅读材料id
        /// </summary>
        [JsonProperty("ReadingId")]
        public string ReadingId { get; set; }
    }
}