using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Test.Mongo.Model;
using Test.IBLL;

namespace Test.BLL
{
    public class UserBll : MongoBaseService<User>, IUserBll
    {
        public override void SetCurrentDal()
        {
            CurrentDal = DbSession.UserDal;
        }
    }
}
