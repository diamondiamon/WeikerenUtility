using Weikeren.Utility.Payment.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weikeren.Utility.Payment.PayProcessor
{
    /// <summary>
    /// 支付处理器
    /// </summary>
    /// <typeparam name="TPayRequestModel">请求类</typeparam>
    /// <typeparam name="TPayReponseModel">回复类</typeparam>
    public interface IPaymentProcessor<TPayRequestModel, TPayReponseModel>
    {
        /// <summary>
        /// 支付方式
        /// </summary>
        PayWayEnum PayWay { get; }

        /// <summary>
        /// 以默认的形式提交到银行
        /// </summary>
        void PostPayment(TPayRequestModel request);

        /// <summary>
        /// 银行CallBack返回的数据
        /// </summary>
        /// <returns></returns>
        TPayReponseModel GetCallbackResultModel(HttpRequestBase request);
        /// <summary>
        /// 处理Callback的逻辑
        /// </summary>
        /// <param name="reponseModel">Callback返回结果</param>
        /// <param name="pageCallbackAction">页面返回</param>
        /// <param name="offlineCallbackAction">离线返回</param>
        void DoCallback(TPayReponseModel reponseModel, Action<TPayReponseModel> pageCallbackAction, Action<TPayReponseModel> offlineCallbackAction);
    }
}