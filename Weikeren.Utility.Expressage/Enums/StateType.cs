using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weikeren.Utility.Expressage
{
    /// <summary>
    /// 快递状态
    /// </summary>
    public enum StateType
    {
        在途 = 0,
        揽件 = 1,
        疑难 = 2,
        签收 = 3,
        退签 = 4,
        派件 = 5,
        退回 = 6
    }
}
