using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

using Test.Service.BLL;

namespace Test.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(UserService)))
            {
                host.Open();
                Console.WriteLine("服务已启动，按任意键中止...");
                Console.ReadKey(true);
                host.Close();
            }
        }
    }
}
