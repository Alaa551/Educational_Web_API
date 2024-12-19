using SharedData.Data.Models;
using SharedData.DTO;

namespace API_Timono_App.Repository.Abstraction
{
    public interface IStudentRepository
    {
        Task<StudentDTO> GetStudentById(int studentId);
        Task<int> GetStudentScore(int studentId);
        Task<Student> UpdateStudentScore(int studentId, int score);

    }
}
