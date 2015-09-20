using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weikeren.Utility.TimingTask.Enums
{
    /// <summary>
    /// 任务状态
    /// </summary>
    public enum TaskStates
    {
        /// <summary>
        /// 准备
        /// </summary>
        [EText("准备")]
        Ready,
        /// <summary>
        /// 运行中
        /// </summary>
        [EText("运行中")]
        Running,
        /// <summary>
        /// 停止
        /// </summary>
        [EText("停止")]
        Stop,
        /// <summary>
        /// 完成
        /// </summary>
        [EText("完成")]
        Completed
    }




}
