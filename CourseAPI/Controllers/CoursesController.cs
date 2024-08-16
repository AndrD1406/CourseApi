using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourseAPI.Data;
using CourseAPI.Models;
using CourseAPI.Repositories;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _coursesRepository;
        public CoursesController(ICourseRepository courseRepository)
        {
            _coursesRepository = courseRepository;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            var courses = await _coursesRepository.GetCoursesAsync();
            return new OkObjectResult(courses);
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _coursesRepository.GetCourseAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (! await _coursesRepository.TeacherExistsAsync(course.TeacherId))
                {
                    return NotFound($"Teacher with ID {course.TeacherId} not found.");
                }

                // Оновлення курсу
                await _coursesRepository.PutCourseAsync(id, course);
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_coursesRepository.CourseExistsRep(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostCourse(CreateCourseDto courseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Перевіряємо, чи існує викладач через репозиторій
            var teacherExists = await _coursesRepository.TeacherExistsAsync(courseDto.TeacherId);
            if (!teacherExists)
            {
                return NotFound($"Teacher with ID {courseDto.TeacherId} not found.");
            }

            var course = await _coursesRepository.CreateCourseAsync(courseDto);

            return CreatedAtAction("GetCourse", new { id = course.Id }, course);
        }




        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            if (await _coursesRepository.DeleteCourseAsync(id))
            {
                return NoContent();
            }   

            return NotFound();
        }

        private bool CourseExists(int id)
        {
            return _coursesRepository.CourseExistsRep(id);
        }
    }
}
