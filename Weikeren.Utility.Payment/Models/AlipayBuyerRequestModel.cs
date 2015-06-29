using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Weikeren.Utility.Payment.PayProcessor.AlipayHelper;

namespace Weikeren.Utility.Payment.Models
{
    /// <summary>
    /// 支付宝，标准双接口请求类
    /// </summary>
    public class AlipayBuyerRequestModel : BaseRequestModel
    {
        /// <summary>
        /// 支付类型
        /// </summary>
        public string payment_type { get; set; }
        /// <summary>
        /// 服务器异步通知页面路径
        /// 需http://格式的完整路径，不能加?id=123这类自定义参数
        /// </summary>
        public string notify_url { get; set; }
        /// <summary>
        /// 页面跳转同步通知页面路径
        /// 需http://格式的完整路径，不能加?id=123这类自定义参数，不能写成http://localhost/
        /// </summary>
        public string return_url { get; set; }
        /// <summary>
        /// 卖家支付宝帐户
        /// </summary>
        public string seller_email { get; set; }
        /// <summary>
        /// 商户订单号
        /// 商户网站订单系统中唯一订单号，必填
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 订单名称
        /// 必填
        /// </summary>
        public string subject { get; set; }
        /// <summary>
        /// 付款金额
        /// 必填
        /// </summary>
        public string price { get; set; }
        /// <summary>
        /// 商品数量
        /// 必填，建议默认为1，不改变值，把一次交易看成是一次下订单而非购买一件商品
        /// </summary>
        public string quantity { get; set; }
        /// <summary>
        /// 物流费用
        /// </summary>
        public string logistics_fee { get; set; }
        /// <summary>
        /// 物流类型
        /// 必填，三个值可选：EXPRESS（快递）、POST（平邮）、EMS（EMS）
        /// </summary>
        public string logistics_type { get; set; }
        /// <summary>
        /// 物流支付方式
        /// 必填，两个值可选：SELLER_PAY（卖家承担运费）、BUYER_PAY（买家承担运费）
        /// </summary>
        public string logistics_payment { get; set; }
        /// <summary>
        /// 订单描述
        /// </summary>
        public string body { get; set; }
        /// <summary>
        /// 商品展示地址
        /// 需以http://开头的完整路径
        /// </summary>
        public string show_url { get; set; }
        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string receive_name { get; set; }
        /// <summary>
        /// 收货人地址
        /// XX省XXX市XXX区XXX路XXX小区XXX栋XXX单元XXX号
        /// </summary>
        public string receive_address { get; set; }
        /// <summary>
        /// 收货人邮编
        /// </summary>
        public string receive_zip { get; set; }
        /// <summary>
        /// 收货人电话号码
        /// </summary>
        public string receive_phone { get; set; }
        /// <summary>
        /// 收货人手机号码
        /// </summary>
        public string receive_mobile { get; set; }
        /// <summary>
        /// 接口名称
        /// </summary>
        public string service { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public AlipayBuyerRequestModel()
        {
            notify_url = "";
            return_url = "";
            seller_email = "";
            out_trade_no = "";
            subject = "";
            price = "";
            body = "";
            show_url = "";
            receive_name = "";
            receive_address = "";
            receive_zip = "";
            receive_phone = "";
            receive_mobile = "";

            logistics_type = "EXPRESS";
            logistics_payment = "SELLER_PAY";
            logistics_fee = "0.00";
            payment_type = "1";
            service = "trade_create_by_buyer";
            quantity = "1";
        }

        /// <summary>
        /// 处理请求
        /// </summary>
        public override void PostProcessPayment()
        {
            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", Config.Partner);
            sParaTemp.Add("_input_charset", Config.Input_charset.ToLower());
            sParaTemp.Add("service", service);
            sParaTemp.Add("payment_type", payment_type);
            sParaTemp.Add("notify_url", notify_url);
            sParaTemp.Add("return_url", return_url);
            sParaTemp.Add("seller_email", seller_email);
            sParaTemp.Add("out_trade_no", out_trade_no);
            sParaTemp.Add("subject", subject);
            sParaTemp.Add("price", price);
            sParaTemp.Add("quantity", quantity);
            sParaTemp.Add("logistics_fee", logistics_fee);
            sParaTemp.Add("logistics_type", logistics_type);
            sParaTemp.Add("logistics_payment", logistics_payment);
            sParaTemp.Add("body", body);
            sParaTemp.Add("show_url", show_url);
            sParaTemp.Add("receive_name", receive_name);
            sParaTemp.Add("receive_address", receive_address);
            sParaTemp.Add("receive_zip", receive_zip);
            sParaTemp.Add("receive_phone", receive_phone);
            sParaTemp.Add("receive_mobile", receive_mobile);

            //建立请求
            string sHtmlText = Submit.BuildRequest(sParaTemp, "get", "确认");
            var context = HttpContext.Current!=null ? new HttpContextWrapper(HttpContext.Current) as HttpContextBase : (new FakeHttpContext("~/") as HttpContextBase);
            context.Response.Write(sHtmlText);
        }
    }
}
