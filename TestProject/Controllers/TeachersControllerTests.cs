using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseAPI.Controllers;
using CourseAPI.Models;
using CourseAPI.Repositories;
using TestProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using CourseAPI.Data;

namespace TestProject.Controllers
{
    public class TeachersControllerTests
    {
        private TeachersController _controller;
        private Mock<CourseContext> _mockDbContext;
        private Mock<ITeacherRepository> _mockTeacherRepository;
        private List<Teacher> _teachersSource;
        private Mock<DbSet<Teacher>> _mockDbSet;

        [SetUp]
        public void Setup()
        {
            // Initialize data
            _teachersSource = TeachersTestCaseDataProvider.GetTeachersSource();

            // Mocking repository
            _mockTeacherRepository = new Mock<ITeacherRepository>();

            // Repository methods
            _mockTeacherRepository.Setup(repo => repo.GetTeachersAsync()).ReturnsAsync(_teachersSource);
            _mockTeacherRepository.Setup(repo => repo.GetTeacherAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => _teachersSource.FirstOrDefault(t => t.Id == id));
            _mockTeacherRepository.Setup(repo => repo.PostTeacherAsync(It.IsAny<Teacher>()))
                .ReturnsAsync((Teacher teacher) =>
                {
                    _teachersSource.Add(teacher);
                    return teacher;
                });
            _mockTeacherRepository.Setup(repo => repo.PutTeacherAsync(It.IsAny<int>(), It.IsAny<Teacher>()))
                .Callback<int, Teacher>((id, teacher) =>
                {
                    var existing = _teachersSource.FirstOrDefault(t => t.Id == id);
                    if (existing != null)
                    {
                        existing.Name = teacher.Name;
                        existing.Email = teacher.Email;
                    }
                });
            _mockTeacherRepository.Setup(repo => repo.DeleteTeacherAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => _teachersSource.Remove(_teachersSource.FirstOrDefault(t => t.Id == id)));

            _controller = new TeachersController(_mockTeacherRepository.Object);
        }

        //GET

        [Test]
        public async Task GetTeachersAsync_ReturnsOkResult_WithListOfTeachers()
        {
            var result = await _controller.GetTeachersAsync();
            
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);

            var teachers = okResult.Value as List<TeacherDto>;
            Assert.IsNotNull(teachers);
            Assert.AreEqual(_teachersSource.Count, teachers.Count);
        }

        //GET{id}

        [Test]
        public async Task GetTeacher_ReturnsOkResult_WithTeacher()
        {
            // Arrange
            int testTeacherId = _teachersSource.First().Id;

            // Act
            var result = await _controller.GetTeacher(testTeacherId);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);

            var teacher = okResult.Value as TeacherDto;
            Assert.IsNotNull(teacher);
            Assert.AreEqual(testTeacherId, teacher.Id);
        }

        [Test]
        public async Task GetTeacher_ReturnsNotFoundResult_WhenTeacherDoesNotExist()
        {
            // Act
            var result = await _controller.GetTeacher(-1); // Non-existing ID

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        // POST

        [Test]
        public async Task PostTeacher_ReturnsCreatedAtActionResult_WithNewTeacher()
        {
            // Arrange
            var newTeacher = new Teacher { Id = 4, Name = "New Teacher", Email = "newteacher@example.com" };

            // Act
            var result = await _controller.PostTeacher(newTeacher);

            // Assert
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            Assert.AreEqual(StatusCodes.Status201Created, createdAtActionResult.StatusCode);

            var createdTeacher = createdAtActionResult.Value as Teacher;
            Assert.IsNotNull(createdTeacher);
            Assert.AreEqual(newTeacher.Name, createdTeacher.Name);
            Assert.AreEqual(newTeacher.Email, createdTeacher.Email);
        }

        [Test]
        public async Task PostTeacher_ReturnsBadRequestResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var invalidTeacher = new Teacher { Name = "", Email = "invalidemail" }; // Invalid Name

            _controller.ModelState.AddModelError("Name", "Name required");

            // Act
            var result = await _controller.PostTeacher(invalidTeacher);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        // PUT

        [Test]
        public async Task PutTeacher_ReturnsNoContentResult_WhenUpdateIsSuccessful()
        {
            // Arrange
            int testTeacherId = _teachersSource.First().Id;
            var updatedTeacher = new Teacher { Id = testTeacherId, Name = "Updated Name", Email = "updatedemail@example.com" };

            // Act
            var result = await _controller.PutTeacher(testTeacherId, updatedTeacher);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        

        [Test]
        public async Task PutTeacher_ReturnsBadRequestResult_WhenIdsDoNotMatch()
        {
            // Arrange
            var updatedTeacher = new Teacher { Id = 999, Name = "Updated Name", Email = "updatedemail@example.com" }; // ID does not match

            // Act
            var result = await _controller.PutTeacher(1, updatedTeacher);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result);
        }
        //DELETE

        [Test]
        public async Task DeleteTeacher_ReturnsNoContentResult_WhenDeletionIsSuccessful()
        {
            // Arrange
            int testTeacherId = _teachersSource.First().Id;

            // Act
            var result = await _controller.DeleteTeacher(testTeacherId);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task DeleteTeacher_ReturnsNotFoundResult_WhenTeacherDoesNotExist()
        {
            // Act
            var result = await _controller.DeleteTeacher(-1);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

    }
}

