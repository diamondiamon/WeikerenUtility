
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weikeren.Utility.MDB;
using Weikeren.Utility.WebTest.MDB.Model;

namespace Weikeren.Utility.WebTest.MDB.Repositories
{
    public class TecherRepository : Repository<Techer>, ITecherRepository
    {
        public TecherRepository(IDataBaseContext context)
            :base(context)
        {

        }
    }
}