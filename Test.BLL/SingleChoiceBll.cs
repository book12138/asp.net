using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Test.IBLL;
using Test.Mongo.Model;

namespace Test.BLL
{
    public class SingleChoiceBll : MongoDBBaseService<SingleChoice>, ISingleChoiceBll
    {
        public override void SetCurrentDal() => base.CurrentDal = DbSession.SingleChoiceDal;
    }
}
