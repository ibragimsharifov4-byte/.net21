using Application.Data;
using Dapper;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Repositories;

public class GroupRepository(Database database) : IGroupRepository
{
    private Database _data = database;

    public async Task AddGroup(Group group)
    {
        using var con = _data.Connection();
        con.Open();
        var query = @"
        insert into groups(groupname, courseid, mentorid, startdate, enddate)
        values(@GroupName, @CourseId, @MentorId, @StartDate, @EndDate)";
        var result = await con.ExecuteAsync(query, group);
        if (result > 0)
        {
            Console.WriteLine("Group added successfully");
        }
        else
        {
            Console.WriteLine("Failed to add group");
        }
    }

    public async Task DeleteGroup(int id)
    {
        using var con = _data.Connection();
        con.Open();
        var query = "delete from groups where groupid = @id";
        var result = await con.ExecuteAsync(query, new { id });
        if (result > 0)
        {
            Console.WriteLine("Group is deleted");
        }
        else
        {
            Console.WriteLine("Group not found");
        }
    }

    public async Task<Group?> GetGroup(int id)
    {
        using var con = _data.Connection();
        con.Open();
        var query = "select * from groups where groupid = @id";
        var result = await con.QueryFirstOrDefaultAsync<Group>(query, new { id });
        if (result == null)
        {
            Console.WriteLine("Group not found");
            return null;
        }
        else
        {
            return result;
        }
    }

    public async Task<List<Group>> GetGroups()
    {
        using var con = _data.Connection();
        con.Open();
        var query = "select * from groups";
        var result = await con.QueryAsync<Group>(query);
        return result.ToList();
    }

    public async Task UpdateGroup(Group group)
    {
        using var con = _data.Connection();
        con.Open();
        var query = @"
        update groups
        set groupname = @GroupName,
        courseid = @CourseId,
        mentorid = @MentorId,
        startdate = @StartDate,
        enddate = @EndDate
        where groupid = @GroupId";
        var result = await con.ExecuteAsync(query, group);
        if (result > 0)
        {
            Console.WriteLine("Group updated successfully");
        }
        else
        {
            Console.WriteLine("Group not found");
        }
    }

    public async Task<int> GetTotalGroupsCount()
    {
        using var con = _data.Connection();
        con.Open();
        var query = "select count(*) from groups";
        var result = await con.ExecuteScalarAsync<int>(query);
        return result;
    }

    public async Task<List<DateTime>> GetAllStartDates()
    {
        using var con = _data.Connection();
        con.Open();
        var query = "select distinct startdate from groups order by startdate";
        var result = await con.QueryAsync<DateTime>(query);
        return result.ToList();
    }

    public async Task<List<Group>> GetStudentsPerGroup()
    {
        using var con = _data.Connection();
        con.Open();
        var query = @"
        select
        g.groupid as GroupId,
        g.groupname as GroupName,
        count(sg.studentid) as StudentsCount
        from groups g
        left join studentgroups sg on g.groupid = sg.groupid
        group by g.groupid, g.groupname
        order by g.groupid";
        var result = await con.QueryAsync<Group>(query);
        return result.ToList();
    }
}