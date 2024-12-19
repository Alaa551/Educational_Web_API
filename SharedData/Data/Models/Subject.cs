namespace SharedData.Data.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string? SubjectName { get; set; }

        //relationships
        public ICollection<Lesson> Lessons { get; set;}= new List<Lesson>();
        //public ICollection<StudentProgress> studentProgresses { get; set; } = new List<StudentProgress>();

    }
}
