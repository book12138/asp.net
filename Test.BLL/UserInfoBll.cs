using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Test.Mongo.Model;
using Test.IBLL;

namespace Test.BLL
{
    public class UserInfoBll : MongoDBBaseService<User>, IUserInfoBll
    {
        public override void SetCurrentDal() => base.CurrentDal = DbSession.UserDal;        
    }
}
