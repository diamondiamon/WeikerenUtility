
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Weikeren.Utility.Payment.Enum;
using Weikeren.Utility.Payment.Models;

namespace Weikeren.PaymentTest.Utility
{
    public class PayRequestModelCreator
    {
        private PayWayEnum _payWay;

        public PayRequestModelCreator(PayWayEnum payWay)
        {
            _payWay = payWay;
        }

        public PayRequestModel CreatePayRequestModel(HttpRequestBase Request, string orderNo, decimal amount, string bankNo, string remark)
        {
            PayRequestModel requestModel = null;

            switch (_payWay)
            {
                case PayWayEnum.Alipay:
                    requestModel = createAlipayRequestModel(Request, orderNo, amount, bankNo, remark);
                    break;
                case PayWayEnum.Yeepay:
                    requestModel = createYeepayRequestModel(Request, orderNo, amount, bankNo, remark);
                    break;
                case PayWayEnum.Zhifu:
                    requestModel = createZhifuRequestModel(Request, orderNo, amount, bankNo, remark);
                    break;
            }

            return requestModel;
        }
        /// <summary>
        /// 易宝支付
        /// </summary>
        /// <param name="Request"></param>
        /// <param name="orderNo"></param>
        /// <param name="amount"></param>
        /// <param name="bankNo"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        private PayRequestModel createYeepayRequestModel(HttpRequestBase Request, string orderNo, decimal amount, string bankNo, string remark)
        {
            var requestModel = getBasePayRequestModel(Request, orderNo, amount, bankNo, remark);
            return requestModel;
        }
        /// <summary>
        /// 直付
        /// </summary>
        /// <param name="Request"></param>
        /// <param name="orderNo"></param>
        /// <param name="amount"></param>
        /// <param name="bankNo"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        private PayRequestModel createZhifuRequestModel(HttpRequestBase Request, string orderNo, decimal amount, string bankNo, string remark)
        {
            var requestModel = getBasePayRequestModel(Request, orderNo, amount, bankNo, remark);
            requestModel.Charset = "gb2312";
            requestModel.PostUrl = System.Configuration.ConfigurationManager.AppSettings["zhifu"];
            requestModel.PartnerId = System.Configuration.ConfigurationManager.AppSettings["zhifu_partnerid"];
            requestModel.PartnerKey = System.Configuration.ConfigurationManager.AppSettings["zhifu_signKey"];
            return requestModel;
        }
        /// <summary>
        /// 支付宝
        /// </summary>
        /// <param name="Request"></param>
        /// <param name="orderNo"></param>
        /// <param name="amount"></param>
        /// <param name="bankNo"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        private PayRequestModel createAlipayRequestModel(HttpRequestBase Request, string orderNo, decimal amount, string bankNo, string remark)
        {
            var requestModel = getBasePayRequestModel(Request, orderNo, amount, bankNo, remark);
            requestModel.Charset = "utf-8";
            requestModel.MerchantName = System.Configuration.ConfigurationManager.AppSettings["AlipaySellerEmail"];
            requestModel.PartnerId = System.Configuration.ConfigurationManager.AppSettings["AlipayPartner"];
            requestModel.PartnerKey = System.Configuration.ConfigurationManager.AppSettings["AlipayKey"];
            return requestModel;
        }

        private PayRequestModel getBasePayRequestModel(HttpRequestBase Request,string orderNo, decimal amount, string bankNo, string remark)
        {
            PayRequestModel model = new PayRequestModel();
            model.OrderNo = orderNo;
            model.Money = amount;
            model.BankCode = bankNo;
            model.Charset = "gb2312";
            model.ProductName = "微客人支付";
            model.ProductDesc = "微客人支付";
            model.MerchantName = "微客人";
            model.Remark = remark;
            model.NotifyCallbackUrl = String.Format("http://{0}/Account/PayNotify", Request.Url.Authority);
            model.ReturnCallbackUrl = String.Format("http://{0}/Account/PayNotify", Request.Url.Authority);
            return model;
        }



