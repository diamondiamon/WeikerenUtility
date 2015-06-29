using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weikeren.Utility.TimingTask.Enums
{
    /// <summary>
    /// 任务执行频率
    /// </summary>
    public enum Frequencies
    {
        /// <summary>
        /// 每秒
        /// </summary>
        [EText("每秒")]
        EverySecond,
        /// <summary>
        /// 每分钟
        /// </summary>
        [EText("每分钟")]
        EveryMinute,
        /// <summary>
        /// 每小时
        /// </summary>
        [EText("每小时")]
        EveryHour,
        /// <summary>
        /// 每天
        /// </summary>
        [EText("每天")]
        EveryDay,
        /// <summary>
        /// 每周
        /// </summary>
        [EText("每周")]
        EveryWeek,
        /// <summary>
        /// 每月
        /// </summary>
        [EText("每月")]
        EveryMonth,
        //  Yearly,
        /// <summary>
        /// 一次
        /// </summary>
        [EText("仅一次")]
        OneTime
    }
}
