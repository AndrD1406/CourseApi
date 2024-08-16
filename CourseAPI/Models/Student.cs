using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CourseAPI.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name required.")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [JsonIgnore]
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }

}
