namespace SharedData.Data.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Score { get; set; }

        //relationships
        public string AccountId { get; set; }
        public Account Account { get; set; } = null!;

        public ICollection<StudentProgress> studentProgresses { get; set; } = new List<StudentProgress>();  

    }

}
