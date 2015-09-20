using Weikeren.Utility.Payment.PayProcessor.AlipayHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Weikeren.Utility.Payment.Models
{
    /// <summary>
    /// 直付支付请求参数模型
    /// </summary>
    public class ZhiFuRequestModel:BaseRequestModel
    {
        /// <summary>
        /// 字符集,
        /// 只能取以下枚举值
        /// 00-GBK
        /// 01-GB2312
        /// 02-UTF-8
        /// 默认00-GBK
        /// (可空)
        /// </summary>
        public string char_set { get; set; }
        /// <summary>
        /// 页面返回url，
        /// 交易结果通过页面跳转通知到这个url
        /// (不可空)
        /// </summary>
        public string page_return_url { get; set; }
        /// <summary>
        /// 后台通知url，
        /// 交易结果通过后台通讯通知到这个url
        /// (不可空)
        /// </summary>
        public string offline_notify_url { get; set; }
        /// <summary>
        /// 业务类型，
        /// DirectPayment:直付，GWDirectPay：银行支付
        /// （不可空）
        /// </summary>
        public string biz_type { get; set; }
        /// <summary>
        /// 接口版本，
        /// 1.0（不可空）
        /// </summary>
        public string version_no { get; set; }
        /// <summary>
        /// 签名方式，
        /// MD5或RSA
        /// （不可空）
        /// </summary>
        public string sign_type { get; set; }
        /// <summary>
        /// 客户IP，
        /// 用户浏览器IP地址，用于防钓鱼控制
        /// （不可空）
        /// </summary>
        public string client_ip { get; set; }
        /// <summary>
        /// 订单日期，
        /// 商户发起请求的日期;格式：YYYYMMDD
        /// （不可空）
        /// </summary>
        public string order_date { get; set; }
        /// <summary>
        /// 银行代码
        /// 用户选择的银行所对应的英文代号，详细信息请见后面的银行代码对照表
        /// （可空）
        /// </summary>
        public string bank_abbr { get; set; }
        /// <summary>
        /// 银行卡种类，
        /// 1-借记卡; 2-信用卡
        /// （可空）
        /// </summary>
        public string card_type { get; set; }
        /// <summary>
        /// 合作商户编号，
        /// 直付给合作商户分配的唯一标识
        /// （不可空）
        /// </summary>
        public string partner_id { get; set; }
        /// <summary>
        /// 合作商户展示名称，
        /// 商户展示名称
        /// （可空）
        /// </summary>
        public string partner_name { get; set; }
        /// <summary>
        /// 合作商户会计日期，
        /// 商户发起请求的会计日期;格式：YYYYMMDD
        /// （不可空）
        /// </summary>
        public string partner_ac_date { get; set; }
        /// <summary>
        /// 请求号，
        /// 合作商户请求的流水号，每次请求保持唯一
        /// （不可空）
        /// </summary>
        public string request_id { get; set; }
        /// <summary>
        /// 订单号，
        /// 商户的订单号，商户系统保证唯一
        ///  （不可空）
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 金额，
        /// </summary>
        public double Money
        {
            set
            {
                var d = value * 100.0;
                total_amount = d.ToString("f0");
            }
        }
        /// <summary>
        /// 订单总金额，
        /// 订单金额，以分为单位，如1元表示为100
        /// （不可空）
        /// </summary>
        public string total_amount { get; set; }
        /// <summary>
        /// 退款金额,退款金额，以分为单位
        /// （不可空）
        /// </summary>
        public string refund_amount { get; set; }
        /// <summary>
        /// 商品展示url，
        /// （可空）
        /// </summary>
        public string show_url { get; set; }
        /// <summary>
        /// 购买者标识，
        /// 待支付的手机号或者直付账户昵称
        /// （可空）
        /// </summary>
        public string purchaser_id { get; set; }
        /// <summary>
        /// 商品名称，
        /// 所购买商品的名称
        /// （不可空）
        /// </summary>
        public string product_name { get; set; }
        /// <summary>
        /// 商品描述，
        /// （可空）
        /// </summary>
        public string product_desc { get; set; }
        /// <summary>
        /// 原样返回的商户数据，
        /// 交易返回时原样返回给商户网站，给商户备用
        /// （可空）
        /// </summary>
        public string attach_param { get; set; }
        /// <summary>
        /// 订单有效期单位，
        /// 只能取以下枚举值 00-分 01-小时 02-日 03-月
        /// （不可空）
        /// </summary>
        public string valid_unit { get; set; }
        /// <summary>
        /// 订单有效期数量，
        /// （不可空）
        /// </summary>
        public string valid_num { get; set; }
        /// <summary>
        /// 商户证书公钥，
        /// 不参与签名；如果signType=RSA，此项必输
        /// （可空）
        /// </summary>
        public string merchant_cert { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string signKey { get; set; }

        /// <summary>
        /// 签名值，
        /// （不可空）
        /// </summary>
        public string mac
        {
            get {
                string reqHmac = MD5EncodeBy32(signData + signKey);
                return reqHmac;
            }
        
        }
        /// <summary>
        /// 参数编码字符集[必填]
        /// <remarks>
        /// 商户网站使用的编码格式，如utf-8、gbk、gb2312等。
        /// </remarks>
        /// </summary>
        public string input_charset { get; set; }
        /// <summary>
        /// 提交地址
        /// </summary>
        public string post_Url { get; set; }

        private string _signData;
        public string signData
        {
            get
            {
                if (string.IsNullOrEmpty(_signData))
                {
                    _signData = char_set + page_return_url + offline_notify_url + biz_type + version_no
                            + sign_type + client_ip + order_date + bank_abbr + card_type + partner_id + partner_name
                            + partner_ac_date + request_id + order_id + total_amount + show_url
                            + purchaser_id + product_name + product_desc + attach_param
                            + valid_unit + valid_num;
                }
                return _signData;
            }
            set
            {
                _signData = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ZhiFuRequestModel()
        {
            char_set = "01";
            biz_type = "GWDirectPay";
            //biz_type = "DirectPayment";
            partner_ac_date = DateTime.Now.ToString("yyyyMMdd");
            sign_type = "MD5";
            version_no = "1.0";
            valid_unit = "02";
            client_ip = new System.Net.IPAddress(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].Address).ToString();
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
                case "ICBC-NET-B2C":
                    Code = "ICBC";
                    break;
                //case "BOC-NET-B2C":
                //    Code = "BOCB2C";
                //    break;
                //case "CMBCHINA-NET-B2C":
                //    Code = "CMB";
                //    break;
                //case "CIB-NET-B2C":
                //    Code = "CIB";
                //    break;
                //case "GDB-NET-B2C":
                //    Code = "GDB";
                //    break;
                //case "BOCO-NET-B2C":
                //    Code = "COMM-DEBIT";
                //    break;
                //case "ECITIC-NET-B2C":
                //    Code = "CITIC";
                //    break;
                //case "CCB-NET-B2C":
                //    Code = "CCB";
                //    break;
                //case "ABC-NET-B2C":
                //    Code = "ABC";
                //    break;
                //case "SPDB-NET-B2C":
                //    Code = "SPDB";
                //    break;
                //case "SDB-NET-B2C":
                //    Code = "SPABANK";
                //    break;
                //case "CMBC-NET-B2C":
                //    Code = "CMBC";
                //    break;
                //case "HZBANK-NET-B2C":
                //    Code = "HZCBB2C";
                //    break;
                //case "CEB-NET-B2C":
                //    Code = "CEB-DEBIT";
                //    break;
                //case "SHB-NET-B2C":
                //    Code = "SHBANK";
                //    break;
                //case "NBCB-NET-B2C":
                //    Code = "NBBANK";
                //    break;
                //case "PINGANBANK-NET":
                //    Code = "SPABANK";
                //    break;
                //case "BJRCB-NET-B2C":
                //    Code = "BJRCB";
                //    break;
                //case "POST-NET-B2C":
                //    Code = "POSTGC";
                //    break;
                //case "1000000-NET":
                //    Code = "abc1003";
                //    break;
                default:
                    Code = "";
                    break;
            }
            return Code;
        }

        private string MD5EncodeBy32(string data)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(UTF8Encoding.Default.GetBytes(data));

            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("X").PadLeft(2, '0'));
            }

            return sb.ToString();
        }


        /// <summary>
        /// 提交请求
        /// </summary>
        public override void PostProcessPayment()
        {
            string[] para ={
                               "char_set="+char_set,
                               "sign_type="+sign_type,
                               "client_ip="+client_ip,
                               "partner_ac_date="+partner_ac_date,
                               "version_no="+version_no,
                               "attach_param="+attach_param,
                               "biz_type=" + biz_type,
                               "order_date=" + order_date,
                               "bank_abbr=" + bank_abbr,
                               "card_type=" + card_type,
                               "order_id=" + order_id,
                               "total_amount=" + total_amount,
                               "product_name=" + product_name,
                               "product_desc=" + product_desc,
                               "partner_name=" + partner_name,
                               "valid_num=" + valid_num,
                               "valid_unit=" + valid_unit,
                               "partner_id=" + partner_id,
                               "page_return_url=" + page_return_url,
                               "offline_notify_url=" + offline_notify_url,
                               "request_id=" + request_id,
                               "show_url=" + show_url,
                               "purchaser_id=" + purchaser_id,
                               "merchant_cert=" + merchant_cert,
                               "mac=" + mac
                           };

            string aliay_url = CreatUrl(
                para,
                input_charset,
                partner_id
                );
            var post = new RemotePost();
            post.FormName = "paysubmit";
            //post.Url = "https://mapi.alipay.com/gateway.do?_input_charset=" + input_charset;
            post.Url = post_Url;
            post.Method = "POST";
            post.AcceptCharset = input_charset;
            post.Add("char_set", char_set);
            post.Add("sign_type", sign_type);
            post.Add("client_ip", client_ip);
            post.Add("partner_ac_date", partner_ac_date);
            post.Add("version_no", version_no);
            post.Add("attach_param", attach_param);
            post.Add("biz_type", biz_type);
            post.Add("order_date", order_date);
            post.Add("bank_abbr", bank_abbr);
            post.Add("card_type", card_type);
            post.Add("order_id", order_id);
            post.Add("total_amount", total_amount);
            post.Add("product_name", product_name);
            post.Add("product_desc", product_desc);
            post.Add("partner_name", partner_name);
            post.Add("valid_num", valid_num);
            post.Add("valid_unit", valid_unit);
            post.Add("partner_id", partner_id);
            post.Add("page_return_url", page_return_url);
            post.Add("offline_notify_url", offline_notify_url);
            post.Add("request_id", request_id);
            post.Add("show_url", show_url);
            post.Add("purchaser_id", purchaser_id);
            post.Add("merchant_cert", merchant_cert);
            post.Add("mac", mac);
            post.Post();
        }



    }
}
