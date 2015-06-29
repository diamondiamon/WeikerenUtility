using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Weikeren.Utility.WebTest.DB.Model;
using Weikeren.Utility.WebTest.DB.Repositories;
using Weikeren.Utility.DenpendencyInjection;

namespace Weikeren.Utility.WebTest.DB.Service
{
    public class StudentService
    {
        private readonly IStudentRepository _studentRepository = DIManager.Instance.Container.Resolve<IStudentRepository>();

        public IList<Student> GetStudent()
        {
            return _studentRepository.Search(c => 1 == 1);
        }

        public void Add(Student model)
        {
            _studentRepository.Add(model);
        }

    }
}