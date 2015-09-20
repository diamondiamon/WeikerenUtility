using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weikeren.Utility.Expressage
{
    /// <summary>
    /// 快递信息获取器
    /// </summary>
    public interface IExpressageGetter
    {
        /// <summary>
        /// 返回订单查询的json字符串
        /// </summary>
        /// <param name="para">查询对象[物流公司+快递单号]</param>
        /// <returns>查询结果</returns>
        MResultMsg GetExpressageMessage(MQueryParameter para);
    }
}
