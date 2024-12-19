using SharedData.DTO;

namespace API_Timono_App.Repository.Abstraction
{
    public interface ISubjectRepository
    {
        Task<IEnumerable<SubjectDTO>> GetAllSubjects();
        Task<IEnumerable<LessonDTO>> GetLessonOfSubjects(int subjectId);
        Task<bool> IsSubjectExists(int subjectId);

    }
}
