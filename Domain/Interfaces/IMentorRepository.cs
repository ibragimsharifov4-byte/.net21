using Domain.Models;

namespace Domain.Interfaces;

public interface IMentorRepository
{
    void AddMentor(Mentor mentor);
    void DeleteMentor(int id);
    void UpdateMentor(Mentor mentor);
    List<Mentor> GetMentors();
    Mentor GetMentor(int id);
}
