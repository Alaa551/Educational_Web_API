using API_Timono_App.Mapping;
using API_Timono_App.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using SharedData.Data;
using SharedData.Data.Models;
using SharedData.DTO;

namespace API_Timono_App.Repository.Implementation
{
    public class ProgressRepositoryImp : IProgressRepository
    {
        private readonly AppDbContext _context;




        public ProgressRepositoryImp(AppDbContext context)
        {
            _context = context;
        }

        private async Task<StudentProgress> IsThereProgressForTheSameSubject(int studentId, int lessonId)
        {
            var lesson = await getLesson(lessonId);
            var subject = await _context.Progresses.Where(p => p.LessonId != lessonId && p.Lesson.SubjectId == lesson.SubjectId
            && p.StudentId == studentId
            ).FirstOrDefaultAsync();
            return subject;

        }
        private async Task<StudentProgress> AddProgress(int studentId, int lessonId, int progress)
        {
            var newStudentProgress = new StudentProgress
            {
                LessonId = lessonId,
                StudentId = studentId,
                Progress = progress
            };
            await _context.Progresses.AddAsync(newStudentProgress);
            await _context.SaveChangesAsync();
            return newStudentProgress;
        }

        private async Task DeleteProgress(StudentProgress studetnProgress)
        {
            _context.Progresses.Remove(studetnProgress);
            await _context.SaveChangesAsync();
        }
        private async Task<StudentProgress> IsProgressExists(int studentId, int lessonId) =>
              await _context.Progresses.FirstOrDefaultAsync(p => p.StudentId == studentId && p.LessonId == lessonId);


        public async Task<ProgressDTO> GetSpecificProgress(int SubjectId, int StudentId)
        {
            var studentProgress = await _context.Progresses.Where(p => p.StudentId == StudentId && p.Lesson.SubjectId == SubjectId)
                .Select(p => new ProgressDTO
                {
                    StudentName = p.Student.Name,
                    SubjectName = p.Lesson.Subject.SubjectName,
                    LessonName = p.Lesson.LessonName,
                    LessonId = p.LessonId,
                    progress = p.Progress
                }).FirstOrDefaultAsync();

            return studentProgress;

        }

        public async Task<ProgressDTO> UpdateSpecificProgress(int lessonId, int studentId, int progress)
        {
            //check lesson and studdent are found first
            if (await getLesson(lessonId) == null)
            {
                throw new ArgumentException($"Lesson with ID {lessonId} does not exist.");
            }

            if (await getStudent(studentId) == null)
            {
                throw new ArgumentException($"Student with ID {studentId} does not exist.");
            }


            // Check for progress on the same subject and update it if exist
            var existingSubjectProgress = await IsThereProgressForTheSameSubject(studentId, lessonId);
            if (existingSubjectProgress != null)
            {
                await DeleteProgress(existingSubjectProgress);
                var newProgress = await AddProgress(studentId, lessonId, progress);
                return newProgress.ToProgressDTO();
            }

            // check if there is already progress for this student and lesson in db
            var studentProgress = await IsProgressExists(studentId, lessonId);
            if (studentProgress == null)
            {
                var newAddedProgress = await AddProgress(studentId, lessonId, progress);
                return newAddedProgress.ToProgressDTO();
            }


            studentProgress.Progress = progress;
            await _context.SaveChangesAsync();
            return studentProgress.ToProgressDTO();
        }

        private async Task<Student> getStudent(int studentId) =>
             await _context.Students.Where(s => s.Id == studentId).FirstOrDefaultAsync();

        private async Task<Lesson> getLesson(int lessonId) =>
            await _context.Lessons.Where(l => l.Id == lessonId).FirstOrDefaultAsync();


    }
}
