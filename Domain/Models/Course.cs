namespace Domain.Models;

public class Course
{
    public int CourseId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime Durationweek { get; set; }
}
