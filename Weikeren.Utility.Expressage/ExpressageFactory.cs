using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weikeren.Utility.Expressage
{
    /// <summary>
    /// 快递工厂类
    /// </summary>
    public sealed class ExpressageFactory
    {
        /// <summary>
        /// 创建快递查询接口
        /// </summary>
        /// <returns></returns>
        public static IExpressageGetter CreateExpressageGetter()
        {
            string expressageType = System.Configuration.ConfigurationManager.AppSettings["ExpressageType"];
            if (string.IsNullOrEmpty(expressageType))
                throw new Exception("AppSetting中没设置ExpressageType的值");

            expressageType = expressageType.ToLower();

            switch (expressageType)
            {
                case "kuaidi100":
                    return new Kuaidi100ExpressageGetter();
                default:
                    throw new Exception("AppSetting中的ExpressageType值没法与实现类匹配");
            }

        }
    }
}
