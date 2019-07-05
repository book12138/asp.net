using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Test.IDAL;

namespace Test.DALFactory
{
    public class DbSession : IDbSession
    {
        public ISingleChoiceDal SingleChoiceDal => AbstractFactory.CreateSingleChoiceDal();

        public IReadingMaterialDal ReadingMaterialDal => AbstractFactory.CreateReadingMaterialDal();

        public IReadingSingleChoiceDal ReadingSingleChoiceDal => AbstractFactory.CreateReadingSingleChoiceDal();

        public IUserInfoDal UserDal => AbstractFactory.CreateUserDal();

        public IPaperDal PaperDal => AbstractFactory.CreatePaperDal();

        public IGlobalVariableDal GlobalVariableDal => AbstractFactory.CreateGlobalVariableDal();
    }
}
