using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weikeren.Utility.Payment.Enum
{
    /// <summary>
    /// 支付方式 
    /// </summary>
    public enum PayWayEnum
    {
        /// <summary>
        /// 支付宝(即时到账)
        /// </summary>
        Alipay_Direct = 0,
        /// <summary>
        /// 易宝支付
        /// </summary>
        Yeepay = 1,
        /// <summary>
        /// 直付
        /// </summary>
        Zhifu = 2,
        /// <summary>
        /// 支付宝(双接口)
        /// </summary>
        Alipay_Buyer = 3,
    }

    /// <summary>
    /// 
    /// </summary>
    public static partial class EnumExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="way"></param>
        /// <returns></returns>
        public static string ToClassName(this PayWayEnum way)
        {
            switch (way)
            {
                case PayWayEnum.Alipay_Direct:
                    return "Weikeren.Utility.Payment.PayProcessor.AlipayProcessor";
                case PayWayEnum.Yeepay:
                    return "Weikeren.Utility.Payment.PayProcessor.YeepayProcessor";
                case PayWayEnum.Zhifu:
                    return "Weikeren.Utility.Payment.PayProcessor.ZhiFuProcessor";
                default:
                    return null;
            }
        }
    }
}
