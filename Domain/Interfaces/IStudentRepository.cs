using Domain.Models;
using Domain.Enums;
namespace Domain.Interfaces;

public interface IStudentRepository
{
    void AddStudent(Student student);
    void DeleteStudent(int id);
    void UpdateStudent(Student student);
    List<Student> GetStudents();
    Student GetStudent(int id);
}
