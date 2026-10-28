using Domain.Models;

namespace Domain.Interfaces;

public interface IStudentGroupRepository
{
    void AddStudentGroup(StudentGroup studentGroup);
    void DeleteStudentGroup(int id);
    void UpdateStudentGroup(StudentGroup studentGroup);
    List<StudentGroup> GetStudentGroups();
}
