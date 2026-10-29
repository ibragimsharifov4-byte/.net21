using Domain.Models;
using Domain.Enums;
namespace Domain.Interfaces;

public interface IStudentRepository
{
    Task AddStudent(Student student);
    Task DeleteStudent(int id);
    Task UpdateStudent(Student student);
    Task<List<Student>> GetStudents();
    Task<Student> GetStudent(int id);
}
