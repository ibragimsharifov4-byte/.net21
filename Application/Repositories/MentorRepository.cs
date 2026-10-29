using Application.Data;
using Dapper;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Repositories;

public class MentorRepository(Database database) : IMentorRepository
{
    private Database _data = database;

    public async Task AddMentor(Mentor mentor)
    {
        using var con = _data.Connection();
        con.Open();

        var query = @"
            insert into mentors(fullname, email, phone, specialization)
            values(@Fullname, @Email, @Phone, @Specialization)";

        var result = await con.ExecuteAsync(query, mentor);

        if (result > 0)
        {
            Console.WriteLine("Mentor added successfully");
        }
        else
        {
            Console.WriteLine("Failed to add mentor");
        }
    }

    public async Task DeleteMentor(int id)
    {
        using var con = _data.Connection();
        con.Open();

        var query = "delete from mentors where mentorid = @id";

        var result = await con.ExecuteAsync(query, new { id });

        if (result > 0)
        {
            Console.WriteLine("Mentor is deleted");
        }
        else
        {
            Console.WriteLine("Mentor not found");
        }
    }

    public async Task<Mentor?> GetMentor(int id)
    {
        using var con = _data.Connection();
        con.Open();

        var query = "select * from mentors where mentorid = @id";

        var result = await con.QueryFirstOrDefaultAsync<Mentor>(query, new { id });

        if (result == null)
        {
            Console.WriteLine("Mentor not found");
            return null;
        }
        else
        {
            return result;
        }
    }

    public async Task<List<Mentor>> GetMentors()
    {
        using var con = _data.Connection();
        con.Open();

        var query = "select * from mentors";

        var result = await con.QueryAsync<Mentor>(query);

        return result.ToList();
    }

    public async Task UpdateMentor(Mentor mentor)
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

        var result = await con.ExecuteAsync(query, mentor);

        if (result > 0)
        {
            Console.WriteLine("Mentor updated successfully");
        }
        else
        {
            Console.WriteLine("Mentor not found");
        }
    }

    public async Task<int> GetTotalMentorsCount()
    {
        using var con = _data.Connection();
        con.Open();

        var query = "select count(*) from mentors";

        var result = await con.ExecuteScalarAsync<int>(query);

        return result;
    }
}