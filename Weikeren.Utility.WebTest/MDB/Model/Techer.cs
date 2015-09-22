using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weikeren.Utility.MDB;

namespace Weikeren.Utility.WebTest.MDB.Model
{
    public class Techer : BaseEntity
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
        /// 映射列[Description]
        /// </summary>
        public virtual string Description { get; set; }
        
		
		
        /// <summary>
        /// 外键表[Student]
        /// </summary>
		public virtual ICollection<Student> Students { get; set; }
        

	    #endregion

	    #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public Techer()
        {
            this.Students = new List<Student>();

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
