namespace SharedData.Data.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string? LessonName { get; set;}
        public string? Content { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; } = null!;

        public ICollection<StudentProgress> studentProgresses { get; set; } = new List<StudentProgress>();


    }
}
