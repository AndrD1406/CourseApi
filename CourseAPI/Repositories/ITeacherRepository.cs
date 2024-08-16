using CourseAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseAPI.Repositories
{
    public interface ITeacherRepository
    {
        Task<IEnumerable<Teacher>> GetTeachersAsync();
        Task<Teacher> GetTeacherAsync(int id);
        Task PutTeacherAsync(int id, Teacher teacher);
        Task<Teacher> PostTeacherAsync(Teacher teacher);
        Task<bool> DeleteTeacherAsync(int id);
        bool TeacherExistsRep(int id);
    }
}
