using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weikeren.Utility.Payment.Models
{
    public class PayRequestModel
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Money { get; set; }
        /// <summary>
        /// 银行代号
        /// </summary>
        public string BankCode { get; set; }
        /// <summary>
        /// 页面编码
        /// </summary>
        public string Charset { get; set; }
        /// <summary>
        /// 商家ID （一连串数字）
        /// </summary>
        public string PartnerId { get; set; }
        /// <summary>
        /// 商家Key（一连串字符）
        /// </summary>
        public string PartnerKey { get; set; }
        /// <summary>
        /// 商家名称,支付宝为seller_email，用支付宝时必填
        /// </summary>
        public string MerchantName { get; set; }
        /// <summary>
        /// 提交地址
        /// </summary>
        public string PostUrl { get; set; }
        /// <summary>
        /// 重定向形式返回的页面
        /// </summary>
        public string ReturnCallbackUrl { get; set; }
        /// <summary>
        /// 服务器点对点返回的页面
        /// </summary>
        public string NotifyCallbackUrl { get; set; }
        /// <summary>
        /// 产品名称（注意编码问题）
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 产品描述（注意编码问题）
        /// </summary>
        public string ProductDesc { get; set; }
        /// <summary>
        /// 标记，发送到银行后，银行会以原文发回来，（注意编码问题）
        /// </summary>
        public string Remark { get; set; }


    }
}
