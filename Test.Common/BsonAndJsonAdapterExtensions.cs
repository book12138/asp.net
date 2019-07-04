using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Common
{
    /// <summary>
    /// mongodb的bson和json之间的适配转换扩展
    /// </summary>
    public static class BsonAndJsonAdapterExtensions
    {
        #region json to bson
        /// <summary>
        /// 转换成为BsonDocument
        /// </summary>
        /// <param name="object"></param>
        /// <returns></returns>
        public static BsonDocument ToBsonDocument(this JObject @object)
        {
            IDictionary<String, Object> elems = new Dictionary<String, Object>(); ;
            foreach (var item in @object)
                elems[item.Key] = ToBsonValue(item.Value);

            return new BsonDocument(elems);
        }

        /// <summary>
        /// 转换为BsonArray
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static BsonArray ToBsonArray(this JArray array)
        {
            IList<BsonValue> bvs = new List<BsonValue>();
            foreach (JToken item in array)
                bvs.Add(ToBsonValue(item));
            return new BsonArray(bvs);
        }

        /// <summary>
        /// 转换为BsonValue
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private static BsonValue ToBsonValue(this JToken val)
        {
            if (val is JArray)
                return ToBsonArray(val as JArray);
            if (val is JObject)
                return ToBsonDocument(val as JObject);
            else
                return ToBasicValue(val);
        }

        /// <summary>
        /// 转换为BasicValue
        /// </summary>
        /// <param name="object"></param>
        /// <returns></returns>
        private static BsonValue ToBasicValue(JToken @object)
        {
            switch (@object.Type)
            {
                case JTokenType.Integer:
                    return BsonValue.Create(@object.ToObject<Int32>());
                case JTokenType.Float:
                    return BsonValue.Create(@object.ToObject<float>());
                case JTokenType.String:
                    return BsonValue.Create(@object.ToObject<String>());
                case JTokenType.Boolean:
                    return BsonValue.Create(@object.ToObject<Boolean>());
                case JTokenType.Date:
                    return BsonValue.Create(@object.ToObject<DateTime>());
                case JTokenType.Guid:
                    return BsonValue.Create(@object.ToObject<Guid>());
                case JTokenType.Null:
                    return null;
                default:
                    throw new Exception("");
            }
        }
        #endregion

        #region bson to json
        /// <summary>
        /// 转json对象
        /// </summary>
        /// <param name="bsonDocument"></param>
        /// <returns></returns>
        public static JObject ToJObject(this BsonDocument bsonDocument)
        {
            JObject elems = new JObject();
            foreach (var item in bsonDocument)
            {
                Object data = item.Value;
                elems[item.Name] = ToJsonValue(item.Value);
            }
            return elems;
        }

        /// <summary>
        /// 转json数组
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static JArray ToJArray(this BsonArray array)
        {
            IList<JToken> bvs = new List<JToken>();
            foreach (var item in array)
                bvs.Add(ToJsonValue(item));
            return new JArray(bvs);
        }

        /// <summary>
        /// 转json值
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private static JToken ToJsonValue(this BsonValue val)
        {
            if (val is BsonArray)
                return ToJArray(val as BsonArray);
            if (val is BsonDocument)
                return ToJObject(val as BsonDocument);
            if (val is BsonNull)
                return null;
            return JToken.FromObject(val);
        }
        #endregion
    }
}
