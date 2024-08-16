using System.ComponentModel.DataAnnotations;

namespace CourseAPI.Models
{
    public class TeacherDto
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }

}
