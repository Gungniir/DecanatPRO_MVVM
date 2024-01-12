using ModelLayer.Models;
using ModelLayer.Repositories;
using System;
using System.Collections.Generic;

namespace ModelLayer.Controllers
{
    internal class StudentsController : IStudentsController
    {
        private readonly IRepository<Student> _studentRepository;

        public StudentsController(IRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public event EventHandler<Student> Added = delegate { };
        public event EventHandler<int> Deleted = delegate { };

        public void Add(string name, string speciality, string group)
        {
            var student = _studentRepository.Create(new Student
            {
                Name = name,
                Speciality = speciality,
                Group = group,
            });

            Added.Invoke(this, student);
        }

        public bool DeleteById(int id)
        {
            var result = _studentRepository.Delete(id);

            if (result)
            {
                Deleted.Invoke(this, id);
            }

            return result;
        }

        public void CreateRandomStudents()
        {
            int baseSeed = DateTime.Now.Millisecond;

            for (int i = 0; i < 8; i++)
            {
                Student student = new Student();
                student.FillRandom(baseSeed * (100 + i));

                student = _studentRepository.Create(student);

                Added.Invoke(this, student);
            }
        }

        public List<Student> GetAll()
        {
            return _studentRepository.Read();
        }
    }
}