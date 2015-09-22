using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Weikeren.Utility.WebTest.MDB.Model;
using Weikeren.Utility.WebTest.MDB.Repositories;
using Weikeren.Utility.DenpendencyInjection;

namespace Weikeren.Utility.WebTest.MDB.Service
{
    public class StudentService
    {
        //private readonly IStudentRepository _studentRepository = DIManager.Instance.Container.Resolve<IStudentRepository>();

        private readonly IStudentRepository _studentRepository = new StudentRepository(new TestContext());

        public IList<Student> GetStudent()
        {
            return _studentRepository.Search(c => 1 == 1);
        }

        public void Add(Student model)
        {
            try
            {
                _studentRepository.Add(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Student model)
        {
            try
            {
                _studentRepository.Update(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Student GetById(int Id)
        {
            return _studentRepository.GetbyId(Id);
            //return _studentRepository.SearchSingle(c => c.Id == (object)Id);
        }


        public List<Student> Search()
        {
            var list = _studentRepository.Search<Student>(c => c.Name == "111",c=>c);

            return list.ToList();
        }

        public void Delete()
        {
           // _studentRepository.DeleteById(83);
            _studentRepository.DeleteBy(c => c.Name == "XXXXXXX");
        }
    }
}