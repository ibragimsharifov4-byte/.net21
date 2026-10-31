using Application.Data;
using Dapper;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Repositories;

public class StudentGroupRepository(Database database) : IStudentGroupRepository
{
    private Database _data = database;
    public async Task AddStudentGroup(StudentGroup studentGroup)
    {
        using var con = _data.Connection();
        con.Open();
        var query = 
        "insert into studentgroups(studentgroupId , groupId , status) values (@studentgroupId , @groupId , @status)";
        await con.ExecuteAsync(query , studentGroup);
    }

    public async Task DeleteStudentGroup(int id)
    {
        using var con = _data.Connection();
        con.Open();
        var query = 
        "delete from studentgroups where id = @id";
        await con.ExecuteAsync(query , new{id});
    }

    public async Task<List<StudentGroup>> GetStudentGroups()
    {
        using var con = _data.Connection();
        con.Open();
        var query = "select * from studentgroups";
        var studentgroups = await con.QueryAsync<StudentGroup>(query);
        return studentgroups.ToList();
    }

    public async Task UpdateStudentGroup(StudentGroup studentGroup)
    {
        using var con = _data.Connection();
        con.Open();
        var query = @"
        update studentgroups set 
        studentgroupId = @studentgroupId,
        studentId = @studentId,
        groupId = @groupId,
        status = @status
        where id = @id";
        await con.ExecuteAsync(query , studentGroup);
    }

}