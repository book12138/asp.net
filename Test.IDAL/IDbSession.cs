using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.IDAL
{
    public interface IDbSession
    {
        ISingleChoiceDal SingleChoiceDal { get; }

        IReadingMaterialDal ReadingMaterialDal { get; }

        IReadingSingleChoiceDal ReadingSingleChoiceDal { get; }

        IUserInfoDal UserDal { get; }

        IPaperDal PaperDal { get; }

        IGlobalVariableDal GlobalVariableDal { get; }
    }
}
