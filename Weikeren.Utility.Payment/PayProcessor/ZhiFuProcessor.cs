using Weikeren.Utility.Payment.Enum;
using Weikeren.Utility.Payment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weikeren.Utility.Payment.PayProcessor
{
    /// <summary>
    /// 直付支付流程处理器
    /// </summary>
    public class ZhiFuProcessor : IPaymentProcessor<PayRequestModel, PayReponseModel>
    {
        /// <summary>
        /// 支付方式
        /// </summary>
        public PayWayEnum PayWay
        {
            get
            {
                return PayWayEnum.Zhifu;
            }
        }

        /// <summary>
        /// 以默认的形式提交到银行
        /// </summary>
        public void PostPayment(PayRequestModel request)
        {
            if (request == null)
                throw new ArgumentNullException("PayRequestModel Is Null");

            ZhiFuRequestModel paymentRequest = new ZhiFuRequestModel();
            paymentRequest.input_charset = request.Charset;
            paymentRequest.attach_param = request.Remark;
            paymentRequest.order_date = DateTime.Now.ToString("yyyyMMdd");
            paymentRequest.bank_abbr = paymentRequest.ConvertBankCode(request.BankCode.ToUpper()); ;
            paymentRequest.card_type = "1";//借记卡
            paymentRequest.order_id = request.OrderNo;
            paymentRequest.total_amount = (request.Money * 100.0m).ToString("f2");
            paymentRequest.product_name = request.ProductName;
            paymentRequest.partner_name = request.MerchantName;
            paymentRequest.valid_num = "1";
            paymentRequest.partner_id = request.PartnerId;
            paymentRequest.post_Url = request.PostUrl;
            paymentRequest.offline_notify_url = request.NotifyCallbackUrl;
            paymentRequest.page_return_url = request.ReturnCallbackUrl;
            paymentRequest.request_id = request.OrderNo;
            paymentRequest.signKey = request.PartnerKey;

            paymentRequest.PostProcessPayment();
        }

        /// <summary>
        /// 银行CallBack返回的数据
        /// </summary>
        /// <returns></returns>
        public PayReponseModel GetCallbackResultModel(HttpRequestBase request)
        {
            var model = getCallbackResultModel(request);
            return model.ToPayReponseModel();
        }

        /// <summary>
        /// 银行CallBack返回的数据
        /// </summary>
        /// <returns></returns>
        private ZhiFuResponseModel getCallbackResultModel(HttpRequestBase request)
        {
            ZhiFuResponseModel model = new ZhiFuResponseModel();
            model.status = request.Form["status"];
            model.message_code = request.Form["message_code"];
            model.message_desc = request.Form["message_desc"];
            model.sign_type = request.Form["sign_type"];
            model.biz_type = request.Form["biz_type"];
            model.version_no = request.Form["version_no"];
            model.partner_id = request.Form["partner_id"];
            model.pay_no = request.Form["pay_no"];
            model.total_amount = request.Form["total_amount"];
            model.bank_abbr = request.Form["bank_abbr"];
            model.purchaser_id = request.Form["purchaser_id"];
            model.order_id = request.Form["order_id"];
            model.order_date = request.Form["order_date"];
            model.pay_date = request.Form["pay_date"];
            model.ac_date = request.Form["ac_date"];
            model.attach_param = HttpUtility.UrlDecode(request.Form["attach_param"]);
            model.fee = request.Form["fee"];
            model.server_cert = request.Form["server_cert"];
            model.mac = request.Form["mac"];

            return model;
        }

        /// <summary>
        /// 处理Callback的逻辑
        /// </summary>
        /// <param name="reponseModel">返回结果</param>
        /// <param name="pageCallbackAction">页面返回</param>
        /// <param name="offlineCallbackAction">离线返回</param>
        public void DoCallback(PayReponseModel reponseModel, Action<PayReponseModel> pageCallbackAction, Action<PayReponseModel> offlineCallbackAction)
        {
            var resultModel = reponseModel.Source as ZhiFuResponseModel;

            if (resultModel.status.ToUpper() == "SUCCESS")
            {
                //在接收到支付结果通知后，判断是否进行过业务逻辑处理，不要重复进行业务逻辑处理
                if (resultModel.message_code == "000000")
                {
                    //页面返回
                    if (resultModel.biz_type.ToLower() == "pageresult")
                    {
                        if (pageCallbackAction != null)
                        {
                            pageCallbackAction.Invoke(reponseModel);
                        }
                    }
                    else if (resultModel.biz_type.ToLower() == "offlineresult") //后台返回
                    {
                        // * 如果是服务器返回则需要回应一个特定字符串'SUCCESS',且在'SUCCESS'之前不可以有任何其他字符输出,保证首先输出的是'SUCCESS'字符串
                        if (offlineCallbackAction != null)
                        {
                            offlineCallbackAction.Invoke(reponseModel);
                        }
                    }
                }
                else
                {
                    System.Web.HttpContext.Current.Response.Write("支付失败!" +
                             "<br>接口类型:" + resultModel.biz_type +
                             "<br>返回码:" + resultModel.message_code +
                        //"<br>商户号:" + result.P1_MerId +
                             "<br>交易流水号:" + resultModel.pay_no +
                             "<br>商户订单号:" + resultModel.order_id +

                             "<br>交易金额:" + resultModel.Money +
                        //"<br>交易币种:" + result.R4_Cur +
                             "<br>订单完成时间:" + resultModel.pay_date +
                             "<br>回调方式:" + resultModel.biz_type +
                             "<br>错误信息:" + resultModel.message_desc + "<BR>");
                }
            }
            else
            {
                System.Web.HttpContext.Current.Response.Write("交易签名无效!");
            }
        }


    }
}