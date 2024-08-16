namespace CourseAPI.Models
{
    public class CreateCourseDto
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int TeacherId { get; set; }
        public List<int> StudentIds { get; set; }
    }

}
