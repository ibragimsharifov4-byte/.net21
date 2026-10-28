
using Application.Data;
using Dapper;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Repositories;

public class MentorRepository(Database database) : IMentorRepository
{
    private Database _data = database;

    public void AddMentor(Mentor mentor)
    {
        using var con = _data.Connection();
        con.Open();
        var query = @"
            insert into mentors(fullname, email, phone, specialization)
            values(@Fullname, @Email, @Phone, @Specialization)";
        var result = con.Execute(query, mentor);
        if (result > 0)
        {
            Console.WriteLine("Mentor added successfully");
        }
        else
        {
            Console.WriteLine("Failed to add mentor");
        }
    }

    public void DeleteMentor(int id)
    {
        using var con = _data.Connection();
        con.Open();
        var query = "delete from mentors where mentorid = @id";
        var result = con.Execute(query, new { id });
        if (result > 0)
        {
            Console.WriteLine("Mentor is deleted");
        }
        else
        {
            Console.WriteLine("Mentor not found");
        }
    }

    public Mentor? GetMentor(int id)
    {
        using var con = _data.Connection();
        con.Open();
        var query = "select * from mentors where mentorid = @id";
        var result = con.QueryFirstOrDefault<Mentor>(query, new { id });
        if (result == null)
        {
            Console.WriteLine("Mentor not found");
        }
        return result;
    }

    public List<Mentor> GetMentors()
    {
        using var con = _data.Connection();
        con.Open();
        var query = "select * from mentors";
        var result = con.Query<Mentor>(query).ToList();
        return result;
    }

    public void UpdateMentor(Mentor mentor)
    {
        using var con = _data.Connection();
        con.Open();
        var query = @"
            update mentors
            set fullname = @Fullname,
                email = @Email,
                phone = @Phone,
                specialization = @Specialization
            where mentorid = @MentorId";
        var result = con.Execute(query, mentor);
        if (result > 0)
        {
            Console.WriteLine("Mentor updated successfully");
        }
        else
        {
            Console.WriteLine("Mentor not found");
        }
    }

    public int GetTotalMentorsCount()
    {
        using var con = _data.Connection();
        con.Open();
        var query = "select count(*) from mentors";
        var result = con.ExecuteScalar<int>(query);
        return result;
    }
}
