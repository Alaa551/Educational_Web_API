using API_Timono_App.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using SharedData.Data;
using SharedData.DTO;

namespace API_Timono_App.Repository.Implementation
{
    public class SubjectRepositoryImp : ISubjectRepository
    {
        private readonly AppDbContext _context;

        public SubjectRepositoryImp(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SubjectDTO>> GetAllSubjects()
        {
            return await _context.Subjects.Select(s => new SubjectDTO { Name = s.SubjectName, Id = s.Id }).ToListAsync();
        }

        public async Task<IEnumerable<LessonDTO>> GetLessonOfSubjects(int subjectId)
        {
            if(! await IsSubjectExists(subjectId))
            {
                return null;
            }
            return await _context.Subjects.Where(s => s.Id==subjectId)
                .SelectMany(s => s.Lessons)
                .Select(lesson => new LessonDTO {
                    Id = lesson.Id,
                    Name= lesson.LessonName,
                    Content= lesson.Content}).ToListAsync();
        }

       public async Task<bool> IsSubjectExists(int subjectId)
        {
            return await _context.Subjects.AnyAsync(s => s.Id==subjectId);
        }
    }
}
