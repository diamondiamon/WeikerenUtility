using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weikeren.Utility.EF;

namespace Weikeren.Utility.WebTest.DB.Model
{
    public class Student :BaseEntity
    {
		
		#region 属性定义
		
        /// <summary>
        /// 映射列[Id]
        /// </summary>
        public virtual int Id { get; set; }
        
        /// <summary>
        /// 映射列[Name]
        /// </summary>
        public virtual string Name { get; set; }
        
        /// <summary>
        /// 映射列[Age]
        /// </summary>
        public virtual Nullable<int> Age { get; set; }
        
        /// <summary>
        /// 映射列[TeacherId]
        /// </summary>
        public virtual Nullable<int> TeacherId { get; set; }
        
        /// <summary>
        /// 学生的分数
        /// </summary>
        public virtual Nullable<decimal> Score { get; set; }
        
		
        /// <summary>
        /// 主键表[techer]
        /// </summary>
        public virtual Techer techer { get; set; }
        
		

	    #endregion

	    #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public Student()
        { 
			
        }

        #endregion 

		#region 重载 GetHashCode

        /// <summary>
        /// GetHashCode（重载函数）
        /// </summary>
        /// <returns></returns>
        override public int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
