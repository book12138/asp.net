using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Test.Mongo.Model;

namespace Test.IBLL
{
    public interface IGlobalVariableBll : IMongoDBBaseService<GlobalVariable>
    {
        /// <summary>
        /// 修改单篇文档（不允许修改name，只允许修改value和describe）
        /// </summary>
        /// <param name="filter">条件</param>
        /// <param name="model">新值模型</param>
        /// <returns></returns>
        bool UpdateOne(Expression<Func<GlobalVariable, bool>> filter, GlobalVariable model);
    }
}
