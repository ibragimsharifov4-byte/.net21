using Application.Data;
using Dapper;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Repositories;

public class StudentRepository(Database database) : IStudentRepository
{
    private Database _data = database;
    public async Task AddStudent(Student student)
    {
        using var con = _data.Connection();
        con.Open();
        var query =
        "insert into students(fullname , email , phone , enrollmentdate) values(@fullname , @email , @phone , @enrollmentdate)";
        var Student = await con.ExecuteAsync(query, student);
        if (Student > 0)
        {
            Console.WriteLine("Student added successfully");
        }
        else
        {
            Console.WriteLine("Failed to add student");
        }
    }

    public async Task DeleteStudent(int id)
    {
        using var con = _data.Connection();
        con.Open();
        var query =
        "delete from students where studentid = @id";
        var Deleting = await con.ExecuteAsync(query, new { id });
        if (Deleting > 0)
        {
            System.Console.WriteLine("Student is deleted");
        }
        else
        {
            System.Console.WriteLine("Student not found");
        }
    }

    public async Task<Student> GetStudent(int id)
    {
        using var con = _data.Connection();
        con.Open();
        var query = "select * from students where studentid = @id";
        var result = await con.QueryFirstOrDefaultAsync<Student>(query, new { id });
        if(result == null)
        {
            System.Console.WriteLine("Student not found");
            return null;
        }
        else
        {
            return result;
        }
    }

    public async Task<List<Student>> GetStudents()
    {
        using var con = _data.Connection();
        con.Open();
        var query = "select * from students";
        var result = await con.QueryAsync<Student>(query);
        return result.ToList();
    }

    public async Task UpdateStudent(Student student)
    {
        using var con = _data.Connection();
        con.Open();
        var query = "update students set fullname = @fullname, email = @email, phone = @phone, enrollmentdate = @enrollmentdate where studentid = @studentid";
        var result = await con.ExecuteAsync(query, student);
        if (result > 0)
        {
            System.Console.WriteLine("Student Updated!");
        }
        else
        {
            System.Console.WriteLine("Student not found");
        }
    }
    public async Task<int> GetTotalStudentsCount()
    {
        using var con = _data.Connection();
        con.Open();
        var query = "select count(*) from students";
        var result = await con.ExecuteScalarAsync<int>(query);
        return result;
    }

    public async Task<List<Student>> GetGraduatedStudents()
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
        var result = await con.QueryAsync<Student>(query, new { Status = Status.Finished });
        return result.ToList();
    }

    public async Task<List<Student>> GetStudentsWithGroups()
    {
        using var con = _data.Connection();
        con.Open();
        var query = @"select s.fullname , g.groupname from students s
        join studentgroups sg on s.studentid = sg.studentid
        join groups g on sg.groupid = g.groupid";
        var result = await con.QueryAsync<Student>(query);
        return result.ToList();
    }

    public async Task<List<Student>> GetStudentsWithoutGroups()
    {
        using var con = _data.Connection();
        con.Open();
        var query = @"select s.fullname from students s
        left join studentgroups sg on s.studentid = sg.studentid
        where sg.studentid is null";
        var result = await con.QueryAsync<Student>(query);
        return result.ToList();
    }

    public async Task<List<Student>> GetDroppedOutStudents()
    {
        using var con = _data.Connection();
        con.Open();
        var query = @"
        select s.* from students s 
        join studentgroups sg on s.studentid = sg.studentid
        where sg.status = @Status";
        var result = await con.QueryAsync<Student>(query, new { Status = Status.Left });
        return result.ToList();
    }

    

}
