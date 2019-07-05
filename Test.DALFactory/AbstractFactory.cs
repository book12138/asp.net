using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Configuration;

using Test.IDAL;

namespace Test.DALFactory
{
    /// <summary>
    /// 抽象工厂（已反射的形式创建数据层实例）
    /// </summary>
    public class AbstractFactory
    {
        private static readonly string assemblyString = ConfigurationManager.AppSettings["assemblyString"];
        private static readonly string namespaceString = ConfigurationManager.AppSettings["namespaceString"];

        public static object CreateInstance(string className)
        {
            var assembly = Assembly.Load(assemblyString);
            return assembly.CreateInstance(className);
        }

        public static ISingleChoiceDal CreateSingleChoiceDal()
        {
            string fullClassName = namespaceString + ".SingleChoiceDal";
            return CreateInstance(fullClassName) as ISingleChoiceDal;
        }

        public static IReadingMaterialDal CreateReadingMaterialDal()
        {
            string fullClassName = namespaceString + ".ReadingMaterialDal";
            return CreateInstance(fullClassName) as IReadingMaterialDal;
        }

        public static IReadingSingleChoiceDal CreateReadingSingleChoiceDal()
        {
            string fullClassName = namespaceString + ".ReadingSingleChoiceDal";
            return CreateInstance(fullClassName) as IReadingSingleChoiceDal;
        }

        public static IUserInfoDal CreateUserDal()
        {
            string fullClassName = namespaceString + ".UserDal";
            return CreateInstance(fullClassName) as IUserInfoDal;
        }

        public static IPaperDal CreatePaperDal()
        {
            string fullClassName = namespaceString + ".PaperDal";
            return CreateInstance(fullClassName) as IPaperDal;
        }

        public static IGlobalVariableDal CreateGlobalVariableDal()
        {
            string fullClassName = namespaceString + ".GlobalVariableDal";
            return CreateInstance(fullClassName) as IGlobalVariableDal;
        }
    }
}
