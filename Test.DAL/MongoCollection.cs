using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


using MongoDB.Driver;
using MongoDB.Bson;

namespace Test.DAL
{
    public class MongoCollection<T> where T : class , new()
    {
        public static IMongoCollection<T>  GetCollection(string dataBaseName,string collectionName)
        {
            var client = new MongoClient(ConfigurationManager.ConnectionStrings["MongoDBConstr"].ConnectionString);//连接mongo服务器
            var db = client.GetDatabase(dataBaseName);//连接数据库(如果服务器上不存在该数据库，则首次使用时将自动创建该数据库)
            var collection = db.GetCollection<T>(collectionName);//对接某一个集合
            return collection;
        }
    }
}
