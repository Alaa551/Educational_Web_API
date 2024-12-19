using SharedData.Data.Models;
using SharedData.DTO;

namespace API_Timono_App.Mapping
{
    public static class ProgressMapping
    {
        public static StudentProgress ToStudentProgress(this ProgressDTO progressDto, int studentId)
        {
            var studentProgress = new StudentProgress
            {
                StudentId = studentId,
                LessonId = progressDto.LessonId,
                Progress = progressDto.progress

            };
            return studentProgress;
        }

        public static ProgressDTO ToProgressDTO(this StudentProgress studentProgress)
        {
            var studentProgressDto = new ProgressDTO
            {
                LessonId = studentProgress.LessonId,
                progress = studentProgress.Progress
            };
            return studentProgressDto;
        }
    }
}
