using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Weikeren.Utility.Payment.Enum;
using Weikeren.Utility.Payment.Models;
using Weikeren.Utility.Payment.PayProcessor.AlipayHelper;

namespace Weikeren.Utility.Payment.PayProcessor
{
    /// <summary>
    /// 支付宝支付流程处理器
    /// </summary>
    public class AlipayBuyerProcessor : IPaymentProcessor<AlipayBuyerRequestModel, AlipayBuyerReponseModel>
    {
        /// <summary>
        /// 支付方式
        /// </summary>
        public Enum.PayWayEnum PayWay
        {
            get { return Enum.PayWayEnum.Alipay_Buyer; }
        }

        /// <summary>
        /// 以默认的形式提交到银行
        /// </summary>
        /// <param name="request"></param>
        public virtual void PostPayment(AlipayBuyerRequestModel request)
        {
            if (request == null)
                throw new ArgumentNullException("AlipayBuyerRequestModel Is Null");

            request.PostProcessPayment();
        }

        #region 银行CallBack返回的数据
        /// <summary>
        /// 银行CallBack返回的数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public AlipayBuyerReponseModel GetCallbackResultModel(System.Web.HttpRequestBase request)
        {
            bool post = request.HttpMethod.ToUpper() == "POST";
            AlipayBuyerReponseModel model = new AlipayBuyerReponseModel();
            if (post)
            {
                model = postCallbackResultModel(request);
                model.CallbackWay = CallbackWayEnum.Notify;
            }
            else
            {
                model.CallbackWay = CallbackWayEnum.Page;
                model = getCallbackResultModel(request);
            }

            return model;
        }

        /// <summary>
        /// 银行CallBack返回的数据
        /// </summary>
        /// <returns></returns>
        private AlipayBuyerReponseModel getCallbackResultModel(HttpRequestBase request)
        {
            AlipayBuyerReponseModel model = new AlipayBuyerReponseModel();
            model.bank_seq_no = request.QueryString("bank_seq_no");
            model.body = request.QueryString("body");
            model.buyer_email = request.QueryString("buyer_email");
            model.buyer_id = request.QueryString("buyer_id");
            model.exterface = request.QueryString("exterface");
            model.extra_common_param = request.QueryString("extra_common_param");
            model.is_success = request.QueryString("is_success");
            model.notify_id = request.QueryString("notify_id");
            model.notify_time = request.QueryString("notify_time");
            model.notify_type = request.QueryString("notify_type");
            model.out_trade_no = request.QueryString("out_trade_no");
            model.payment_type = request.QueryString("payment_type");
            model.seller_email = request.QueryString("seller_email");
            model.seller_id = request.QueryString("seller_id");
            model.sign = request.QueryString("sign");
            model.sign_type = request.QueryString("sign_type");
            model.subject = request.QueryString("subject");
            model.total_fee = request.QueryString("total_fee");
            model.trade_no = request.QueryString("trade_no");
            model.trade_status = request.QueryString("trade_status");

            return model;
        }

        /// <summary>
        /// 银行CallBack返回的数据
        /// </summary>
        /// <returns></returns>
        private AlipayBuyerReponseModel postCallbackResultModel(HttpRequestBase request)
        {
            AlipayBuyerReponseModel model = new AlipayBuyerReponseModel();
            model.bank_seq_no = request.Form["bank_seq_no"];
            model.body = request.Form["body"];
            model.buyer_email = request.Form["buyer_email"];
            model.buyer_id = request.Form["buyer_id"];
            model.exterface = request.Form["exterface"];
            model.extra_common_param = request.Form["extra_common_param"];
            model.is_success = request.Form["is_success"];
            model.notify_id = request.Form["notify_id"];
            model.notify_time = request.Form["notify_time"];
            model.notify_type = request.Form["notify_type"];
            model.out_trade_no = request.Form["out_trade_no"];
            model.payment_type = request.Form["payment_type"];
            model.seller_email = request.Form["seller_email"];
            model.seller_id = request.Form["seller_id"];
            model.sign = request.Form["sign"];
            model.sign_type = request.Form["sign_type"];
            model.subject = request.Form["subject"];
            model.total_fee = request.Form["total_fee"];
            model.trade_no = request.Form["trade_no"];
            model.trade_status = request.Form["trade_status"];

            return model;
        } 

        #endregion

