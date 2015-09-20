using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weikeren.Utility.Expressage
{
    /// <summary>
    /// 查询参数
    /// </summary>
    public class MQueryParameter
    {
        /// <summary>
        /// 快递公司
        /// </summary>
        public string TypeCom { get; set; }

        /// <summary>
        /// 快递订单号
        /// </summary>
        public string OrderId { get; set; }
    }
}
