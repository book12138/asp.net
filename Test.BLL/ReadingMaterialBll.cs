using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Test.DALFactory;
using Test.Mongo.Model;
using Test.IBLL;

namespace Test.BLL
{
    public class ReadingMaterialBll : MongoBaseService<ReadingMaterial>, IReadingMaterialBll
    {
        public override void SetCurrentDal() => CurrentDal = DbSession.ReadingMaterialDal;
    }
}
