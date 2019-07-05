using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

using Test.Mongo.Model;
using Test.IDAL;
using MongoDB.Driver;

namespace Test.DAL
{
    public class UserInfoDal : MongoBaseDal<User>, IUserInfoDal
    {
        public override void LoadMongo() => base.MongoCollection = MongoCollection<User>.GetCollection("EnglishItemPool", "UserInfo");
    }
}
