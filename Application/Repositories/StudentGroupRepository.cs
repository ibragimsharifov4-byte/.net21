using Application.Data;
using Dapper;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Repositories;

public class StudentGroupRepository(Database database) : IStudentGroupRepository
{
    private Database _data = database;
    public void AddStudentGroup(StudentGroup studentGroup)
    {
        using var con = _data.Connection();
        con.Open();
        var query = "insert into studentgroups(groupname , groupdescription) values(@groupname , @groupdescription)";
        var result = con.Execute(query, studentGroup);
        if (result > 0)
        {
            Console.WriteLine("Student group added successfully");
        }
        else
        {
            Console.WriteLine("Failed to add student group");
        }
    }

    public void DeleteStudentGroup(int id)
    {
        using var con = _data.Connection();
        con.Open();
        var query = "delete from studentgroups where studentgroupid = @id";
        var result = con.Execute(query, new { id });
        if (result > 0)
        {
            Console.WriteLine("Student group is deleted");
        }
        else
        {
            Console.WriteLine("Student group not found");
        }
    }

    public List<StudentGroup> GetStudentGroups()
    {
        using var con = _data.Connection();
        con.Open();
        var query = "select * from studentgroups";
        return con.Query<StudentGroup>(query).ToList();
    }

    public void UpdateStudentGroup(StudentGroup studentGroup)
    {
        using var con = _data.Connection();
        con.Open();
        var query = "update studentgroups set groupname = @groupname, groupdescription = @groupdescription where studentgroupid = @studentgroupid";
        var result = con.Execute(query, studentGroup);
        if (result > 0)
        {
            Console.WriteLine("Student group updated successfully");
        }
        else
        {
            Console.WriteLine("Failed to update student group");
        }
    }

}
