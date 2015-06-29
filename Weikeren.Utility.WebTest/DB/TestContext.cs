using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Reflection;
using System.Linq;
using Weikeren.Article.EntityFramework;
using System.Data.Entity.ModelConfiguration;
using System;
using Weikeren.Utility.EF;
using Weikeren.Utility.WebTest.DB.Model;

namespace Weikeren.Article.EntityFramework
{
    public partial class TestContext :  DataBaseContext, IDataBaseContext
    {
        static TestContext()
        {
            Database.SetInitializer<TestContext>(null);
        }

        public TestContext()
            : base("Name=WeikerenArticleContext")
        {
            this.Database.Initialize(false);    
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new StudentMap());
            modelBuilder.Configurations.Add(new TecherMap());

            //string nameSpace = "Weikeren.Article.Domain.Models.Mapping";

            //var assembly = Assembly.Load("Weikeren.Article.Domain");
            //var typesToRegister = assembly.GetTypes()
            //.Where(type => !string.IsNullOrEmpty(type.Namespace))
            //.Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>))
            //.Where(type => type.Namespace.Contains(nameSpace));
            //foreach (var type in typesToRegister)
            //{
            //    dynamic configurationInstance = Activator.CreateInstance(type);
            //    modelBuilder.Configurations.Add(configurationInstance);
            //}
        }
    }
}
