using Weikeren.Utility.Payment.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weikeren.Utility.Payment.Models
{
    /// <summary>
    /// 支付宝返回数据模型
    /// </summary>
    public class AlipayDirectReponseModel
    {
        #region Property
        /// <summary>
        /// 成功标识
        /// <remarks>
        /// 表示接口调用是否成功，并不表明业务处理结果 T|F
        /// </remarks>
        /// </summary>
        public string is_success { get; set; }
        /// <summary>
        /// 签名方式
        /// <remarks>
        /// DSA、RSA、MD5三个值可选，必须大写。
        /// </remarks>
        /// </summary>
        public string sign_type { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 商户网站唯一订单号
        /// <remarks>
        /// 对应商户网站的订单系统中的唯一订单号，非支付宝交易号。需保证在商户网站中的唯一性。是请求时对应的参数，原样返回。
        /// </remarks>
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 商品名称
        /// <remarks>
        /// 商品的标题/交易标题/订单标题/订单关键字等。
        /// </remarks>
        /// </summary>
        public string subject { get; set; }
        /// <summary>
        /// 支付类型
        /// <remarks>
        /// 对应请求时的payment_type参数，原样返回。
        /// </remarks>
        /// </summary>
        public string payment_type { get; set; }
        /// <summary>
        /// 接口名称
        /// <remarks>
        /// 标志调用哪个接口返回的链接。
        /// </remarks>
        /// </summary>
        public string exterface { get; set; }
        /// <summary>
        /// 支付宝交易号
        /// <remarks>
        /// 该交易在支付宝系统中的交易流水号。最短16位，最长64位。
        /// </remarks>
        /// </summary>
        public string trade_no { get; set; }
        /// <summary>
        /// 交易状态
        /// <remarks>
        /// 交易目前所处的状态。成功状态的值只有两个：TRADE_FINISHED（普通即时到账的交易成功状态）TRADE_SUCCESS（开通了高级即时到账或机票分销产品后的支付成功状态）
        /// </remarks>
        /// </summary>
        public string trade_status { get; set; }
        /// <summary>
        /// 通知校验ID
        /// <remarks>
        /// 支付宝通知校验ID，商户可以用这个流水号询问支付宝该条通知的合法性
        /// </remarks>
        /// </summary>
        public string notify_id { get; set; }
        /// <summary>
        /// 通知类型
        /// <remarks>
        /// 返回通知类型。
        /// </remarks>
        /// </summary>
        public string notify_type { get; set; }
        /// <summary>
        /// 通知时间
        /// <remarks>
        /// 通知时间（支付宝时间）。格式为yyyy-MM-dd HH:mm:ss。
        /// </remarks>
        /// </summary>
        public string notify_time { get; set; }
        /// <summary>
        /// 卖家支付宝账号
        /// <remarks>
        /// 卖家支付宝账号，可以是Email或手机号码
        /// </remarks>
        /// </summary>
        public string seller_email { get; set; }
        /// <summary>
        /// 买家支付宝账号
        /// <remarks>
        /// 买家支付宝账号，可以是Email或手机号码。
        /// </remarks>
        /// </summary>
        public string buyer_email { get; set; }
        /// <summary>
        /// 卖家支付宝账户号
        /// <remarks>
        /// 卖家支付宝账号对应的支付宝唯一用户号
        /// </remarks>
        /// </summary>
        public string seller_id { get; set; }
        /// <summary>
        /// 买家支付宝账户号
        /// <remarks>
        /// 买家支付宝账号对应的支付宝唯一用户号
        /// </remarks>
        /// </summary>
        public string buyer_id { get; set; }
        /// <summary>
        /// 交易金额
        /// <remarks>
        /// 该笔订单的资金总额，单位为RMB-Yuan。取值范围为[0.01，100000000.00]，精确到小数点后两位。
        /// </remarks>
        /// </summary>
        public string total_fee { get; set; }
        /// <summary>
        /// 商品描述
        /// <remarks>
        /// 对一笔交易的具体描述信息。如果是多种商品，请将商品描述字符串累加传给body
        /// </remarks>
        /// </summary>
        public string body { get; set; }
        /// <summary>
        /// 网银流水
        /// <remarks>
        /// 网银流水号。只有开通了纯网关和伪网关的商户，才返回该参数
        /// </remarks>
        /// </summary>
        public string bank_seq_no { get; set; }
        /// <summary>
        /// 公用回传参数
        /// <remarks>
        /// 用于商户回传参数，该值不能包含“=”、“&”等特殊字符。如果用户请求时传递了该参数，则返回给商户时会回传该参数。
        /// </remarks>
        /// </summary>
        public string extra_common_param { get; set; }

        /// <summary>
        /// 支付网关返回参数
        /// </summary>
        public string ReceivedArgs
        {
            get
            {
                return "is_success:" + is_success + ","
                    + "sign_type:" + sign_type + ","
                    + "sign:" + sign + ","
                    + "out_trade_no:" + out_trade_no + ","
                    + "subject:" + subject + ","
                    + "payment_type:" + payment_type + ","
                    + "exterface:" + exterface + ","
                    + "trade_no:" + trade_no + ","
                    + "trade_status:" + trade_status + ","
                    + "notify_id:" + notify_id + ","
                    + "notify_type:" + notify_type + ","
                    + "notify_time:" + notify_time + ","
                    + "seller_email:" + seller_email + ","
                    + "buyer_email:" + buyer_email + ","
                    + "seller_id:" + seller_id + ","
                    + "buyer_id:" + buyer_id + ","
                    + "total_fee:" + total_fee + ","
                    + "body:" + body + ","
                    + "bank_seq_no:" + bank_seq_no + ","
                    + "extra_common_param:" + extra_common_param + ","
                    ;
            }
        }

        /// <summary>
        /// Callback方式
        /// </summary>
        public CallbackWayEnum CallbackWay { get; set; }

        #endregion

    }
}