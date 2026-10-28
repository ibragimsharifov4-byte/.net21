using Application.Data;
using Dapper;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Repositories;

public class CourseRepository(Database database) : ICourseRepository
{
    private Database _data = database;

    public void AddCourse(Course course)
    {
        using var con = _data.Connection();
        con.Open();
        var query = @"
            insert into courses(title, description, durationweek)
            values(@Title, @Description, @Durationweek)";
        var result = con.Execute(query, course);
        if (result > 0)
        {
            Console.WriteLine("Course added successfully");
        }
        else
        {
            Console.WriteLine("Failed to add course");
        }
    }

    public void DeleteCourse(int id)
    {
        using var con = _data.Connection();
        con.Open();
        var query = "delete from courses where courseid = @id";
        var result = con.Execute(query, new { id });
        if (result > 0)
        {
            Console.WriteLine("Course is deleted");
        }
        else
        {
            Console.WriteLine("Course not found");
        }
    }

    public Course? GetCourse(int id)
    {
        using var con = _data.Connection();
        con.Open();
        var query = "select * from courses where courseid = @id";
        var result = con.QueryFirstOrDefault<Course>(query, new { id });
        if (result == null)
        {
            Console.WriteLine("Course not found");
        }
        return result;
    }

    public List<Course> GetCourses()
    {
        using var con = _data.Connection();
        con.Open();
        var query = "select * from courses";
        var result = con.Query<Course>(query).ToList();
        return result;
    }

    public void UpdateCourse(Course course)
    {
        using var con = _data.Connection();
        con.Open();
        var query = @"
            update courses
            set title = @Title,
                description = @Description,
                durationweek = @Durationweek
            where courseid = @CourseId";
        var result = con.Execute(query, course);
        if (result > 0)
        {
            Console.WriteLine("Course updated successfully");
        }
        else
        {
            Console.WriteLine("Course not found");
        }
    }

    public int GetTotalCoursesCount()
    {
        using var con = _data.Connection();
        con.Open();
        var query = "select count(distinct courseid) from courses";
        var result = con.ExecuteScalar<int>(query);
        return result;
    }

    public List<Course> GetStudentsPerCourse()
    {
    using var con = _data.Connection();
    con.Open();

    var query = @"
        select c.courseid,
        c.title,
        count(distinct sg.studentid)
        from courses c
        left join groups g on c.courseid = g.courseid
        left join studentgroups sg on g.groupid = sg.groupid
        group by c.courseid, c.title
        order by StudentsCount desc";
    var result = con.Query<Course>(query).ToList();
    return result;
    }
    
    public List<Course> GetLeastPopularCourses()
    {
        using var con = _data.Connection();
        con.Open();
        var query = @"
        select c.courseid as CourseId,
        c.title as Title,
        count(distinct sg.studentid) as StudentsCount
        from courses c
        left join groups g on c.courseid = g.courseid
        left join studentgroups sg on g.groupid = sg.groupid
        group by c.courseid, c.title
        order by StudentsCount, c.courseid
        limit 3";
        var result = con.Query<Course>(query).ToList();
        return result;
    }
}

