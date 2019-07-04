using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Test.DAL
{
    public abstract class MongoBaseDal<T> where T : class , new()
    {
        /// <summary>
        /// mongo集合
        /// </summary>
        public IMongoCollection<T> MongoCollection { get; set; }

        /// <summary>
        /// 子类必须重写该方法，通过子类的重写，实现mongo的初始化
        /// </summary>
        public abstract void LoadMongo();

        /// <summary>
        /// 构造方法（当子类在编译时，会执行此构造方法，从而实现主动执行子类重写的LoadMongo方法，完成mongo初始化）
        /// </summary>
        public MongoBaseDal() => LoadMongo();

        /// <summary>
        /// 修改单篇文档，而且仅能修改某一个字段的值(即使有多篇文档符合匹配条件，但是也仅仅只会修改第一篇)
        /// </summary>
        /// <param name="filter">条件</param>
        /// <param name="update">新值</param>
        /// <returns></returns>
        public bool UpdateOne(Expression<Func<T, bool>> filter, UpdateDefinition<T> update)
        {
            //var filter = Builders<MultipleChoiceDal>.Filter.Eq("_id", model._id);//查找需要修改的文档的条件部分

            ///*最终要修改的文档内容*/
            //var update = Builders<MultipleChoiceDal>.Update.Set("Question", model.Question);

            var result = this.MongoCollection.UpdateOne(filter, update);            
            return result.ModifiedCount > 0;//ModifiedCount —— 受影响的行数
        }

        /// <summary>
        /// 修改多篇文档
        /// </summary>
        /// <param name="filter">条件</param>
        /// <param name="update">新值</param>
        /// <returns></returns>
        public bool UpdateMany(Expression<Func<T, bool>> filter,UpdateDefinition<T> update)
        {
            var result = MongoCollection.UpdateMany(filter, update);
            return result.ModifiedCount > 0;
        }

        /// <summary>
        /// 插入单篇文档
        /// </summary>
        /// <param name="model"></param>
        public void InsertOne(T model)=> this.MongoCollection.InsertOne(model);

        /// <summary>
        /// 插入多篇文档
        /// </summary>
        /// <param name="models"></param>
        public void InsertMany(IEnumerable<T> models) => MongoCollection.InsertMany(models);

        /// <summary>
        /// 删除一篇文档（即使有多篇文档符合匹配条件，但是也仅仅只会删除第一篇）
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public bool DeleteOne(Expression<Func<T, bool>> filter)
        {
            var result = this.MongoCollection.DeleteOne(filter);
            return result.DeletedCount > 0;
        }

        /// <summary>
        /// 删除多篇文档
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public bool DeleteMany(Expression<Func<T, bool>> filter)
        {
            var result = this.MongoCollection.DeleteMany(filter);
            return result.DeletedCount > 0;
        }

        /// <summary>
        /// 查找文档
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<T> Find(Expression<Func<T, bool>> filter)
        {
            var result = this.MongoCollection.Find(filter);
            return result.ToList();
        }
    }
}
