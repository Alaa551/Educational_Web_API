namespace SharedData.Data.Models
{
    public class StudentProgress
    {
        //public int SubjectId { get; set; }
        //public Subject Subject { get; set; } = null!;

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; } = null!;

        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public int Progress { get; set; }

    }
}
