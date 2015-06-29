using Weikeren.Utility.Payment.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weikeren.Utility.Payment.Models
{
    /// <summary>
    /// 返回数据模型
    /// </summary>
    public class PayReponseModel
    {
        /// <summary>
        /// 交易号
        /// </summary>
        public string OrderNo { get; set; }
        ///// <summary>
        ///// 交易号
        ///// </summary>
        //public string TradeOrderNo { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Money { get; set; }
        /// <summary>
        /// 银行代号
        /// </summary>
        public string BankCode { get; set; }
        /// <summary>
        /// 流水号
        /// </summary>
        public string PaySSN { get; set; }
        /// <summary>
        /// 收到的参数
        /// </summary>
        public string ReceivedArgs { get; set; }
        /// <summary>
        /// 标记，发送到银行后，银行会以原文发回来，（注意编码问题）
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public string PaidDate { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public PayWayEnum PayWay { get; set; }
        /// <summary>
        /// 返回数据源，ZhiFuResponseModel | YeepayReponseModel | AlipayReponseModel
        /// </summary>
        public object Source { get; set; }

        
    }
}
