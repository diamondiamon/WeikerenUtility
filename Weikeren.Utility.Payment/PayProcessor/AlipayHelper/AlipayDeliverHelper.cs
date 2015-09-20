using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Weikeren.Utility.Payment.PayProcessor.AlipayHelper
{
    /// <summary>
    /// 
    /// </summary>
    public class AlipayDeliverResponModel
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 错误代码
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// 返回xml
        /// </summary>
        public string Data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public AlipayDeliverResponModel()
        {
            Success = false;
            Data = "";
        }
    }

    /// <summary>
    /// 支付宝发货助手
    /// </summary>
    public class AlipayDeliverHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="trade_no">支付宝交易号</param>
        /// <param name="logistics_name">物流公司名称</param>
        /// <param name="invoice_no">物流发货单号</param>
        /// <param name="transport_type">物流运输类型。三个值可选：POST（平邮）、EXPRESS（快递）、EMS（EMS）</param>
        /// <param name="SendSuccess">发货成功回调函数</param>
        /// <param name="SendError">发货失败回调函数</param>
        public static void Deliver(string trade_no, string logistics_name, string invoice_no, string transport_type, Action<AlipayDeliverResponModel> SendSuccess, Action<Exception, AlipayDeliverResponModel> SendError)
        {
            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", Config.Partner);
            sParaTemp.Add("_input_charset", Config.Input_charset.ToLower());
            sParaTemp.Add("service", "send_goods_confirm_by_platform");
            sParaTemp.Add("trade_no", trade_no);
            sParaTemp.Add("logistics_name", logistics_name);
            sParaTemp.Add("invoice_no", invoice_no);
            sParaTemp.Add("transport_type", transport_type);

            //建立请求
            string sHtmlText = Submit.BuildRequest(sParaTemp);

            AlipayDeliverResponModel responseData = new AlipayDeliverResponModel() { Data = sHtmlText };

            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.LoadXml(sHtmlText);
                string strXmlResponse = xmlDoc.SelectSingleNode("/alipay/is_success").InnerText;
                string errCode = xmlDoc.SelectSingleNode("/alipay/error").InnerText;
                responseData.ErrorCode = errCode;
                if(strXmlResponse=="T")
                {
                    responseData.Success = true;
                }

                if(SendSuccess!=null)
                {
                    SendSuccess.Invoke(responseData);
                }
                //Response.Write(strXmlResponse);
            }
            catch (Exception exp)
            {
                if (SendError != null)
                {
                    SendError.Invoke(exp, responseData);
                }
                //Response.Write(sHtmlText);
            }
        }
    }
}
