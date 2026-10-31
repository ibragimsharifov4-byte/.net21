using Application.Data;
using Application.Repositories;
using Domain.Enums;
using Domain.Models;

var database = new Database();

var studentRepo = new StudentRepository(database);
var courseRepo = new CourseRepository(database);
var groupRepo = new GroupRepository(database);
var mentorRepo = new MentorRepository(database);
var studentGroupRepo = new StudentGroupRepository(database);

while (true)
{
    Console.WriteLine("\n========== CRM ==========");
    Console.WriteLine("1. Students");
    Console.WriteLine("2. Courses");
    Console.WriteLine("3. Groups");
    Console.WriteLine("4. Mentors");
    Console.WriteLine("5. Student groups");
    Console.WriteLine("0. Exit");
    Console.Write("Choose: ");

    var choice = Console.ReadLine();

    if (choice == "1")
    {
        while (true)
        {
            Console.WriteLine("\n--- Students ---");
            Console.WriteLine("1. Add student");
            Console.WriteLine("2. Delete student");
            Console.WriteLine("3. Update student");
            Console.WriteLine("4. Get student by id");
            Console.WriteLine("5. Get all students");
            Console.WriteLine("6. Total students count");
            Console.WriteLine("7. Graduated students");
            Console.WriteLine("8. Students with groups");
            Console.WriteLine("9. Students without groups");
            Console.WriteLine("10. Dropped out students");
            Console.WriteLine("0. Back");
            Console.Write("Choose: ");

            var option = Console.ReadLine();

            if (option == "1")
            {
                var student = new Student();
                Console.Write("Full name: ");
                student.Fullname = Console.ReadLine()!;
                Console.Write("Email: ");
                student.Email = Console.ReadLine()!;
                Console.Write("Phone: ");
                student.Phone = Console.ReadLine()!;
                Console.Write("Enrollment date (yyyy-mm-dd): ");
                student.Enrollmentdate = DateTime.Parse(Console.ReadLine()!);
                await studentRepo.AddStudent(student);
            }
            else if (option == "2")
            {
                Console.Write("Student id: ");
                int id = int.Parse(Console.ReadLine()!);
                await studentRepo.DeleteStudent(id);
            }
            else if (option == "3")
            {
                var student = new Student();
                Console.Write("Student id: ");
                student.StudentId = int.Parse(Console.ReadLine()!);
                Console.Write("Full name: ");
                student.Fullname = Console.ReadLine()!;
                Console.Write("Email: ");
                student.Email = Console.ReadLine()!;
                Console.Write("Phone: ");
                student.Phone = Console.ReadLine()!;
                Console.Write("Enrollment date (yyyy-mm-dd): ");
                student.Enrollmentdate = DateTime.Parse(Console.ReadLine()!);
                await studentRepo.UpdateStudent(student);
            }
            else if (option == "4")
            {
                Console.Write("Student id: ");
                int id = int.Parse(Console.ReadLine()!);
                var student = await studentRepo.GetStudent(id);
                if (student != null)
                    Console.WriteLine($"{student.StudentId} | {student.Fullname} | {student.Email} | {student.Phone} | {student.Enrollmentdate}");
            }
            else if (option == "5")
            {
                var students = await studentRepo.GetStudents();
                foreach (var student in students)
                    Console.WriteLine($"{student.StudentId} | {student.Fullname} | {student.Email} | {student.Phone} | {student.Enrollmentdate}");
            }
            else if (option == "6")
            {
                Console.WriteLine("Total students: " + await studentRepo.GetTotalStudentsCount());
            }
            else if (option == "7")
            {
                var students = await studentRepo.GetGraduatedStudents();
                foreach (var student in students)
                    Console.WriteLine($"{student.StudentId} | {student.Fullname}");
            }
            else if (option == "8")
            {
                var students = await studentRepo.GetStudentsWithGroups();
                foreach (var student in students)
                    Console.WriteLine(student.Fullname);
            }
            else if (option == "9")
            {
                var students = await studentRepo.GetStudentsWithoutGroups();
                foreach (var student in students)
                    Console.WriteLine(student.Fullname);
            }
            else if (option == "10")
            {
                var students = await studentRepo.GetDroppedOutStudents();
                foreach (var student in students)
                    Console.WriteLine($"{student.StudentId} | {student.Fullname}");
            }
            else if (option == "0")
                break;
        }
    }
    else if (choice == "2")
    {
        while (true)
        {
            Console.WriteLine("\n--- Courses ---");
            Console.WriteLine("1. Add course");
            Console.WriteLine("2. Delete course");
            Console.WriteLine("3. Update course");
            Console.WriteLine("4. Get course by id");
            Console.WriteLine("5. Get all courses");
            Console.WriteLine("6. Total courses count");
            Console.WriteLine("7. Students per course");
            Console.WriteLine("8. Most popular course");
            Console.WriteLine("9. Least popular courses");
            Console.WriteLine("10. Top three courses");
            Console.WriteLine("0. Back");
            Console.Write("Choose: ");

            var option = Console.ReadLine();

            if (option == "1")
            {
                var course = new Course();
                Console.Write("Title: ");
                course.Title = Console.ReadLine()!;
                Console.Write("Description: ");
                course.Description = Console.ReadLine()!;
                Console.Write("Duration in weeks: ");
                course.Durationweek = DateTime.Parse(Console.ReadLine()!);
                await courseRepo.AddCourse(course);
            }
            else if (option == "2")
            {
                Console.Write("Course id: ");
                await courseRepo.DeleteCourse(int.Parse(Console.ReadLine()!));
            }
            else if (option == "3")
            {
                var course = new Course();
                Console.Write("Course id: ");
                course.CourseId = int.Parse(Console.ReadLine()!);
                Console.Write("Title: ");
                course.Title = Console.ReadLine()!;
                Console.Write("Description: ");
                course.Description = Console.ReadLine()!;
                Console.Write("Duration in weeks (date format as in your model): ");
                course.Durationweek = DateTime.Parse(Console.ReadLine()!);
                await courseRepo.UpdateCourse(course);
            }
            else if (option == "4")
            {
                Console.Write("Course id: ");
                var course = await courseRepo.GetCourse(int.Parse(Console.ReadLine()!));
                if (course != null)
                    Console.WriteLine($"{course.CourseId} | {course.Title} | {course.Description} | {course.Durationweek}");
            }
            else if (option == "5")
            {
                var courses = await courseRepo.GetCourses();
                foreach (var course in courses)
                    Console.WriteLine($"{course.CourseId} | {course.Title} | {course.Description} | {course.Durationweek}");
            }
            else if (option == "6")
            {
                Console.WriteLine("Total courses: " + await courseRepo.GetTotalCoursesCount());
            }
            else if (option == "7")
            {
                var courses = await courseRepo.GetStudentsPerCourse();
                foreach (var course in courses)
                    Console.WriteLine(course.Title);
            }
            else if (option == "8")
            {
                var courses = await courseRepo.GetMostPopularCourse();
                foreach (var course in courses)
                    Console.WriteLine(course.Title);
            }
            else if (option == "9")
            {
                var courses = await courseRepo.GetLeastPopularCourses();
                foreach (var course in courses)
                    Console.WriteLine(course.Title);
            }
            else if (option == "10")
            {
                var courses = await courseRepo.GetTopThreeCourses();
                foreach (var course in courses)
                    Console.WriteLine(course.Title);
            }
            else if (option == "0")
                break;
        }
    }
    else if (choice == "3")
    {
        while (true)
        {
            Console.WriteLine("\n--- Groups ---");
            Console.WriteLine("1. Add group");
            Console.WriteLine("2. Delete group");
            Console.WriteLine("3. Update group");
            Console.WriteLine("4. Get group by id");
            Console.WriteLine("5. Get all groups");
            Console.WriteLine("6. Total groups count");
            Console.WriteLine("7. All group start dates");
            Console.WriteLine("8. Students per group");
            Console.WriteLine("9. Empty groups");
            Console.WriteLine("0. Back");
            Console.Write("Choose: ");

            var option = Console.ReadLine();

            if (option == "1")
            {
                var group = new Group();
                Console.Write("Group name: ");
                group.GroupName = Console.ReadLine()!;
                Console.Write("Course id: ");
                group.CourseId = int.Parse(Console.ReadLine()!);
                Console.Write("Mentor id: ");
                group.MentorId = int.Parse(Console.ReadLine()!);
                Console.Write("Start date (yyyy-mm-dd): ");
                group.StartDate = DateTime.Parse(Console.ReadLine()!);
                Console.Write("End date (yyyy-mm-dd): ");
                group.EndDate = DateTime.Parse(Console.ReadLine()!);
                await groupRepo.AddGroup(group);
            }
            else if (option == "2")
            {
                Console.Write("Group id: ");
                await groupRepo.DeleteGroup(int.Parse(Console.ReadLine()!));
            }
            else if (option == "3")
            {
                var group = new Group();
                Console.Write("Group id: ");
                group.GroupId = int.Parse(Console.ReadLine()!);
                Console.Write("Group name: ");
                group.GroupName = Console.ReadLine()!;
                Console.Write("Course id: ");
                group.CourseId = int.Parse(Console.ReadLine()!);
                Console.Write("Mentor id: ");
                group.MentorId = int.Parse(Console.ReadLine()!);
                Console.Write("Start date (yyyy-mm-dd): ");
                group.StartDate = DateTime.Parse(Console.ReadLine()!);
                Console.Write("End date (yyyy-mm-dd): ");
                group.EndDate = DateTime.Parse(Console.ReadLine()!);
                await groupRepo.UpdateGroup(group);
            }
            else if (option == "4")
            {
                Console.Write("Group id: ");
                var group = await groupRepo.GetGroup(int.Parse(Console.ReadLine()!));
                if (group != null)
                    Console.WriteLine($"{group.GroupId} | {group.GroupName} | Course: {group.CourseId} | Mentor: {group.MentorId}");
            }
            else if (option == "5")
            {
                var groups = await groupRepo.GetGroups();
                foreach (var group in groups)
                    Console.WriteLine($"{group.GroupId} | {group.GroupName} | Course: {group.CourseId} | Mentor: {group.MentorId}");
            }
            else if (option == "6")
            {
                Console.WriteLine("Total groups: " + await groupRepo.GetTotalGroupsCount());
            }
            else if (option == "7")
            {
                var dates = await groupRepo.GetAllStartDates();
                foreach (var date in dates)
                    Console.WriteLine(date.ToShortDateString());
            }
            else if (option == "8")
            {
                var groups = await groupRepo.GetStudentsPerGroup();
                foreach (var group in groups)
                    Console.WriteLine(group.GroupName);
            }
            else if (option == "9")
            {
                var groups = await groupRepo.GetEmptyGroups();
                foreach (var group in groups)
                    Console.WriteLine($"{group.GroupId} | {group.GroupName}");
            }
            else if (option == "0")
                break;
        }
    }
    else if (choice == "4")
    {
        while (true)
        {
            Console.WriteLine("\n--- Mentors ---");
            Console.WriteLine("1. Add mentor");
            Console.WriteLine("2. Delete mentor");
            Console.WriteLine("3. Update mentor");
            Console.WriteLine("4. Get mentor by id");
            Console.WriteLine("5. Get all mentors");
            Console.WriteLine("6. Total mentors count");
            Console.WriteLine("7. Mentor with most students");
            Console.WriteLine("0. Back");
            Console.Write("Choose: ");

            var option = Console.ReadLine();

            if (option == "1")
            {
                var mentor = new Mentor();
                Console.Write("Full name: ");
                mentor.Fullname = Console.ReadLine()!;
                Console.Write("Email: ");
                mentor.Email = Console.ReadLine()!;
                Console.Write("Phone: ");
                mentor.Phone = Console.ReadLine()!;
                Console.Write("Specialization: ");
                mentor.Specialization = Console.ReadLine()!;
                await mentorRepo.AddMentor(mentor);
            }
            else if (option == "2")
            {
                Console.Write("Mentor id: ");
                await mentorRepo.DeleteMentor(int.Parse(Console.ReadLine()!));
            }
            else if (option == "3")
            {
                var mentor = new Mentor();
                Console.Write("Mentor id: ");
                mentor.MentorId = int.Parse(Console.ReadLine()!);
                Console.Write("Full name: ");
                mentor.Fullname = Console.ReadLine()!;
                Console.Write("Email: ");
                mentor.Email = Console.ReadLine()!;
                Console.Write("Phone: ");
                mentor.Phone = Console.ReadLine()!;
                Console.Write("Specialization: ");
                mentor.Specialization = Console.ReadLine()!;
                await mentorRepo.UpdateMentor(mentor);
            }
            else if (option == "4")
            {
                Console.Write("Mentor id: ");
                var mentor = await mentorRepo.GetMentor(int.Parse(Console.ReadLine()!));
                if (mentor != null)
                    Console.WriteLine($"{mentor.MentorId} | {mentor.Fullname} | {mentor.Email} | {mentor.Phone} | {mentor.Specialization}");
            }
            else if (option == "5")
            {
                var mentors = await mentorRepo.GetMentors();
                foreach (var mentor in mentors)
                    Console.WriteLine($"{mentor.MentorId} | {mentor.Fullname} | {mentor.Email} | {mentor.Phone} | {mentor.Specialization}");
            }
            else if (option == "6")
            {
                Console.WriteLine("Total mentors: " + await mentorRepo.GetTotalMentorsCount());
            }
            else if (option == "7")
            {
                var mentors = await mentorRepo.GetMentorWithMostStudents();
                foreach (var mentor in mentors)
                    Console.WriteLine(mentor.Fullname);
            }
            else if (option == "0")
                break;
        }
    }
    else if (choice == "5")
    {
        while (true)
        {
            Console.WriteLine("\n--- Student groups ---");
            Console.WriteLine("1. Add student to group");
            Console.WriteLine("2. Delete student group");
            Console.WriteLine("3. Update student group");
            Console.WriteLine("4. Get all student groups");
            Console.WriteLine("0. Back");
            Console.Write("Choose: ");

            var option = Console.ReadLine();

            if (option == "1")
            {
                var studentGroup = new StudentGroup();
                Console.Write("Student group id: ");
                studentGroup.StudentGroupId = int.Parse(Console.ReadLine()!);
                Console.Write("Student id: ");
                studentGroup.StudentId = int.Parse(Console.ReadLine()!);
                Console.Write("Group id: ");
                studentGroup.GroupId = int.Parse(Console.ReadLine()!);
                Console.WriteLine("Status: 0 - Active, 1 - Left, 2 - Finished");
                studentGroup.Status = (Status)int.Parse(Console.ReadLine()!);
                await studentGroupRepo.AddStudentGroup(studentGroup);
            }
            else if (option == "2")
            {
                Console.Write("Student group id: ");
                await studentGroupRepo.DeleteStudentGroup(int.Parse(Console.ReadLine()!));
            }
            else if (option == "3")
            {
                var studentGroup = new StudentGroup();
                Console.Write("Student group id: ");
                studentGroup.StudentGroupId = int.Parse(Console.ReadLine()!);
                Console.Write("Student id: ");
                studentGroup.StudentId = int.Parse(Console.ReadLine()!);
                Console.Write("Group id: ");
                studentGroup.GroupId = int.Parse(Console.ReadLine()!);
                Console.WriteLine("Status: 0 - Active, 1 - Left, 2 - Finished");
                studentGroup.Status = (Status)int.Parse(Console.ReadLine()!);
                await studentGroupRepo.UpdateStudentGroup(studentGroup);
            }
            else if (option == "4")
            {
                var studentGroups = await studentGroupRepo.GetStudentGroups();
                foreach (var studentGroup in studentGroups)
                    Console.WriteLine($"{studentGroup.StudentGroupId} | Student: {studentGroup.StudentId} | Group: {studentGroup.GroupId} | Status: {studentGroup.Status}");
            }
            else if (option == "0")
                break;
        }
    }
    else if (choice == "0")
    {
        break;
    }
}
