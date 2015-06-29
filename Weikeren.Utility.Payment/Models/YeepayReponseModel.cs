using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weikeren.Utility.Payment.Models
{
    /// <summary>
    /// 易宝返回数据模型
    /// </summary>
    public class YeepayReponseModel
    {
        #region Property
        /// <summary>
        /// 商户编号
        /// </summary>
        public string p1_MerId { get; set; }
        /// <summary>
        /// 业务类型
        /// <remarks>固定值 ”Buy”.</remarks>
        /// </summary>
        public string r0_Cmd { get; set; }
        /// <summary>
        /// 支付结果
        /// <remarks>固定值 “1”, 代表支付成功.</remarks>
        /// </summary>
        public string r1_Code { get; set; }
        /// <summary>
        /// 易宝支付交易流水号
        /// <remarks>
        /// 易宝支付平台产生的交易流水号，每笔订单唯一
        /// </remarks>
        /// </summary>
        public string r2_TrxId { get; set; }
        /// <summary>
        /// 支付金额
        /// 单位:元，精确到分.此参数为空则无法直连(如直连会报错：抱歉，交易金额太小。),必须到易宝网关让消费者输入金额
        /// </summary>
        public string r3_Amt { get; set; }
        /// <summary>
        /// 交易币种
        /// 返回时是"RMB"
        /// </summary>
        public string r4_Cur { get; set; }
        /// <summary>
        /// 商品名称
        /// <remarks>
        /// 易宝支付返回商户设置的商品名称. 此参数如用到中文，请注意转码
        /// </remarks>
        /// </summary>
        public string r5_Pid { get; set; }
        /// <summary>
        /// 商户订单号
        /// <remarks>
        /// 易宝支付返回商户订单号.
        /// </remarks>
        /// </summary>
        public string r6_Order { get; set; }
        /// <summary>
        /// 易宝支付会员ID
        /// <remarks>
        /// 如果用户使用的易宝支付会员进行支付则返回该用户的易宝支付会员ID;反之为””.
        /// </remarks>
        /// </summary>
        public string r7_Uid { get; set; }
        /// <summary>
        /// 商户扩展信息
        /// <remarks>
        /// 此参数如用到中文，请注意转码.
        /// </remarks>
        /// </summary>
        public string r8_MP { get; set; }
        /// <summary>
        /// 交易结果返回类型
        /// <remarks>
        /// 为“1”: 浏览器重定向;为“2”: 服务器点对点通讯.
        /// </remarks>
        /// </summary>
        public string r9_BType { get; set; }
        /// <summary>
        /// 支付通道编码
        /// <remarks>
        /// 返回用户所使用的支付通道编码.该返回参数不参与到hmac校验，范例中没有收录，可根据您的需要自行添加.
        /// </remarks>
        /// </summary>
        public string rb_BankId { get; set; }
        /// <summary>
        /// 银行订单号
        /// <remarks>
        /// 该返回参数不参与到hmac校验，范例中没有收录，可根据您的需要自行添加
        /// </remarks>
        /// </summary>
        public string ro_BankOrderId { get; set; }
        /// <summary>
        /// 支付成功时间
        /// <remarks>
        /// 该返回参数不参与到hmac校验，范例中没有收录，可根据您的需要自行添加.
        /// </remarks>
        /// </summary>
        public string rp_PayDate { get; set; }
        /// <summary>
        /// 神州行充值卡序列号
        /// <remarks>
        /// 若用户使用神州行卡支付，返回用户所使用的神州行卡序列号. 该返回参数不参与到hmac校验，范例中没有收录，可根据您的需要自行添加
        /// </remarks>
        /// </summary>
        public string rq_CardNo { get; set; }
        /// <summary>
        /// 交易结果通知时间
        /// <remarks>
        /// 该返回参数不参与到hmac校验，范例中没有收录，可根据您的需要自行添加.
        /// </remarks>
        /// </summary>
        public string ru_Trxtime { get; set; }
        /// <summary>
        /// 签名数据
        /// </summary>
        public string hmac { get; set; }

        public string ErrMsg { get; set; }

        public string ReceivedArgs
        {
            get {
                return "P1_MerId:" + p1_MerId 
                    + "R0_Cmd:" + r0_Cmd 
                    + "R1_Code:" + r1_Code 
                    + "R2_TrxId:" + r2_TrxId 
                    + "R3_Amt:" + r3_Amt 
                    + "R4_Cur:" + r4_Cur 
                    + "R5_Pid:" + r5_Pid 
                    + "R6_Order:" + r6_Order 
                    + "R7_Uid:" + r7_Uid 
                    + "R8_MP:" + r8_MP 
                    + "R9_BType:" + r9_BType 
                    + "Rp_PayDate:" 
                    + rp_PayDate + "Hmac:" 
                    + hmac;
            }
        }

        #endregion

    }
}