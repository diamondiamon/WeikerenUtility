using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weikeren.Utility.DenpendencyInjection
{
    public enum ComponentServiceLifetime
    {
        /// <summary>
        /// 单例
        /// </summary>
        Never = 0,
        /// <summary>
        /// 生命周期
        /// </summary>
        LifetimeScope = 1,
        /// <summary>
        /// 每次都实例
        /// </summary>
        PreInstance = 2
    }
}
