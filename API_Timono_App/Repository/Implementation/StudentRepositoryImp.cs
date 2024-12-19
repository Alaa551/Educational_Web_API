using API_Timono_App.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using SharedData.Data;
using SharedData.Data.Models;
using SharedData.DTO;

namespace API_Timono_App.Repository.Implementation
{
    public class StudentRepositoryImp : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepositoryImp(AppDbContext context)
        {
            _context = context;
        }

        public async Task<StudentDTO> GetStudentById(int studentId) =>

           await _context.Students.Where(s => s.Id == studentId)
                                  .Select(s => new StudentDTO
                                  {
                                      Id = s.Id,
                                      Name = s.Name,
                                      Score = s.Score,
                                      AccountId = s.AccountId
                                  })
                                  .FirstOrDefaultAsync();

        public async Task<int> GetStudentScore(int studentId)
        {
            var student = await GetStudentById(studentId);
            if (student == null)
                return -1;

            return student.Score;

        }

        public async Task<Student> UpdateStudentScore(int studentId, int score)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == studentId);
            if (student == null)
                return null;

            student.Score = score;
            await _context.SaveChangesAsync();
            return student;

        }
    }
}
