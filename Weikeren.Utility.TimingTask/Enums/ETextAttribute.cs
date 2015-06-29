using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weikeren.Utility.TimingTask.Enums
{
    /// <summary>
    /// 枚举附加属性
    /// </summary>
    public class ETextAttribute : Attribute
    {
        private string _displayValue;
        /// <summary>
        /// 显示文字
        /// </summary>
        public string DisplayValue
        {
            get { return _displayValue; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="displayValue"></param>
        public ETextAttribute(string displayValue)
        {         
            _displayValue = displayValue;
        }
    }
}
