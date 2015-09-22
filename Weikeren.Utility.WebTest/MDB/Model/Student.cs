using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weikeren.Utility.MDB;

namespace Weikeren.Utility.WebTest.MDB.Model
{
    public class Student :BaseEntity
    {


        #region 属性定义

        ///// <summary>
        ///// 映射列[Id]
        ///// </summary>
        //[BsonId]
        //public new Object Id { get; set; }

        /// <summary>
        /// 映射列[Name]
        /// </summary>
        public  string Name { get; set; }
        
        /// <summary>
        /// 映射列[Age]
        /// </summary>
        public  Nullable<int> Age { get; set; }
        
        /// <summary>
        /// 映射列[TeacherId]
        /// </summary>
        public  Nullable<int> TeacherId { get; set; }
        
        /// <summary>
        /// 学生的分数
        /// </summary>
        public  Nullable<decimal> Score { get; set; }
        
		
        ///// <summary>
        ///// 主键表[techer]
        ///// </summary>
        //public  Techer techer { get; set; }
        
		

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