        #region 处理Callback的逻辑
        /// <summary>
        /// 处理Callback的逻辑
        /// </summary>
        /// <param name="reponseModel"></param>
        /// <param name="pageCallbackAction"></param>
        /// <param name="offlineCallbackAction"></param>
        public void DoCallback(AlipayBuyerReponseModel reponseModel, Action<AlipayBuyerReponseModel> pageCallbackAction, Action<AlipayBuyerReponseModel> offlineCallbackAction)
        {
            bool post = reponseModel.CallbackWay == Enum.CallbackWayEnum.Notify;
            if (post)
            {
                doPost(reponseModel, offlineCallbackAction);
            }
            else
            {
                doGet(reponseModel, pageCallbackAction);
            }
        }

        private void doPost(AlipayBuyerReponseModel reponseModel, Action<AlipayBuyerReponseModel> offlineCallbackAction)
        {
            SortedDictionary<string, string> sPara = GetRequestPost();

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                AlipayNotify aliNotify = new AlipayNotify();
                bool verifyResult = aliNotify.Verify(sPara, reponseModel.notify_id, reponseModel.sign);

                if (verifyResult)//验证成功
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码


                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中服务器异步通知参数列表

                   
                    if (reponseModel.trade_status.ToUpper() == "WAIT_BUYER_PAY")
                    {//该判断表示买家已在支付宝交易管理中产生了交易记录，但没有付款

                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序
                        if (offlineCallbackAction != null)
                        {
                            offlineCallbackAction.Invoke(reponseModel);
                        }
                        System.Web.HttpContext.Current.Response.Write("success");  //请不要修改或删除
                    }
                    else if (reponseModel.trade_status.ToUpper() == "WAIT_SELLER_SEND_GOODS")
                    {//该判断示买家已在支付宝交易管理中产生了交易记录且付款成功，但卖家没有发货

                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序
                        if (offlineCallbackAction != null)
                        {
                            offlineCallbackAction.Invoke(reponseModel);
                        }
                        System.Web.HttpContext.Current.Response.Write("success");  //请不要修改或删除
                    }
                    else if (reponseModel.trade_status.ToUpper() == "WAIT_BUYER_CONFIRM_GOODS")
                    {//该判断表示卖家已经发了货，但买家还没有做确认收货的操作

                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序
                        if (offlineCallbackAction != null)
                        {
                            offlineCallbackAction.Invoke(reponseModel);
                        }
                        System.Web.HttpContext.Current.Response.Write("success");  //请不要修改或删除
                    }
                    else if (reponseModel.trade_status.ToUpper() == "TRADE_FINISHED")
                    {//该判断表示买家已经确认收货，这笔交易完成

                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序
                        if (offlineCallbackAction != null)
                        {
                            offlineCallbackAction.Invoke(reponseModel);
                        }
                        System.Web.HttpContext.Current.Response.Write("success");  //请不要修改或删除
                    }
                    else
                    {
                        //System.Web.HttpContext.Current.Response.Write("success");  //其他状态判断。
                    }

                    //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else//验证失败
                {
                    System.Web.HttpContext.Current.Response.Write("fail");
                }
            }
            else
            {
                System.Web.HttpContext.Current.Response.Write("无通知参数");
            }


        }

        private void doGet(AlipayBuyerReponseModel reponseModel, Action<AlipayBuyerReponseModel> pageCallbackAction)
        {
            SortedDictionary<string, string> sPara = GetRequestGet();

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                AlipayNotify aliNotify = new AlipayNotify();
                bool verifyResult = aliNotify.Verify(sPara, reponseModel.notify_id, reponseModel.sign);

                if (verifyResult)//验证成功
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码

                    if (reponseModel.trade_status.ToUpper() == "WAIT_SELLER_SEND_GOODS")
                    {
                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序
                        if (pageCallbackAction != null)
                        {
                            pageCallbackAction.Invoke(reponseModel);
                        }
                    }
                    else if (reponseModel.trade_status.ToUpper() == "TRADE_FINISHED")
                    {
                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序
                        if (pageCallbackAction != null)
                        {
                            pageCallbackAction.Invoke(reponseModel);
                        }
                    }
                    else
                    {
                        HttpContext.Current.Response.Write("trade_status=" + reponseModel.trade_status);
                    }

                    //打印页面
                    HttpContext.Current.Response.Write("验证成功<br />");

                    //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else//验证失败
                {
                    HttpContext.Current.Response.Write("验证失败");
                }
            }
            else
            {
                HttpContext.Current.Response.Write("无返回参数");
            }
        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        private SortedDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = System.Web.HttpContext.Current.Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], System.Web.HttpContext.Current.Request.Form[requestItem[i]]);
            }

            return sArray;
        }

        /// <summary>
        /// 获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        private SortedDictionary<string, string> GetRequestGet()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = System.Web.HttpContext.Current.Request.QueryString;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], System.Web.HttpContext.Current.Request.QueryString[requestItem[i]]);
            }

            return sArray;
        } 

        #endregion
    }
}
