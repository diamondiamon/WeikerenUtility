using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weikeren.Utility.TimingTask
{
    /// <summary>
    /// 工作接口
    /// </summary>
    public interface IJob
    {
        /// <summary>
        /// 执行
        /// </summary>
        void Execute();
        /// <summary>
        /// 发生错误
        /// </summary>
        /// <param name="exception"></param>
        void OnError(Exception exception);
    }
}
