using System.Collections.Generic;
using System.Linq;
using ModelLayer.Models;

namespace ModelLayer.Repositories.Entity
{
    public class StudentEntityRepository: IRepository<Student>
    {
        private readonly EntityContext _db = new();

        public Student Create(Student entity)
        {
            entity = _db.Students.Add(entity);
            _db.SaveChanges();

            return entity;
        }

        public List<Student> Read()
        {
            return _db.Students.ToList();
        }

        public Student Update(Student entity)
        {
            Student student = _db.Students.SingleOrDefault(student => student.Id == entity.Id);

            if (student != null)
            {
                student.Name = entity.Name;
                student.Group = entity.Group;
                student.Speciality = entity.Speciality;
                _db.SaveChanges();

                return entity;
            }

            return null;
        }

        public bool Delete(int id)
        {
            Student a = _db.Students.FirstOrDefault(student => student.Id == id);

            if (a == null)
            {
                return false;
            }

            _db.Students.Remove(a);
            _db.SaveChanges();
            return true;
        }
    }
}