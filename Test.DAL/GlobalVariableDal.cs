using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Test.IDAL;
using Test.Mongo.Model;

namespace Test.DAL
{
    public class GlobalVariableDal : MongoBaseDal<GlobalVariable>, IGlobalVariableDal
    {
        public override void LoadMongo() => base.MongoCollection = MongoCollection<GlobalVariable>.GetCollection("EnglishItemPool", "GlobalVariable");
    }
}
