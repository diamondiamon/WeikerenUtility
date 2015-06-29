using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weikeren.Utility
{
    /// <summary>
    /// 释放对象
    /// </summary>
    public abstract class DisposableObject : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                DoDispose();
            }
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~DisposableObject()
        {
            Dispose(false);
        }
        /// <summary>
        /// 
        /// </summary>
        public abstract void DoDispose();

        /// <summary>
        /// 释放对象
        /// </summary>
        /// <param name="disposeObject"></param>
        protected void DisposeIt(IDisposable disposeObject)
        {
            if (disposeObject != null)
            {
                disposeObject.Dispose();
            }
        }

    }
}
