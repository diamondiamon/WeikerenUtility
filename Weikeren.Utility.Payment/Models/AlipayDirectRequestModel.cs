using Weikeren.Utility.Payment.PayProcessor.AlipayHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Weikeren.Utility.Payment.Models
{
    /// <summary>
    /// 支付宝支付请求参数模型
    /// </summary>
    public class AlipayDirectRequestModel:BaseRequestModel
    {
        /// <summary>
        /// 接口名称[必填]
        /// <remarks>
        /// 默认：create_direct_pay_by_user
        /// </remarks>
        /// </summary>
        public string service { get; set; }
        /// <summary>
        /// 合作者身份ID[必填]
        /// <remarks>
        /// 签约的支付宝账号对应的支付宝唯一用户号。以2088开头的16位纯数字组成。
        /// </remarks>
        /// </summary>
        public string partner { get; set; }
        /// <summary>
        /// 合作者身份Key[必填]
        /// </summary>
        public string key { get; set; }
        /// <summary>
        /// 参数编码字符集[必填]
        /// <remarks>
        /// 商户网站使用的编码格式，如utf-8、gbk、gb2312等。
        /// </remarks>
        /// </summary>
        public string input_charset { get; set; }
        /// <summary>
        /// 签名方式[必填]
        /// <remarks>
        /// DSA、RSA、MD5三个值可选，必须大写。
        /// </remarks>
        /// </summary>
        public string sign_type { get; set; }
        /// <summary>
        /// 签名[必填]
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 服务器异步通知页面路径
        /// <remarks>
        /// 支付宝服务器主动通知商户网站里指定的页面http路径。
        /// </remarks>
        /// </summary>
        public string notify_url { get; set; }
        /// <summary>
        /// 页面跳转同步通知页面路径
        /// <remarks>
        /// 支付宝处理完请求后，当前页面自动跳转到商户网站里指定页面的http路径。
        /// </remarks>
        /// </summary>
        public string return_url { get; set; }
        /// <summary>
        /// 请求出错时的通知页面路径
        /// <remarks>
        /// 当商户通过该接口发起请求时，如果出现提示报错，支付宝会根据“11.10 item_orders_info出错时的通知错误码”和“11.11 请求出错时的通知错误码”通过异步的方式发送通知给商户。该功能需要联系支付宝开通。
        /// </remarks>
        /// </summary>
        public string error_notify_url { get; set; }

        #region 业务参数
        /// <summary>
        /// 商户网站唯一订单号[必填]
        /// <remarks>
        /// 支付宝合作商户网站唯一订单号（确保在商户系统中唯一）
        /// </remarks>
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 商品名称[必填]
        /// <remarks>
        /// 商品的标题/交易标题/订单标题/订单关键字等。该参数最长为128个汉字。
        /// </remarks>
        /// </summary>
        public string subject { get; set; }
        /// <summary>
        /// 支付类型[必填]
        /// <remarks>
        /// 默认值为：1（商品购买）
        /// </remarks>
        /// </summary>
        public string payment_type { get; set; }
        /// <summary>
        /// 默认网银[必填]
        /// </summary>
        public string defaultbank { get; set; }
        /// <summary>
        /// 卖家支付宝账号
        /// </summary>
        public string seller_email { get; set; }
        /// <summary>
        /// 卖家支付宝账户号
        /// </summary>
        public string seller_id { get; set; }
        /// <summary>
        /// 卖家别名支付宝账号
        /// </summary>
        public string seller_account_name { get; set; }
        /// <summary>
        /// 商品单价
        /// <remarks>
        /// 单位为：RMB Yuan。取值范围为[0.01，100000000.00]，精确到小数点后两位。
        /// </remarks>
        /// </summary>
        public string price { get; set; }
        /// <summary>
        /// 交易金额
        /// <remarks>
        /// 该笔订单的资金总额，单位为RMB-Yuan。取值范围为[0.01，100000000.00]，精确到小数点后两位。
        /// </remarks>
        /// </summary>
        public string total_fee { get; set; }
        /// <summary>
        /// 交易金额
        /// <remarks>
        /// 该笔订单的资金总额，单位为RMB-Yuan。取值范围为[0.01，100000000.00]，精确到小数点后两位。
        /// </remarks>
        /// </summary>
        public string quantity { get; set; }
        /// <summary>
        /// 商品描述
        /// <remarks>
        /// 对一笔交易的具体描述信息。如果是多种商品，请将商品描述字符串累加传给body
        /// </remarks>
        /// </summary>
        public string body { get; set; }
        /// <summary>
        /// 商品展示网址
        /// <remarks>
        /// 收银台页面上，商品展示的超链接。
        /// </remarks>
        /// </summary>
        public string show_url { get; set; }
        /// <summary>
        /// 默认支付方式
        /// <remarks>
        /// 若要使用纯网关，取值必须是bankPay（网银支付）。如果不设置，默认为directPay（余额支付）。
        /// </remarks>
        /// </summary>
        public string paymethod { get; set; }
        /// <summary>
        /// 网银支付时是否做CTU校验
        /// <remarks>
        /// 商户在配置了支持CTU（支付宝风险稽查系统）校验权限的前提下，可以选择本次交易是否需要经过CTU校验。Y：做CTU校验；N：不做CTU校验
        /// </remarks>
        /// </summary>
        public string need_ctu_check { get; set; }
        /// <summary>
        /// 提成类型
        /// <remarks>
        /// 目前只支持一种类型：10（卖家给第三方提成）。当传递了参数royalty_parameters时，提成类型参数不能为空
        /// </remarks>
        /// </summary>
        public string royalty_type { get; set; }
        /// <summary>
        /// 分润账号集
        /// </summary>
        public string royalty_parameters { get; set; }
        /// <summary>
        /// 防钓鱼时间戳
        /// <remarks>
        /// 通过时间戳查询接口获取的加密支付宝系统时间戳。如果已申请开通防钓鱼时间戳验证，则此字段必填
        /// </remarks>
        /// </summary>
        public string anti_phishing_key { get; set; }
        /// <summary>
        /// 客户端IP
        /// <remarks>
        /// 用户在创建交易时，该用户当前所使用机器的IP。如果商户申请后台开通防钓鱼选项，此字段必填，校验用
        /// </remarks>
        /// </summary>
        public string exter_invoke_ip { get; set; }
        /// <summary>
        /// 公用回传参数
        /// <remarks>
        /// 如果用户请求时传递了该参数，则返回给商户时会回传该参数
        /// </remarks>
        /// </summary>
        public string extra_common_param { get; set; }
        /// <summary>
        /// 公用业务扩展参数
        /// </summary>
        public string extend_param { get; set; }
        /// <summary>
        /// 超时时间,默认：1h
        /// <remarks>
        /// 设置未付款交易的超时时间，一旦超时，该笔交易就会自动被关闭。取值范围：1m～15d。m-分钟，h-小时，d-天，1c-当天（无论交易何时创建，都在0点关闭）。该参数数值不接受小数点，如1.5h，可转换为90m。该功能需要联系支付宝配置关闭时间
        /// </remarks>
        /// </summary>
        public string it_b_pay { get; set; }
        /// <summary>
        /// 商户申请的产品类型
        /// <remarks>
        /// 用于针对不同的产品，采取不同的计费策略。如果开通了航旅垂直搜索平台产品，请填写CHANNEL_FAST_PAY；如果没有，则为空。
        /// </remarks>
        /// </summary>
        public string product_type { get; set; }
        #endregion



        //public string TradeNo { get; set; }
        //public string Subject { get; set; }
        //public string Body { get; set; }
        //public decimal TotalFee { get; set; }
        //public string BankCode { get; set; }
        ///// <summary>
        ///// 后台通知URL
        ///// </summary>
        //public string Notify_url { get; set; }
        ///// <summary>
        ///// 前台通知URL
        ///// </summary>
        //public string Return_url { get; set; }


        public AlipayDirectRequestModel()
        {
            service = "create_direct_pay_by_user";
            input_charset = "utf-8";
            sign_type = "MD5";
            payment_type = "1";
            paymethod = "bankPay";
            show_url = "http://www.alipay.com/";
        }

        /// <summary>
        /// 提交请求
        /// </summary>
        public override void PostProcessPayment()
        {
            string[] para ={
                               "service="+service,
                               "partner=" + partner,
                               "seller_email=" + seller_email,
                               "out_trade_no=" + out_trade_no,
                               "subject=" + subject,
                               "body=" + body,
                               "total_fee=" + total_fee,
                               "show_url=" + show_url,
                               "payment_type=1",
                               "notify_url=" + notify_url,
                               "return_url=" + return_url,
                               "_input_charset=" + input_charset,
                               "extra_common_param=" + extra_common_param,
                               "paymethod=bankPay",
                               "defaultbank=" + defaultbank
                           };
            string aliay_url = CreatUrl(
                para,
                input_charset,
                key
                );
            var post = new RemotePost();
            post.FormName = "paysubmit";
            post.Url = "https://mapi.alipay.com/gateway.do?_input_charset=" + input_charset;
            post.Method = "POST";

            post.Add("service", service);
            post.Add("partner", partner);
            post.Add("seller_email", seller_email);
            post.Add("out_trade_no", out_trade_no);
            post.Add("subject", subject);
            post.Add("body", body);
            post.Add("total_fee", total_fee);
            post.Add("show_url", show_url);
            post.Add("return_url", return_url);
            post.Add("notify_url", notify_url);
            post.Add("payment_type", "1");
            post.Add("sign", aliay_url);
            post.Add("sign_type", sign_type);
            post.Add("extra_common_param", extra_common_param);
            post.Add("paymethod", "bankPay");
            post.Add("defaultbank", defaultbank);
            post.Post();
        }

        



        /// <summary>
        /// 
        /// </summary>
        /// <param name="bankCode"></param>
        /// <returns></returns>
        public string ConvertBankCode(string bankCode)
        {
            string Code = bankCode;
            switch (bankCode)
            {
                case "BOC-NET-B2C":
                    Code = "BOCB2C";
                    break;
                case "ICBC-NET-B2C":
                    Code = "ICBCB2C";
                    break;
                case "CMBCHINA-NET-B2C":
                    Code = "CMB";
                    break;
                case "CIB-NET-B2C":
                    Code = "CIB";
                    break;
                case "GDB-NET-B2C":
                    Code = "GDB";
                    break;
                case "BOCO-NET-B2C":
                    Code = "COMM-DEBIT";
                    break;
                case "ECITIC-NET-B2C":
                    Code = "CITIC";
                    break;
                case "CCB-NET-B2C":
                    Code = "CCB";
                    break;
                case "ABC-NET-B2C":
                    Code = "ABC";
                    break;
                case "SPDB-NET-B2C":
                    Code = "SPDB";
                    break;
                case "SDB-NET-B2C":
                    Code = "SPABANK";
                    break;
                case "CMBC-NET-B2C":
                    Code = "CMBC";
                    break;
                case "HZBANK-NET-B2C":
                    Code = "HZCBB2C";
                    break;
                case "CEB-NET-B2C":
                    Code = "CEB-DEBIT";
                    break;
                case "SHB-NET-B2C":
                    Code = "SHBANK";
                    break;
                case "NBCB-NET-B2C":
                    Code = "NBBANK";
                    break;
                case "PINGANBANK-NET":
                    Code = "SPABANK";
                    break;
                case "BJRCB-NET-B2C":
                    Code = "BJRCB";
                    break;
                case "POST-NET-B2C":
                    Code = "POSTGC";
                    break;
                case "1000000-NET":
                    Code = "abc1003";
                    break;
                default:
                    Code = "";
                    break;
            }
            return Code;
        }
    }
}
