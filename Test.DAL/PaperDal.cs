using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Test.Mongo.Model;
using Test.IDAL;
using MongoDB.Driver;
using System.Configuration;

namespace Test.DAL
{
    public class PaperDal : MongoBaseDal<Paper>, IPaperDal
    {
        public override void LoadMongo() => MongoCollection = MongoCollection<Paper>.GetCollection("EnglishItemPool","Paper");    
    }
}
