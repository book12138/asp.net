using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Driver;
using Test.IDAL;
using Test.DALFactory;

namespace Test.BLL
{
    public abstract class MongoDBBaseService<T> where T: class , new()
    {
        /// <summary>
        /// 数据层对象
        /// </summary>
        public IMongoDBBaseDal<T> CurrentDal { get; set; }

        /// <summary>
        /// 数据会话层实例
        /// </summary>
        public IDbSession DbSession => DbSessionFactory.CreateDbSession();

        /// <summary>
        /// 此方法，子类中必须实现，通过子类的具体实现，从而实现对属性currentDal的赋值
        /// </summary>
        public abstract void SetCurrentDal();

        /// <summary>
        /// 构造方法（在子类进行编译时，会执行此其父类构造方法，即执行子类重写的SetCurrentDal方法，完成对CurrentDal的赋值）
        /// </summary>
        public MongoDBBaseService() => SetCurrentDal();

        /// <summary>
        /// 修改单个文档，而且仅能修改某一个字段的值(即使有多篇文档符合匹配条件，但是也仅仅只会修改第一篇)
        /// </summary>
        /// <param name="filter">条件</param>
        /// <param name="update">新值</param>
        /// <returns></returns>
        public bool UpdateOne(Expression<Func<T, bool>> filter, UpdateDefinition<T> update) => this.CurrentDal.UpdateOne(filter, update);

        /// <summary>
        /// 修改多篇文档
        /// </summary>
        /// <param name="filter">条件</param>
        /// <param name="update">新值</param>
        /// <returns></returns>
        public bool UpdateMany(Expression<Func<T, bool>> filter, UpdateDefinition<T> update) => this.CurrentDal.UpdateMany(filter, update);

        /// <summary>
        /// 插入单篇文档
        /// </summary>
        /// <param name="model"></param>
        public void InsertOne(T model) => this.CurrentDal.InsertOne(model);

        /// <summary>
        /// 插入多篇文档
        /// </summary>
        /// <param name="models"></param>
        public void InsertMany(IEnumerable<T> models) => this.CurrentDal.InsertMany(models);

        /// <summary>
        /// 删除一篇文档（即使有多篇文档符合匹配条件，但是也仅仅只会删除第一篇）
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public bool DeleteOne(Expression<Func<T, bool>> filter) => this.CurrentDal.DeleteOne(filter);

        /// <summary>
        /// 删除多篇文档
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public bool DeleteMany(Expression<Func<T, bool>> filter) => this.CurrentDal.DeleteMany(filter);

        /// <summary>
        /// 查找文档
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<T> Find(Expression<Func<T, bool>> filter) => this.CurrentDal.Find(filter);
    }
}
