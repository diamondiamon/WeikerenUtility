using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Weikeren.Article.EntityFramework;
using Weikeren.Utility.WebTest.DB.Repositories;
using Weikeren.Utility.DenpendencyInjection;
using Weikeren.Utility.EF;

namespace Weikeren.Utility.WebTest.DITest
{
    public class DIRegister : DependencyRegister
    {
        protected override ContainerStyle ContainerStyle
        {
            get
            {
                return Weikeren.Utility.DenpendencyInjection.ContainerStyle.Autofac;
            }
            set { }
        }

        protected override void Register(IDenpendencyContainer container)
        {
            container.AddComponent<IA, A>(string.Empty, ComponentServiceLifetime.LifetimeScope);

            container.AddComponent<IDataBaseContext, TestContext>(string.Empty, ComponentServiceLifetime.LifetimeScope);
            container.AddComponent<IStudentRepository, StudentRepository>("", ComponentServiceLifetime.LifetimeScope);
            container.AddComponent<ITecherRepository, TecherRepository>("", ComponentServiceLifetime.LifetimeScope);
        }
    }
}