using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Test.Mongo.Model;
using Test.Service.IBLL;
using Test.BLL;

namespace Test.Service.BLL
{
    public class UserInfoService : MongoDBBaseService<User>, IUserInfoService
    {
        public override void SetCurrentBll() => base.CurrentBll = new UserInfoBll();        
    }
}
