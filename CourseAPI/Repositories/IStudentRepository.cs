using CourseAPI.Models;

namespace CourseAPI.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetStudentsAsync();
        Task<Student> GetStudentAsync(int id);
        Task PutStudentAsync(int id, Student student);
        Task<Student> PostStudentAsync(Student student);
        Task<bool> DeleteStudentAsync(int id);
        bool StudentExistsRep(int id);
    }
}
