using API_Timono_App.Repository.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace API_Timono_App.Controllers
{
    [Route("XR_Timono_App/[controller]")]
    [ApiController]
    public class ProgressController : ControllerBase
    {

        private readonly IProgressRepository _progressRepository;
        public ProgressController(IProgressRepository progressRepository)
        {
            _progressRepository = progressRepository;
        }


        [HttpGet("SpecificProgress/{studentId:int}/{SubjectId:int}")]
        public async Task<IActionResult> GetSpecificProgress(int studentId, int SubjectId)
        {
            try
            {
                var progress = await _progressRepository.GetSpecificProgress(SubjectId, studentId);
                if (progress == null)
                {
                    return NotFound(new { message = "This progress not found please try again." });
                }
                return Ok(progress);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching specific student's progress", details = ex.Message });

            }

        }


        [HttpPost("AddProgress")]
        public async Task<IActionResult> UpdateSpecificProgress(int studentId, int lessonId, int progress)
        {
            try
            {
                // Input validation
                if (lessonId <= 0 || studentId <= 0 || progress < 0 || progress > 100)
                {
                    return BadRequest(new { message = "Invalid input. Lesson ID, Student ID, and Progress must be valid positive values." });
                }

                var studentProgress = await _progressRepository.UpdateSpecificProgress(lessonId, studentId, progress);
                return Ok(studentProgress);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating specific student's progress", details = ex.Message });
            }

        }
    }
}
