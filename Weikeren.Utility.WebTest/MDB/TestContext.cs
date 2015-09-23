using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Reflection;
using System.Linq;
using Weikeren.Article.EntityFramework;
using System.Data.Entity.ModelConfiguration;
using System;
using Weikeren.Utility.MDB;
using Weikeren.Utility.WebTest.MDB.Model;

namespace Weikeren.Utility.WebTest.MDB
{
    public partial class TestContext :  DataBaseContext, IDataBaseContext
    {
        static TestContext()
        {
        }

        public TestContext()
            : base("MDBContext")
        {
                
        }
        
    }
}
