using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.IBLL
{
    public interface IBLLSession
    {
        IUserInfoBll UserBll { get; }
    }
}
