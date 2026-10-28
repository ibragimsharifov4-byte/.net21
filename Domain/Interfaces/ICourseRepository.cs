using Domain.Models;

namespace Domain.Interfaces;

public interface ICourseRepository
{
    void AddCourse(Course course);
    void DeleteCourse(int id);
    void UpdateCourse(Course course);
    List<Course> GetCourses();
    Course GetCourse(int id);
}
