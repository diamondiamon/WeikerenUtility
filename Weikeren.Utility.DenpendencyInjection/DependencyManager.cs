using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weikeren.Utility.DenpendencyInjection
{
    /// <summary>
    /// 依赖注入管理器
    /// </summary>
    public sealed class DIManager
    {
        private static readonly DIManager _instance = new DIManager();


        private DIManager()
        {
        }

        public static DIManager Instance
        {
            get { return _instance; }
        }


        public void Init(DependencyRegister dr) {
            Container = dr.Container;
            dr.Register();
        }


        /// <summary>
        /// 依赖注册容器
        /// </summary>
        public IDenpendencyContainer Container
        {
            get;
            private set;
        }




    }
}
