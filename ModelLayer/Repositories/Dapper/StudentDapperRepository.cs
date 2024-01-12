using System.Collections.Generic;
using System.Linq;
using Dapper;
using ModelLayer.Models;
using Npgsql;

namespace ModelLayer.Repositories.Dapper;

public class StudentDapperRepository : IRepository<Student>
{
    private readonly NpgsqlConnection _connection;

    public StudentDapperRepository()
    {
        _connection =
            new NpgsqlConnection("Server=localhost;Port=5432;User ID=decanat;Password=decanat;Database=decanat");
    }

    public Student Create(Student entity)
    {
        dynamic result = _connection.QuerySingle(
            "insert into students(name, speciality, \"group\") values(@name, @speciality, @group) returning id", new
            {
                name = entity.Name,
                speciality = entity.Speciality,
                group = entity.Group,
            });

        entity.Id = result.id;

        return entity;
    }

    public List<Student> Read()
    {
        return _connection.Query<Student>("select * from students").ToList();
    }

    public Student Update(Student entity)
    {
        _connection.Execute(
            "update students set name = @name, speciality = @speciality, \"group\" = @group where id = @id", new
            {
                id = entity.Id,
                group = entity.Group,
                speciality = entity.Speciality,
                name = entity.Name,
            });

        return entity;
    }

    public bool Delete(int id)
    {
        _connection.Execute("delete from students where id = @id", new { id });

        return true;
    }
}