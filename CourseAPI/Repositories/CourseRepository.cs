using CourseAPI.Data;
using CourseAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseAPI.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CourseContext _context;
        public CourseRepository(CourseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Course>> GetCoursesAsync()
        {
            return await _context.Courses.ToListAsync();
        }  
        public async Task<Course> GetCourseAsync(int id)
        {
            return await _context.Courses.FindAsync(id);
        }
        public async Task PutCourseAsync(int id, Course course)
        {
            _context.Entry(course).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task PostCourseAsync(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteCourseAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return false;
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }        
        public bool CourseExistsRep(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }

        public async Task<bool> TeacherExistsAsync(int teacherId)
        {
            return await _context.Teachers.AnyAsync(t => t.Id == teacherId);
        }
        public async Task<Course> CreateCourseAsync(CreateCourseDto courseDto)
        {
            var course = new Course
            {
                Name = courseDto.Name,
                Price = courseDto.Price,
                TeacherId = courseDto.TeacherId
            };

            if (courseDto.StudentIds != null)
            {
                var students = await _context.Students
                    .Where(s => courseDto.StudentIds.Contains(s.Id))
                    .ToListAsync();

                course.Students = students;
            }

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return course;
        }
    }
}
