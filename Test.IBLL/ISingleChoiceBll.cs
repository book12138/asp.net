using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Driver;
using Test.Mongo.Model;

namespace Test.IBLL
{
    public interface ISingleChoiceBll: IMongoDBBaseBll<SingleChoice>
    {
        
    }
}
