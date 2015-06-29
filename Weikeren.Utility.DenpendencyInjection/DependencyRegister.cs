using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weikeren.Utility.DenpendencyInjection
{
    public abstract class DependencyRegister
    {
        public IDenpendencyContainer Container { get; private set; }

        public DependencyRegister()
        {
            Container = ContainerFactory.CreateNewContainer(ContainerStyle);
        }

        /// <summary>
        /// 依赖注册
        /// </summary>
        public void Register()
        {
            Register(Container);
        }

        /// <summary>
        /// 依赖容器类型
        /// </summary>
        protected abstract ContainerStyle ContainerStyle { get; set; }

        /// <summary>
        /// 依赖注册
        /// </summary>
        /// <param name="container">依赖容器</param>
        protected abstract void Register(IDenpendencyContainer container);
    }
}
