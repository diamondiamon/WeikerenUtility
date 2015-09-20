using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weikeren.Utility.WebTest.DITest
{
    public class A:IA
    {
        private string k = "";

        public A()
        {
            k = "Instance";
        }

        public string Test1()
        {
            return k;
        }
    }
}