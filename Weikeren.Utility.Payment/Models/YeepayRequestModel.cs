using com.yeepay.icc;
using Weikeren.Utility.Payment.PayProcessor.AlipayHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weikeren.Utility.Payment.Models
{
    /// <summary>
    /// 易宝支付请求参数模型
    /// </summary>
    public class YeepayRequestModel:BaseRequestModel
    {
        /// <summary>
        /// 业务类型[必填]
        /// 固定值“Buy”
        /// </summary>
        public string p0_Cmd { get; set; }
        /// <summary>
        /// 商户编号[必填]
        /// </summary>
        public string p1_MerId { get { return Buy.GetMerId(); } }
        /// <summary>
        /// 商户订单号
        /// 若不为””，提交的订单号必须在自身账户交易中唯一;为 ””时，易宝支付会自动生成随机的商户订单号.易宝支付系统中对于已付或者撤销的订单，商户端不能重复提交。
        /// </summary>
        public string p2_Order { get; set; }
        /// <summary>
        /// 支付金额
        /// 单位:元，精确到分.此参数为空则无法直连(如直连会报错：抱歉，交易金额太小。),必须到易宝网关让消费者输入金额
        /// </summary>
        public string p3_Amt { get; set; }
        /// <summary>
        /// 交易币种[必填]
        /// 固定值 ”CNY”.
        /// </summary>
        public string p4_Cur { get; set; }
        /// <summary>
        /// 商品名称
        /// <remarks>
        /// 用于支付时显示在易宝支付网关左侧的订单产品信息,此参数如用到中文，请注意转码
        /// </remarks>
        /// </summary>
        public string p5_Pid { get; set; }
        /// <summary>
        /// 商品种类
        /// <remarks>
        /// 商品种类.此参数如用到中文，请注意转码
        /// </remarks>
        /// </summary>
        public string p6_Pcat { get; set; }
        /// <summary>
        /// 商品描述
        /// <remarks>
        /// 商品描述.此参数如用到中文，请注意转码
        /// </remarks>
        /// </summary>
        public string p7_Pdesc { get; set; }
        /// <summary>
        /// 商户接收支付成功数据的地址
        /// <remarks>
        /// 支付成功后易宝支付会向该地址发送两次成功通知，该地址可以带参数，如:“ www.yeepay.com/callback.action?test=test”.注意：如不填p8_Url的参数值支付成功后您将得不到支付成功的通知。
        /// </remarks>
        /// </summary>
        public string p8_Url { get; set; }
        /// <summary>
        /// 送货地址
        /// <remarks>
        /// 为“1”: 需要用户将送货地址留在易宝支付系统;为“0”: 不需要，默认为 ”0”.
        /// </remarks>
        /// </summary>
        public string p9_SAF { get; set; }
        /// <summary>
        /// 商户扩展信息
        /// <remarks>
        /// 返回时原样返回，此参数如用到中文，请注意转码.
        /// </remarks>
        /// </summary>
        public string pa_MP { get; set; }
        /// <summary>
        /// 支付通道编码
        /// <remarks>
        /// 该字段可依照附录:支付通道编码列表 设置参数值.如果此值设置错误则会报"error.noAvaliableFrp"错误
        /// </remarks>
        /// </summary>
        public string pd_FrpId { get; set; }
        /// <summary>
        /// 应答机制
        /// <remarks>
        /// 固定值为“1”: 需要应答机制; 收到易宝支付服务器点对点支付成功通知，必须回写以”success”（无关大小写）开头的字符串，即使您收到成功通知时发现该订单已经处理过，也要正确回写”success”，否则易宝支付将认为您的系统没有收到通知，启动重发机制，直到收到”success”为止。
        /// </remarks>
        /// </summary>
        public string pr_NeedResponse { get; set; }

        //private string _hmac = "";

        /// <summary>
        /// 签名数据
        /// </summary>
        public string hmac
        {
            //get
            //{
            //    if (string.IsNullOrEmpty(_hmac))
            //    {
            //        _hmac = Buy.CreateBuyHmac(p2_Order, p3_Amt, p4_Cur, p5_Pid, p6_Pcat, p7_Pdesc, p8_Url,
            //           p9_SAF, pa_MP, pd_FrpId, pr_NeedResponse);
            //    }
            //    return _hmac;
            //}
            get;
            set;
        }
        /// <summary>
        /// 字符集
        /// </summary>
        public string input_charset { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public YeepayRequestModel()
        {
            p0_Cmd = "Buy";
            p4_Cur = "CNY";
            p9_SAF = "0";
            pr_NeedResponse = "1";
            input_charset = "utf-8";
        }

        /// <summary>
        /// 提交请求
        /// </summary>
        public override void PostProcessPayment()
        {
            string[] para ={
                               "p0_Cmd="+p0_Cmd,
                               "p1_MerId=" + p1_MerId,
                               "p2_Order=" + p2_Order,
                               "p3_Amt=" + p3_Amt,
                               "p4_Cur=" + p4_Cur,
                               "p5_Pid=" + p5_Pid,
                               "p6_Pcat=" + p6_Pcat,
                               "p7_Pdesc=" + p7_Pdesc,
                               "p8_Url=" + p8_Url,
                               "p9_SAF=" + p9_SAF,
                               "pa_MP=" + pa_MP,
                               "pr_NeedResponse=" + pr_NeedResponse,
                               "hmac=" + hmac,
                               "pd_FrpId=" + pd_FrpId
                           };

            string aliay_url = CreatUrl(
                para,
                input_charset,
                p1_MerId
                );
            var post = new RemotePost();
            post.FormName = "paysubmit";
            //post.Url = "https://mapi.alipay.com/gateway.do?_input_charset=" + input_charset;
            post.Url = Buy.GetBuyUrl();
            post.Method = "POST";
            post.AcceptCharset = input_charset;

            post.Add("p0_Cmd", p0_Cmd);
            post.Add("p1_MerId", p1_MerId);
            post.Add("p2_Order", p2_Order);
            post.Add("p3_Amt", p3_Amt);
            post.Add("p4_Cur", p4_Cur);
            post.Add("p5_Pid", p5_Pid);
            post.Add("p6_Pcat", p6_Pcat);
            post.Add("p7_Pdesc", p7_Pdesc);
            post.Add("p8_Url", p8_Url);
            post.Add("p9_SAF", p9_SAF);
            post.Add("pa_MP", pa_MP);
            post.Add("pr_NeedResponse", pr_NeedResponse);
            post.Add("hmac", hmac);
            post.Add("pd_FrpId", pd_FrpId);
            post.Post();
        }

 



    }
}