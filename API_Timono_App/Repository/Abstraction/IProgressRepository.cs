using SharedData.Data.Models;
using SharedData.DTO;

namespace API_Timono_App.Repository.Abstraction
{
    public interface IProgressRepository
    {
        Task<ProgressDTO> GetSpecificProgress(int SubjectId, int studentId);
        Task<ProgressDTO> UpdateSpecificProgress(int lessonId, int studentId, int progress);

    }
}