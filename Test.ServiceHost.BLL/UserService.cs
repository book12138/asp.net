using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Test.Mongo.Model;
using Test.Service.IBLL;

namespace Test.Service.BLL
{
    public class UserService : IUserService
    {
        public string Find(Expression<Func<User, bool>> filter)
        {
            return "";
        }
    }
}
