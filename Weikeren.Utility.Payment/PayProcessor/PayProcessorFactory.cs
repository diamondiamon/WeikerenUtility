using Weikeren.Utility.Payment.Enum;
using Weikeren.Utility.Payment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Weikeren.Utility.Payment.PayProcessor
{
    /// <summary>
    /// 支付处理器工作 
    /// </summary>
    public class PayProcessorFactory
    {
        private PayProcessorFactory()
        {

        }

        /// <summary>
        /// 创建支付处理器
        /// PayWayEnum.Alipay_Buyer:<AlipayBuyerRequestModel, AlipayBuyerReponseModel>
        /// PayWayEnum.Alipay_Direct:<AlipayDirectRequestModel, AlipayDirectReponseModel>
        /// PayWayEnum.Yeepay:<YeepayRequestModel, YeepayReponseModel>
        /// </summary>
        /// <param name="payWay"></param>
        /// <returns></returns>
        public static IPaymentProcessor<TPayRequestModel, TPayReponseModel> CreatePayProcessor<TPayRequestModel, TPayReponseModel>(PayWayEnum payWay)
        {
            try
            {
                switch (payWay)
                {
                    case PayWayEnum.Alipay_Buyer:
                        return new AlipayBuyerProcessor() as IPaymentProcessor<TPayRequestModel, TPayReponseModel>;
                    case PayWayEnum.Alipay_Direct:
                        return new AlipayDirectProcessor() as IPaymentProcessor<TPayRequestModel, TPayReponseModel>;
                    case PayWayEnum.Yeepay:
                        return new YeepayProcessor() as IPaymentProcessor<TPayRequestModel, TPayReponseModel>;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        ///// <summary>
        ///// 创建支付处理器
        ///// </summary>
        ///// <param name="payWay"></param>
        ///// <returns></returns>
        //public static T CreatePayProcessor<T>(PayWayEnum payWay)
        //{
        //    try
        //    {
        //        switch (payWay)
        //        {
        //            case PayWayEnum.Alipay_Buyer:
        //                return new AlipayBuyerProcessor();
        //            case PayWayEnum.Alipay_Direct:
        //                return new AlipayDirectProcessor() as IPaymentProcessor<TPayRequestModel, TPayReponseModel>;
        //            case PayWayEnum.Yeepay:
        //                return new YeepayProcessor() as IPaymentProcessor<TPayRequestModel, TPayReponseModel>;
        //        }

        //        return null;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
    }
}