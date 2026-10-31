using Application.Data;
using Dapper;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Repositories;

public class CourseRepository(Database database) : ICourseRepository
{
    private Database _data = database;

    public async Task AddCourse(Course course)
    {
        using var con = _data.Connection();
        con.Open();
        var query = @"
            insert into courses(title, description, durationweek)
            values(@Title, @Description, @Durationweek)";
        var result = await con.ExecuteAsync(query, course);
        if (result > 0)
        {
            Console.WriteLine("Course added successfully");
        }
        else
        {
            Console.WriteLine("Failed to add course");
        }
    }

    public async Task DeleteCourse(int id)
    {
        using var con = _data.Connection();
        con.Open();
        var query = "delete from courses where courseid = @id";
        var result = await con.ExecuteAsync(query, new { id });
        if (result > 0)
        {
            Console.WriteLine("Course is deleted");
        }
        else
        {
            Console.WriteLine("Course not found");
        }
    }

    public async Task<Course?> GetCourse(int id)
    {
        using var con = _data.Connection();
        con.Open();
        var query = "select * from courses where courseid = @id";
        var result = await con.QueryFirstOrDefaultAsync<Course>(query, new { id });
        if (result == null)
        {
            Console.WriteLine("Course not found");
            return null;
        }
        else
        {
            return result;
        }
    }

    public async Task<List<Course>> GetCourses()
    {
        using var con = _data.Connection();
        con.Open();
        var query = "select * from courses";
        var result = await con.QueryAsync<Course>(query);
        return result.ToList();
    }

    public async Task UpdateCourse(Course course)
    {
        using var con = _data.Connection();
        con.Open();
        var query = @"
        update courses
        set title = @Title,
        description = @Description,
        durationweek = @Durationweek
        where courseid = @CourseId";
        var result = await con.ExecuteAsync(query, course);
        if (result > 0)
        {
            Console.WriteLine("Course updated successfully");
        }
        else
        {
            Console.WriteLine("Course not found");
        }
    }

    public async Task<int> GetTotalCoursesCount()
    {
        using var con = _data.Connection();
        con.Open();
        var query = "select count(*) from courses";
        var result = await con.ExecuteScalarAsync<int>(query);
        return result;
    }

    public async Task<List<Course>> GetStudentsPerCourse()
    {
        using var con = _data.Connection();
        con.Open();
        var query = @"
        select s.fullname , c.title from courses c
        join groups g on c.courseid = g.courseid
        join students s on g.groupid = s.groupid";
        var result = await con.QueryAsync<Course>(query);
        return result.ToList();
    }

    public async Task<List<Course>> GetMostPopularCourse()
    {
        using var con = _data.Connection();
        con.Open();
        var query = @"
        select c.title, count(s.studentid) as StudentsCount
        from courses c
        join groups g on c.courseid = g.courseid
        join studentgroups sg on g.groupid = sg.groupid
        join students s on sg.studentid = s.studentid
        group by c.title
        order by StudentsCount desc limit 1";
        var result = await con.QueryAsync<Course>(query);
        return result.ToList();
    }

    public async Task<List<Course>> GetLeastPopularCourses()
    {
        using var con = _data.Connection();
        con.Open();
        var query = @"
        select c.title, count(s.studentid) as StudentsCount
        from courses c
        join groups g on c.courseid = g.courseid
        join studentgroups sg on g.groupid = sg.groupid
        join students s on sg.studentid = s.studentid
        group by c.title
        order by StudentsCount desc";
        var result = await con.QueryAsync<Course>(query);
        return result.ToList();
    }

    public async Task<List<Course>> GetTopThreeCourses()
    {
        using var con = _data.Connection();
        con.Open();
        var query = @"
        select c.title, count(s.studentid) as StudentsCount
        from courses c
        join groups g on c.courseid = g.courseid
        join studentgroups sg on g.groupid = sg.groupid
        join students s on sg.studentid = s.studentid
        group by c.title
        order by StudentsCount desc limit 3";
        var result = await con.QueryAsync<Course>(query);
        return result.ToList();
    }

    
}