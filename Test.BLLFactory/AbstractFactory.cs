using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Configuration;

using Test.IBLL;

namespace Test.BLLFactory
{
    /// <summary>
    /// 抽象工厂（以反射的形式创建业务层实例）
    /// </summary>
    public class AbstractFactory
    {
        private static readonly string assemblyString = ConfigurationManager.AppSettings["bllAssemblyString"];
        private static readonly string namespaceString = ConfigurationManager.AppSettings["bllNamespaceString"];

        public static object CreateInstance(string className)
        {
            var assembly = Assembly.Load(assemblyString);
            return assembly.CreateInstance(className);
        }

        public static IUserInfoBll CreateUserBll()
        {
            string fullClassName = namespaceString + ".SingleChoiceDal";
            return CreateInstance(fullClassName) as IUserInfoBll;
        }       
    }
}
