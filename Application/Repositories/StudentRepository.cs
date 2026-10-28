using Application.Data;
using Dapper;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Repositories;

public class StudentRepository(Database database) : IStudentRepository
{
    private Database _data = database;
    public void AddStudent(Student student)
    {
        using var con = _data.Connection();
        con.Open();
        var query =
        "insert into students(fullname , email , phone , enrollmentdate) values(@fullname , @email , @phone , @enrollmentdate)";
        var Student = con.Execute(query, student);
        if (Student > 0)
        {
            Console.WriteLine("Student added successfully");
        }
        else
        {
            Console.WriteLine("Failed to add student");
        }
    }

    public void DeleteStudent(int id)
    {
        using var con = _data.Connection();
        con.Open();
        var query =
        "delete from students where studentid = @id";
        var Deleting = con.Execute(query, new { id });
        if (Deleting > 0)
        {
            System.Console.WriteLine("Student is deleted");
        }
        else
        {
            System.Console.WriteLine("Student not found");
        }
    }

    public Student GetStudent(int id)
    {
        using var con = _data.Connection();
        con.Open();
        var result = con.QueryFirstOrDefault<Student>("select * from students where studentid = @id", new { id });
        if (result == null)
        {
            Console.WriteLine("Student not found");
        }
        return result;
    }

    public List<Student> GetStudents()
    {
        using var con = _data.Connection();
        con.Open();
        var query = "select * from students";
        var result = con.Query<Student>(query).ToList();
        return result;
    }

    public void UpdateStudent(Student student)
    {
        using var con = _data.Connection();
        con.Open();
        var query =
        @"update students 
        set studentid = @studentid , fullname = @fullname , email = @email , phone = @phone , enrollmentdate = @enrollmentdate
        where studentid = @id";
        var result = con.Execute(query, student);
        if (result > 0)
        {
            System.Console.WriteLine("Student Updated!");
        }
        else
        {
            System.Console.WriteLine("Student not found");
        }
    }
    public int GetTotalStudentsCount()
    {
        using var con = _data.Connection();
        con.Open();
        var query = "select count(*) from students";
        var result = con.ExecuteScalar<int>(query);
        return result;
    }

    public List<Student> GetDroppedOutStudents()
    {
        using var con = _data.Connection();
        con.Open();
        var query = @"
            select s.*
            from students s
            where exists (
            select 1
            from studentgroups sg
            where sg.studentid = s.studentid
            and sg.status = @Status
            )";
        var result = con.Query<Student>(
        query, new { Status = Status.Left }).ToList();
        return result;
    }

    public List<Student> GetGraduatedStudents()
    {
        using var con = _data.Connection();
        con.Open();

        var query = @"
        select s.*
        from students s
        where exists (
        select 1
        from studentgroups sg
        where sg.studentid = s.studentid
        and sg.status = @Status
        )";
        var result = con.Query<Student>(
        query, new { Status = Status.Finished }).ToList();
        return result;
    }
}
