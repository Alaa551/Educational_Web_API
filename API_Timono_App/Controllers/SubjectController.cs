using API_Timono_App.Repository.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace API_Timono_App.Controllers
{
    [Route("XR_Timono_App/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectController(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        [HttpGet("lessons/{subjectId:int}")]
        public async Task<IActionResult> GetLessonsOfSubject(int subjectId)
        {
            try
            {
                var lessons = await _subjectRepository.GetLessonOfSubjects(subjectId);
                if (lessons == null)
                {
                    return NotFound(new { message = "This subject id not found please try again." });
                }
                return Ok(lessons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching Lessons.", details = ex.Message });

            }


        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllSubjects()
        {
            try
            {
                var subjects = await _subjectRepository.GetAllSubjects();
                return Ok(subjects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching Subjects.", details = ex.Message });

            }

        }
    }
}
