using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;

namespace Test.Json.Model
{
    public class PaperSingleChoice
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
    }
}