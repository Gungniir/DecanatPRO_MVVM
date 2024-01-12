using ModelLayer.Models;
using System.Collections.Generic;
using System;

namespace ModelLayer.Controllers;

public interface IStudentsController
{
    event EventHandler<Student> Added;
    event EventHandler<int> Deleted;

    public void Add(string name, string speciality, string group);

    public bool DeleteById(int id);

    public void CreateRandomStudents();

    public List<Student> GetAll();
}