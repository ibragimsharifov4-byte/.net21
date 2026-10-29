using Domain.Models;

namespace Domain.Interfaces;

public interface IMentorRepository
{
    Task AddMentor(Mentor mentor);
    Task DeleteMentor(int id);
    Task UpdateMentor(Mentor mentor);
    Task<List<Mentor>> GetMentors();
    Task<Mentor> GetMentor(int id);
}
