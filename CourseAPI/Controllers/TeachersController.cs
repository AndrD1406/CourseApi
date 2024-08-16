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
using Microsoft.Build.Evaluation;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeachersController(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        // GET: api/Teachers
        [HttpGet]
        public async Task<IActionResult> GetTeachersAsync()
        {
            var teachers = await _teacherRepository.GetTeachersAsync();

            var teacherDtos = teachers.Select(t => new TeacherDto
            {
                Id = t.Id,
                Name = t.Name,
                Email = t.Email
            }).ToList();

            return Ok(teacherDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherDto>> GetTeacher(int id)
        {
            var teacher = await _teacherRepository.GetTeacherAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }

            // Map the teacher to the DTO
            var teacherDTO = new TeacherDto
            {
                Id = teacher.Id,
                Name = teacher.Name,
                Email = teacher.Email,
                // Additional mapping logic if needed
            };

            return Ok(teacherDTO);
        }

        // PUT: api/Teachers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(int id, Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return BadRequest();
            }

            try
            {
                await _teacherRepository.PutTeacherAsync(id, teacher);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Teachers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher(Teacher teacher)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                // Call repository method to add the teacher
                var resultTeacher = await _teacherRepository.PostTeacherAsync(teacher);

                // Return the created result
                return CreatedAtAction("GetTeacher", new { id = resultTeacher.Id }, resultTeacher);
            }
            catch (InvalidOperationException ex)
            {
                // Handle any custom validation exceptions or other errors
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/Teachers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            if (await _teacherRepository.DeleteTeacherAsync(id))
            {
                return NoContent();
            }

            return NotFound();
        }

        private bool TeacherExists(int id)
        {
            return _teacherRepository.TeacherExistsRep(id);
        }
    }
}
