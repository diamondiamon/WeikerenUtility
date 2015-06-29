using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weikeren.Utility.DenpendencyInjection
{
    public class ContainerFactory
    {
        public static IDenpendencyContainer CreateNewContainer(ContainerStyle style)
        {
            switch (style)
            {
                case  ContainerStyle.Autofac:
                    ContainerBuilder builder = new ContainerBuilder();
                    var container = new AutofacContainer(builder.Build());
                    return container;
                //case "Microsoft Unity":
                //    IUnityContainer unityBuilder = new UnityContainer();
                //    MicrosoftUnityContainer unityContainer = new MicrosoftUnityContainer(unityBuilder);
                //    return unityContainer;
                default:
                    ContainerBuilder builder2 = new ContainerBuilder();
                    var container2 = new AutofacContainer(builder2.Build());
                    return container2;
            }
        }
    }
}
