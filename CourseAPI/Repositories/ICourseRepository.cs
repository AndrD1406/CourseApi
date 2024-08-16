using CourseAPI.Models;

namespace CourseAPI.Repositories
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetCoursesAsync();
        Task<Course> GetCourseAsync(int id);
        Task PutCourseAsync(int id,  Course course);
        Task PostCourseAsync(Course course);
        Task<bool> DeleteCourseAsync(int id);
        bool CourseExistsRep(int id);
        Task<Course> CreateCourseAsync(CreateCourseDto courseDto);
        Task<bool> TeacherExistsAsync(int teacherId);
    }
}
