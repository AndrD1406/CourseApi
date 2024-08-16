using CourseAPI.Data;
using CourseAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseAPI.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly CourseContext _context;
        public TeacherRepository(CourseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Teacher>> GetTeachersAsync()
        {
            return await _context.Teachers.Include(s => s.Courses).ToListAsync();
        }

        public async Task<Teacher> GetTeacherAsync(int id)
        {
            return await _context.Teachers
            .Include(t => t.Courses)
            .FirstOrDefaultAsync(t => t.Id == id);
        }
        public async Task PutTeacherAsync(int id, Teacher teacher)
        {
            _context.Entry(teacher).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<Teacher> PostTeacherAsync(Teacher teacher)
        {
            var existingTeacher = await _context.Teachers
            .FirstOrDefaultAsync(t => t.Email == teacher.Email);

            if (existingTeacher != null)
            {
                throw new InvalidOperationException("A teacher with the same email already exists.");
            }

            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
            return teacher;
        }
        public async Task<bool> DeleteTeacherAsync(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return false;
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            return true;
        }
        public bool TeacherExistsRep(int id)
        {
            return _context.Teachers.Any(e => e.Id == id);
        }
    }
}
