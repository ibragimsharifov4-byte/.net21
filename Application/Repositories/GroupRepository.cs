using Application.Data;
using Dapper;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Repositories;

public class GroupRepository(Database database) : IGroupRepository
{
    private Database _data = database;

    public void AddGroup(Group group)
    {
        using var con = _data.Connection();
        con.Open();
        var query = @"
            insert into groups(groupname, courseid, mentorid, startdate, enddate)
            values(@GroupName, @CourseId, @MentorId, @StartDate, @EndDate)";
        var result = con.Execute(query, group);
        if (result > 0)
        {
            Console.WriteLine("Group added successfully");
        }
        else
        {
            Console.WriteLine("Failed to add group");
        }
    }



    public void DeleteGroup(int id)
    {
        using var con = _data.Connection();
        con.Open();
        var query = "delete from groups where groupid = @id";
        var result = con.Execute(query, new { id });
        if (result > 0)
        {
            Console.WriteLine("Group is deleted");
        }
        else
        {
            Console.WriteLine("Group not found");
        }
    }

    public Group? GetGroup(int id)
    {
        using var con = _data.Connection();
        con.Open();
        var query = "select * from groups where groupid = @id";
        var result = con.QueryFirstOrDefault<Group>(query, new { id });
        if (result == null)
        {
            Console.WriteLine("Group not found");
        }
        return result;
    }

    public List<Group> GetGroups()
    {
        using var con = _data.Connection();
        con.Open();
        var query = "select * from groups";
        var result = con.Query<Group>(query).ToList();
        return result;
    }

    public void UpdateGroup(Group group)
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
        var result = con.Execute(query, group);
        if (result > 0)
        {
            Console.WriteLine("Group updated successfully");
        }
        else
        {
            Console.WriteLine("Group not found");
        }
    }

    public int GetTotalGroupsCount()
    {
        using var con = _data.Connection();
        con.Open();
        var query = "select count(distinct groupid) from groups";
        var result = con.ExecuteScalar<int>(query);
        return result;
    }

    public List<DateTime> GetAllStartDates()
    {
        using var con = _data.Connection();
        con.Open();
        var query = "select distinct startdate from groups order by startdate";
        var result = con.Query<DateTime>(query).ToList();
        return result;
    }

    public List<Group> GetStudentsPerGroup()
    {
        using var con = _data.Connection();
        con.Open();
        var query = @"
            select g.groupid as GroupId,
            g.groupname as GroupName,
            count(sg.studentid) as StudentsCount
            from groups g
            left join studentgroups sg on g.groupid = sg.groupid
            group by g.groupid, g.groupname
            order by g.groupid";
        var result = con.Query<Group>(query).ToList();
        return result;
    }
}
