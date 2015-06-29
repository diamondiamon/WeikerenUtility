
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weikeren.Utility.WebTest.DB.Model;
using Weikeren.Utility.EF;

namespace Weikeren.Utility.WebTest.DB.Repositories
{
    public class TecherRepository : Repository<Techer>, ITecherRepository
    {
        public TecherRepository(IDataBaseContext context)
            :base(context)
        {

        }
    }
}