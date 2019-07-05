using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

using Test.IBLL;

namespace Test.BLLFactory
{
    /// <summary>
    /// 创建业务会话层实例的工厂
    /// </summary>
    public class BllSessionFactory
    {
        public static IBLLSession  CreateBllSession()
        {
            IBLLSession bllSession = CallContext.GetData("bllSession") as IBLLSession;

            if(bllSession == null)
            {
                bllSession = new BLLSession();
                CallContext.SetData("dbSession", bllSession);
            }

            return bllSession;
        }
    }
}
