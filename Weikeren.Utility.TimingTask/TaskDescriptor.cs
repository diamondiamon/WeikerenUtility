using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Weikeren.Utility.TimingTask.Enums;

namespace Weikeren.Utility.TimingTask
{
    /// <summary>
    /// 任务项
    /// </summary>
    public class TaskDescriptor
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaskDescriptor()
        {
            Recurs = 1;
            IsFixedTime = false;
        }

        /// <summary>
        /// 任务标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 任务描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 任务开始时间
        /// </summary>
        [XmlIgnore]
        public DateTime? StartAt { get; set; }


        /// <summary>
        /// 替代屬性
        /// </summary>
        [XmlElement("StartAt")]
        public string StartAtString
        {
            get
            {
                if (StartAt == null)
                    return string.Empty;

                return this.StartAt.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }
            set
            {
                this.StartAt = DateTime.Parse(value);
            }
        }

        /// <summary>
        /// 执行频率
        /// </summary>
        public Frequencies Frequency { get; set; }
        /// <summary>
        /// 频率间隔值（默认为1）
        /// </summary>
        public int Recurs { get; set; }

        /// <summary>
        /// 是否在固定时间上执行（默认为False，即执行完后，再暂停设置的时间间隔后再执行，若设置为True，即固定相隔多久执行一次，不管相隔时间内是否执行完成）
        /// </summary>
        public bool IsFixedTime { get; set; }

        /// <summary>
        /// 下次执行的时间
        /// </summary>
        /// <param name="startedTime"></param>
        /// <returns></returns>
        public DateTime? GetNextStartTime(DateTime? startedTime)
        {
            if (!startedTime.HasValue) return null;

            var nextStart = startedTime.Value;

            switch (Frequency)
            {
                case Frequencies.EverySecond:
                    nextStart = nextStart.AddSeconds(Recurs);
                    break;
                case Frequencies.EveryMinute:
                    nextStart = nextStart.AddMinutes(Recurs);
                    break;
                case Frequencies.EveryHour:
                    nextStart = nextStart.AddHours(Recurs);
                    break;
                case Frequencies.EveryDay:
                    nextStart = nextStart.AddDays(Recurs);
                    break;
                case Frequencies.EveryWeek:
                    var offset = 7 * Recurs;
                    nextStart = nextStart.AddDays(offset);
                    break;
                case Frequencies.EveryMonth:
                    nextStart = nextStart.AddMonths(Recurs);
                    break;
            }

            return nextStart;


        }
        /// <summary>
        /// 获取执行一次后要等待的时间间隔
        /// </summary>
        /// <returns></returns>
        public TimeSpan GetWaitSeconds()
        {

            switch (Frequency)
            {
                case Frequencies.EverySecond:
                    return DateTime.Now.AddSeconds(Recurs) - DateTime.Now;
                case Frequencies.EveryMinute:
                    return DateTime.Now.AddMinutes(Recurs) - DateTime.Now;
                case Frequencies.EveryHour:
                    return DateTime.Now.AddHours(Recurs) - DateTime.Now;
                case Frequencies.EveryDay:
                    return DateTime.Now.AddDays(Recurs) - DateTime.Now;
                case Frequencies.EveryWeek:
                    return DateTime.Now.AddDays(7 * Recurs) - DateTime.Now;
                case Frequencies.EveryMonth:
                    return DateTime.Now.AddMonths(Recurs) - DateTime.Now;
            }

            return new TimeSpan(0);
        }

    }
}
