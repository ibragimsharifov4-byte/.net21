using Domain.Models;

namespace Domain.Interfaces;

public interface IStudentGroupRepository
{
    Task AddStudentGroup(StudentGroup studentGroup);
    Task DeleteStudentGroup(int id);
    Task UpdateStudentGroup(StudentGroup studentGroup);
    Task<List<StudentGroup>> GetStudentGroups();
}
