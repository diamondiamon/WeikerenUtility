using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Xml.Linq;

namespace Weikeren.Utility.Expressage
{
    /// <summary>
    /// 快递100
    /// </summary>
    public class Kuaidi100ExpressageGetter:IExpressageGetter
    {
        /// <summary>
        /// 返回订单查询的json字符串
        /// </summary>
        /// <param name="para">查询对象[物流公司+快递单号]</param>
        /// <returns>查询结果</returns>
        public MResultMsg GetExpressageMessage(MQueryParameter para)
        {
            // 获取配置文件
            //Configuration config = null;
            string queryUrl = "http://m.kuaidi100.com/query?type={0}&postid={1}";
            string com = string.Empty;
            MResultMsg msg = new MResultMsg();

            Queue<Action<MResultMsg, MQueryParameter>> myQueue = new Queue<Action<MResultMsg, MQueryParameter>>();

            if (string.IsNullOrEmpty(para.TypeCom))
            {
                msg.Result = false;
                msg.Error = new ErrorMsg() { ErrorCode = "001", ErrorMessage = "物流公司不能为空" };
                return msg;
            }

            if (string.IsNullOrEmpty(para.OrderId))
            {
                msg.Result = false;
                msg.Error = new ErrorMsg() { ErrorCode = "002", ErrorMessage = "订单号不能为空" };
                return msg;
            }

            try
            {
                //string configPath = System.IO.Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Expressage.config");
                //ExeConfigurationFileMap map = new ExeConfigurationFileMap();
                //map.ExeConfigFilename = configPath;
                //config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
                //queryUrl = config.AppSettings.Settings["queryUrl"] == null ? string.Empty : config.AppSettings.Settings["queryUrl"].Value;
                //com = config.AppSettings.Settings[para.TypeCom] == null ? string.Empty : config.AppSettings.Settings[para.TypeCom].Value;

                var expressageList = getExpressageList();
                var expressage = expressageList.Where(c => c.Key == para.TypeCom).FirstOrDefault();
                com = expressage == null ? string.Empty : expressage.Value;
            }
            catch (Exception ex)
            {
                msg.Result = false;
                msg.Error = new ErrorMsg() { ErrorCode = "003", ErrorMessage = ex.Message };
                return msg;
            }

            if (string.IsNullOrEmpty(com))
            {
                msg.Result = false;
                msg.Error = new ErrorMsg() { ErrorCode = "004", ErrorMessage = "配置文件缺少对于的物流公司类型" };
                return msg;
            }

            // 网上获取物流信息    
            string jsonResult = null;
            try
            {
                queryUrl = string.Format(queryUrl, com, para.OrderId);
                WebRequest request = WebRequest.Create(@queryUrl);
                WebResponse response = request.GetResponse();
                string message = string.Empty;
                using (Stream stream = response.GetResponseStream())
                {
                    Encoding encode = Encoding.UTF8;
                    using (StreamReader reader = new StreamReader(stream, encode))
                    {
                        message = reader.ReadToEnd();
                    }
                    jsonResult = message;
                }
            }
            catch (Exception ex)
            {
                msg.Result = false;
                msg.Error = new ErrorMsg() { ErrorCode = "005", ErrorMessage = ex.Message };
                return msg;
            }

            msg = JSONStringToObj<MResultMsg>(jsonResult);
            msg.JsonMessage = jsonResult;
            msg.Result = true;
            return msg;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        private List<ExpressKeyMapping> getExpressageList()
        {
            string expressKeyMappingPath = System.IO.Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "ExpressKeyMapping.xml");
            var expressesInfos = XElement.Load(expressKeyMappingPath);

            var mappings = from m
                          in expressesInfos.Elements()
                           select new ExpressKeyMapping()
                           {
                               Key = m.Element("Key").Value,
                               Value = m.Element("Value").Value
                           };

            var result = mappings.ToList();

            return result;

        }

        private static T JSONStringToObj<T>(string JsonStr)
        {
            JavaScriptSerializer Serializer = new JavaScriptSerializer();
            T objs = Serializer.Deserialize<T>(JsonStr);
            return objs;
        }
    }
}
