using CourseAPI.Controllers;
using CourseAPI.Data;
using CourseAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseAPI.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly CourseContext _context;
        public StudentRepository(CourseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            return await _context.Students.Include(s => s.Courses).ToListAsync();
        }
        public async Task<Student> GetStudentAsync(int id)
        {
            return await _context.Students
            .Include(s => s.Courses)
            .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task PutStudentAsync(int id, Student student)
        {

            var existingStudent = await _context.Students.FindAsync(id);
            if (existingStudent == null)
            {
                throw new KeyNotFoundException("Student not found.");
            }

            _context.Entry(student).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Students.Any(e => e.Id == id))
                {
                    throw new KeyNotFoundException("Student not found.");
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating the student.", ex);
            }
        }
        public async Task<Student> PostStudentAsync(Student student)
        {
            var existingStudent = await _context.Students
            .FirstOrDefaultAsync(s => s.Email == student.Email);

            if (existingStudent != null)
            {
                throw new InvalidOperationException("A student with the same email already exists.");
            }

            _context.Students.Add(student);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("An error occurred while saving the student. Please try again later.", ex);
            }
            return student;
        }
        public async Task<bool> DeleteStudentAsync(int id)
        {

            var existingStudent = await _context.Students
            .FirstOrDefaultAsync(s => s.Id == id);

            if (existingStudent != null)
            {
                throw new InvalidOperationException("A student with the same Id already exists.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return false;
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return true;
        }
        public bool StudentExistsRep(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }

}
