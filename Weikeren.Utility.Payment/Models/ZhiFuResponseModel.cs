using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Weikeren.Utility.Payment.Models
{
    /// <summary>
    /// 直付返回结果
    /// </summary>
    public class ZhiFuResponseModel:BaseReponseModel
    {
        #region Property
        /// <summary>
        /// 支付结果,
        /// 支付结果状态:SUCCESS,FAILED
        /// (不可空)
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 返回码，
        /// 000000-成功。其他信息提示码，提示各类相关失败信息
        /// (不可空)
        /// </summary>
        public string message_code { get; set; }
        /// <summary>
        /// 返回码描述信息
        /// (可空)
        /// </summary>
        public string message_desc { get; set; }
        /// <summary>
        /// 签名方式，
        /// MD5或RSA
        /// （不可空）
        /// </summary>
        public string sign_type { get; set; }
        /// <summary>
        /// 业务类型，
        /// PageResult:支付结果页面通知(即时到账)，OfflineResult：支付结果后台通知(即时到账)
        /// （不可空）
        /// </summary>
        public string biz_type { get; set; }
        /// <summary>
        /// 接口版本，
        /// 1.0（不可空）
        /// </summary>
        public string version_no { get; set; }
        /// <summary>
        /// 合作商户编号，
        /// 直付给合作商户分配的唯一标识
        /// （不可空）
        /// </summary>
        public string partner_id { get; set; }
        /// <summary>
        /// 流水号,
        /// 直付系统返回的交易流水号
        /// （不可空）
        /// </summary>
        public string pay_no { get; set; }
        /// <summary>
        /// 支付金额，以元为单位
        /// </summary>
        public decimal Money
        {
            get
            {
                decimal d = 0.0m;
                decimal.TryParse(total_amount, out d);
                return d / 100.0m;
            }
        }
        /// <summary>
        /// 支付金额,
        /// 实际支付的金额，以分为单位
        /// （不可空）
        /// </summary>
        public string total_amount { get; set; }
        /// <summary>
        /// 支付银行,
        /// 用户使用哪个银行进行支付的。详细信息请见后面的银行代码对照表
        /// （可空）
        /// </summary>
        public string bank_abbr { get; set; }
        /// <summary>
        /// 支付手机号
        /// </summary>
        public string purchaser_id { get; set; }
        /// <summary>
        /// 商户订单号,
        /// 商户系统的订单号
        /// （不可空）
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 订单提交日期,
        /// 商户发起请求的日期：格式：YYYYMMDD
        /// （不可空）
        /// </summary>
        public string order_date { get; set; }
        /// <summary>
        /// 支付时间,用户完成支付的时间,格式：YYYYMMDDHHmmss
        /// （不可空）
        /// </summary>
        public string pay_date { get; set; }
        /// <summary>
        /// 会计日期,
        /// 直付系统会计日期，格式：YYYYMMDDHHmmss
        /// （不可空）
        /// </summary>
        public string ac_date { get; set; }
        /// <summary>
        /// 原样返回的商户数据,
        /// 交易返回时原样返回给商户网站，给商户备用
        /// （可空）
        /// </summary>
        public string attach_param { get; set; }
        /// <summary>
        /// 费用,单位为分
        /// （不可空）
        /// </summary>
        public string fee { get; set; }
        /// <summary>
        /// 服务器公钥证书,
        /// 不参与签名；如果signType=RSA，此项必输
        /// （可空）
        /// </summary>
        public string server_cert { get; set; }
        /// <summary>
        /// 签名数据，
        /// 以上请求参数生成的签名串,获得mac的方法见签名算法,参数顺序按照表格中从上到下的顺序,但不包括本参数.
        /// （不可空）
        /// </summary>
        public string mac { get; set; }

        /// <summary>
        /// 支付网关返回参数
        /// </summary>
        public string ReceivedArgs
        {
            get
            {
                return "status:" + status + ","
                    + "message_code:" + message_code + ","
                    + "message_desc:" + message_desc + ","
                    + "sign_type:" + sign_type + ","
                    + "biz_type:" + biz_type + ","
                    + "version_no:" + version_no + ","
                    + "partner_id:" + partner_id + ","
                    + "pay_no:" + pay_no + ","
                    + "total_amount:" + total_amount + ","
                    + "bank_abbr:" + bank_abbr + ","
                    + "purchaser_id:" + purchaser_id + ","
                    + "order_id:" + order_id + ","
                    + "order_date:" + order_date + ","
                    + "pay_date:" + pay_date + ","
                    + "ac_date:" + ac_date + ","
                    + "attach_param:" + attach_param + ","
                    + "fee:" + fee + ","
                    + "server_cert:" + server_cert + ","
                    + "mac:" + mac + ","
                    ;
            }
        } 
        #endregion



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override PayReponseModel ToPayReponseModel()
        {
            PayReponseModel model = new PayReponseModel();
            model.BankCode = bank_abbr;
            model.Money = Money;
            model.OrderNo = order_id;
            model.PaySSN = pay_no;
            model.ReceivedArgs = ReceivedArgs;
            model.Remark = attach_param;
            model.PaidDate = pay_date;
            model.Source = this;
            model.PayWay = Enum.PayWayEnum.Zhifu;
            return model;
        }
    }
}