        #region 基类参考
        //private AlipayRequestModel getAlipayRequestModel(string orderNo, decimal amount, string bankNo)
        //{
        //    AlipayRequestModel paymentRequest = new AlipayRequestModel();
        //    //paymentRequest.seller_email = System.Configuration.ConfigurationManager.AppSettings["AlipaySellerEmail"];
        //    paymentRequest.partner = System.Configuration.ConfigurationManager.AppSettings["AlipayPartner"];
        //    paymentRequest.key = System.Configuration.ConfigurationManager.AppSettings["AlipayKey"];
        //    paymentRequest.out_trade_no = orderNo;
        //    paymentRequest.subject = "手机订购";
        //    paymentRequest.body = "第三批手机订购";
        //    paymentRequest.total_fee = amount.ToString();
        //    paymentRequest.defaultbank = paymentRequest.ConvertBankCode(bankNo.ToUpper());
        //    paymentRequest.notify_url = String.Format("http://{0}{1}", Request.Url.Authority, Url.Action("AlipayNotify"));
        //    paymentRequest.return_url = String.Format("http://{0}{1}", Request.Url.Authority, Url.Action("AlipayReturn"));

        //    return paymentRequest;
        //}

        //private YeepayRequestModel getYeepayRequestModel(string orderNo, decimal amount, string bankNo)
        //{
        //    YeepayRequestModel paymentRequest = new YeepayRequestModel();
        //    paymentRequest.input_charset = "gb2312";
        //    paymentRequest.p2_Order = orderNo;
        //    paymentRequest.p3_Amt = amount.ToString();
        //    paymentRequest.p4_Cur = "CNY";
        //    paymentRequest.p5_Pid = "责任额转置";
        //    paymentRequest.p9_SAF = "0";
        //    paymentRequest.pr_NeedResponse = "1";
        //    paymentRequest.pa_MP = "裸价网";
        //    paymentRequest.pd_FrpId = bankNo;
        //    paymentRequest.p8_Url = String.Format("http://{0}{1}", Request.Url.Authority, Url.Action("PayNotify"));
        //    paymentRequest.hmac = Buy.CreateBuyHmac(paymentRequest.p2_Order, paymentRequest.p3_Amt, paymentRequest.p4_Cur, paymentRequest.p5_Pid, paymentRequest.p6_Pcat, paymentRequest.p7_Pdesc, paymentRequest.p8_Url,
        //        paymentRequest.p9_SAF, paymentRequest.pa_MP, paymentRequest.pd_FrpId, paymentRequest.pr_NeedResponse);
        //    return paymentRequest;

        //}

        //private ZhiFuRequestModel getZhiFuRequestModel(string orderNo, decimal amount, string bankNo)
        //{
        //    ZhiFuRequestModel paymentRequest = new ZhiFuRequestModel();
        //    paymentRequest.input_charset = "gb2312";
        //    paymentRequest.attach_param = "责任额转置";
        //    paymentRequest.biz_type = System.Configuration.ConfigurationManager.AppSettings["zhifu_Payway"];//支付方式
        //    paymentRequest.order_date = DateTime.Now.ToString("yyyyMMdd");
        //    paymentRequest.bank_abbr = "ICBC";
        //    paymentRequest.card_type = "1";//借记卡
        //    paymentRequest.order_id = orderNo;
        //    paymentRequest.total_amount = (amount * 100.0m).ToString("f2");
        //    paymentRequest.product_name = "责任额转置";
        //    paymentRequest.partner_name = "裸价网";
        //    paymentRequest.valid_num = "1";
        //    paymentRequest.partner_id = System.Configuration.ConfigurationManager.AppSettings["zhifu_partnerid"];
        //    paymentRequest.post_Url = System.Configuration.ConfigurationManager.AppSettings["zhifu"];
        //    paymentRequest.offline_notify_url = "http://" + Request.Url.Authority + Url.Action("ZhifuPayCallback");
        //    paymentRequest.page_return_url = "http://" + Request.Url.Authority + Url.Action("ZhifuPayCallback");
        //    paymentRequest.request_id = orderNo;
        //    paymentRequest.signKey = System.Configuration.ConfigurationManager.AppSettings["zhifu_signKey"];
        //    //string signKey = System.Configuration.ConfigurationManager.AppSettings["zhifu_signKey"];
        //    //String reqHmac = ep.MD5EncodeBy32(zfm.signData + signKey);
        //    //zfm.mac = reqHmac;

        //    return paymentRequest;
        //}
        #endregion

    }
}