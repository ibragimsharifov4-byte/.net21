using Domain.Models;

namespace Domain.Interfaces;

public interface ICourseRepository
{
    Task AddCourse(Course course);
    Task DeleteCourse(int id);
    Task UpdateCourse(Course course);
    Task<List<Course>> GetCourses();
    Task<Course?> GetCourse(int id);
}
