using com.yeepay.icc;
using com.yeepay.utils;
using Weikeren.Utility.Payment.Enum;
using Weikeren.Utility.Payment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weikeren.Utility.Payment.PayProcessor
{
    /// <summary>
    /// 易宝支付流程处理器
    /// </summary>
    public class YeepayProcessor : IPaymentProcessor<YeepayRequestModel, YeepayReponseModel>
    {
        /// <summary>
        /// 支付方式
        /// </summary>
        public PayWayEnum PayWay
        {
            get
            {
                return PayWayEnum.Yeepay;
            }
        }
        /// <summary>
        /// 以默认的形式提交到银行
        /// </summary>
        public void PostPayment(YeepayRequestModel paymentRequest)
        {
            if (paymentRequest == null)
                throw new ArgumentNullException("PayRequestModel Is Null");

            paymentRequest.p4_Cur = "CNY";
            paymentRequest.p9_SAF = "0";
            paymentRequest.pr_NeedResponse = "1";
            paymentRequest.hmac = Buy.CreateBuyHmac(paymentRequest.p2_Order, paymentRequest.p3_Amt, paymentRequest.p4_Cur, paymentRequest.p5_Pid, paymentRequest.p6_Pcat, paymentRequest.p7_Pdesc, paymentRequest.p8_Url,
                paymentRequest.p9_SAF, paymentRequest.pa_MP, paymentRequest.pd_FrpId, paymentRequest.pr_NeedResponse);

            paymentRequest.PostProcessPayment();

        }

        /// <summary>
        /// 银行CallBack返回的数据
        /// </summary>
        /// <returns></returns>
        public YeepayReponseModel GetCallbackResultModel(HttpRequestBase request)
        {
            BuyCallbackResult result = Buy.VerifyCallback(FormatQueryString.GetQueryString("p1_MerId"), FormatQueryString.GetQueryString("r0_Cmd"), FormatQueryString.GetQueryString("r1_Code"), FormatQueryString.GetQueryString("r2_TrxId"),
               FormatQueryString.GetQueryString("r3_Amt"), FormatQueryString.GetQueryString("r4_Cur"), FormatQueryString.GetQueryString("r5_Pid"), FormatQueryString.GetQueryString("r6_Order"), FormatQueryString.GetQueryString("r7_Uid"),
               FormatQueryString.GetQueryString("r8_MP"), FormatQueryString.GetQueryString("r9_BType"), FormatQueryString.GetQueryString("rp_PayDate"), FormatQueryString.GetQueryString("hmac"));

            YeepayReponseModel model = new YeepayReponseModel();
            model.p1_MerId = result.P1_MerId;
            model.r0_Cmd = result.R0_Cmd;
            model.r1_Code = result.R1_Code;
            model.r2_TrxId = result.R2_TrxId;
            model.r3_Amt = result.R3_Amt;
            model.r4_Cur = result.R4_Cur;
            model.r5_Pid = result.R5_Pid;
            model.r6_Order = result.R6_Order;
            model.r7_Uid = result.R7_Uid;
            model.r8_MP = result.R8_MP;
            model.r9_BType = result.R9_BType;
            model.ro_BankOrderId = FormatQueryString.GetQueryString("ro_BankOrderId");
            model.rp_PayDate = result.Rp_PayDate;
            model.rq_CardNo = FormatQueryString.GetQueryString("rq_CardNo");
            model.ru_Trxtime = FormatQueryString.GetQueryString("ru_Trxtime");
            model.rb_BankId = FormatQueryString.GetQueryString("rb_BankId");
            model.hmac = result.Hmac;
            model.ErrMsg = result.ErrMsg;
            return model;
        }

        /// <summary>
        /// 处理Callback的逻辑
        /// </summary>
        /// <param name="reponseModel">Callback返回结果</param>
        /// <param name="pageCallbackAction">页面返回</param>
        /// <param name="offlineCallbackAction">离线返回</param>
        public void DoCallback(YeepayReponseModel reponseModel, Action<YeepayReponseModel> pageCallbackAction, Action<YeepayReponseModel> offlineCallbackAction)
        {
            if (string.IsNullOrEmpty(reponseModel.ErrMsg))
            {
                if (reponseModel.r1_Code == "1")
                {
                    //PageCallback
                    if (reponseModel.r9_BType == "1")
                    {
                        if (pageCallbackAction != null)
                        {
                            pageCallbackAction.Invoke(reponseModel);
                        }
                    }
                    else if (reponseModel.r9_BType == "2") //OfflineCallback
                    {

                        if (offlineCallbackAction != null)
                        {
                            offlineCallbackAction.Invoke(reponseModel);
                        }
                    }
                }
                else
                {
                    System.Web.HttpContext.Current.Response.Write("支付失败!" +
                            "<br>接口类型:" + reponseModel.r0_Cmd +
                            "<br>返回码:" + reponseModel.r1_Code +
                        //"<br>商户号:" + result.P1_MerId +
                            "<br>交易流水号:" + reponseModel.r2_TrxId +
                            "<br>商户订单号:" + reponseModel.r6_Order +

                            "<br>交易金额:" + reponseModel.r3_Amt +
                            "<br>交易币种:" + reponseModel.r4_Cur +
                            "<br>订单完成时间:" + reponseModel.rp_PayDate +
                            "<br>回调方式:" + reponseModel.r9_BType +
                            "<br>错误信息:" + reponseModel.ErrMsg + "<BR>");
                }

            }
            else
            {
                System.Web.HttpContext.Current.Response.Write("交易签名无效!");
            }

        }

    }
}