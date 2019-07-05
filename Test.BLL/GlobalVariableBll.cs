using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

using Test.DALFactory;
using Test.Mongo.Model;
using Test.IBLL;
using MongoDB.Driver;

namespace Test.BLL
{
    public class GlobalVariableBll : MongoDBBaseService<GlobalVariable>, IGlobalVariableBll
    {
        public override void SetCurrentDal() => base.CurrentDal = DbSession.GlobalVariableDal;

        /// <summary>
        /// 修改单篇文档（不允许修改name，只允许修改value和describe）
        /// </summary>
        /// <param name="filter">条件</param>
        /// <param name="model">新值模型</param>
        /// <returns></returns>
        public bool UpdateOne(Expression<Func<GlobalVariable, bool>> filter,GlobalVariable model)
        {
            var update = Builders<GlobalVariable>.Update.Set("Value", model.Value).Set("Describle", model.Describle);//不允许修改name，只允许修改value和describe
            return UpdateOne(filter, update);
        }
    }
}
