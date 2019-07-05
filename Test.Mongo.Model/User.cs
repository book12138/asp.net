using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;
using Test.Enum;

namespace Test.Mongo.Model
{
    /// <summary>
    /// 用户
    /// </summary>
    [DataContract(IsReference = true , Name = "UserInfo")]
    public class User
    {
        /// <summary>
        /// id
        /// </summary>
        [DataMember]
        public ObjectId _id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "不可为空")]
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "不可为空")]
        [DataMember]
        public string PassWord { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "不可为空")]
        [DataMember]
        public string EMail { get; set; }

        /// <summary>
        /// 用户身份
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "不可为空")]
        [DataMember]
        public UserRank Rank { get; set; }
    }
}
