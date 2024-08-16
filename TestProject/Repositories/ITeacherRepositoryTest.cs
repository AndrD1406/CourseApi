using CourseAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Repositories
{
    public interface ITestTeacherRepository
    {
        Task<IEnumerable<Teacher>> GetTeachersAsync();
        Task<Teacher> GetTeacherAsync(int id);
        Task<Teacher> PostTeacherAsync(Teacher teacher);
        Task PutTeacherAsync(int id, Teacher teacher);
        Task<bool> DeleteTeacherAsync(int id);
    }
}
