using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
            IBLLSession bllSession = HttpContext.Current.Items["bllSession"] as IBLLSession;

            if(bllSession == null)
            {
                bllSession = new BLLSession();
                HttpContext.Current.Items.Add("dbSession", bllSession);
            }

            return bllSession;
        }
    }
}
