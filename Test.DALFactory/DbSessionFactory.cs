using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

using Test.IDAL;

namespace Test.DALFactory
{
    /// <summary>
    /// 创建数据会话层实例的工厂
    /// </summary>
    public class DbSessionFactory
    {
        public static IDbSession CreateDbSession()
        {
            IDbSession dbSession = CallContext.GetData("dbSession") as IDbSession;

            if(dbSession == null)
            {
                dbSession = new DbSession();
                CallContext.SetData("dbSession", dbSession);
            }

            return dbSession;
        }
    }
}
