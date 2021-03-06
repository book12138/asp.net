﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Driver;

namespace Test.Service.IBLL
{
    [ServiceContract]
    public interface IMongoDBBaseService<T> where T : class, new()
    {
        /// <summary>
        /// 更新单篇文档(就算匹配到多个可跟新的文档，但是它也只会更新匹配结果中的第一篇)
        /// </summary>
        /// <param name="filter">条件</param>
        /// <param name="update">新值</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateOne(Expression<Func<T, bool>> filter, UpdateDefinition<T> update);

        /// <summary>
        /// 更新多篇文档
        /// </summary>
        /// <param name="filter">条件</param>
        /// <param name="update">新值</param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateMany(Expression<Func<T, bool>> filter, UpdateDefinition<T> update);

        /// <summary>
        /// 插入单篇文档
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        void InsertOne(T model);

        /// <summary>
        /// 插入多篇文档
        /// </summary>
        /// <param name="models"></param>
        [OperationContract]
        void InsertMany(IEnumerable<T> models);

        /// <summary>
        /// 删除多篇文档
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteMany(Expression<Func<T, bool>> filter);

        /// <summary>
        /// 删除一篇文档（即使有多篇文档符合匹配条件，但是也仅仅只会删除第一篇）
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteOne(Expression<Func<T, bool>> filter);

        /// <summary>
        /// 查找文档
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [OperationContract]
        List<T> Find(Expression<Func<T, bool>> filter);
    }
}
