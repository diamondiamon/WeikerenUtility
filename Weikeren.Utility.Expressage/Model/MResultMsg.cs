using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weikeren.Utility.Expressage
{
    /// <summary>
    /// 结果信息
    /// </summary>
    public class MResultMsg
    {
        /// <summary>
        /// 结果
        /// </summary>
        public bool Result { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public StateType State { get; set; }
        /// <summary>
        /// 返回的JSON
        /// </summary>
        public string JsonMessage { get; set; }
        /// <summary>
        /// 快递信息
        /// </summary>
        public List<ExpressageInfo> Data { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public ErrorMsg Error { get; set; }
    }

   

   
}
