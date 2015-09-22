
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weikeren.Utility.WebTest.MDB.Model;
using Weikeren.Utility.MDB;

namespace Weikeren.Utility.WebTest.MDB.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(IDataBaseContext context)
            :base(context)
        {

        }
    }
}