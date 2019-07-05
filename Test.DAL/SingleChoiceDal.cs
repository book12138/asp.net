using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Configuration;

using MongoDB.Driver;
using MongoDB.Bson;

using Test.Mongo.Model;
using Test.IDAL;

namespace Test.DAL
{
    public class SingleChoiceDal : MongoBaseDal<SingleChoice> , ISingleChoiceDal
    {
        public override void LoadMongo() => base.MongoCollection = MongoCollection<SingleChoice>.GetCollection("EnglishItemPool", "SingleChoice");
    }
}
