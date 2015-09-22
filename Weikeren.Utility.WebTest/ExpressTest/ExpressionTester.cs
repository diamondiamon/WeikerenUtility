using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Weikeren.Utility.WebTest.ExpressTest
{
    public class ExpressionTester
    {

        public void Test(Expression<Func<User, bool>> whereLamdba)
        {

        }

    }


    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
    }


}