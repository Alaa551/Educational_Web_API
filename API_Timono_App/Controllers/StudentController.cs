using API_Timono_App.Repository.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace API_Timono_App.Controllers
{
    [Route("XR_Timono_App/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet("{studentId:int}")]
        public async Task<IActionResult> GetStudentById(int studentId)
        {
            try
            {
                var student = await _studentRepository.GetStudentById(studentId);
                if (student == null)
                {
                    return NotFound(new { message = "This student id not found please try again." });
                }
                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching specific student.", details = ex.Message });

            }


        }


        [HttpGet("score/{studentId:int}")]
        public async Task<IActionResult> GetStudentScore(int studentId)
        {
            try
            {
                var score = await _studentRepository.GetStudentScore(studentId);
                if (score == -1)
                {
                    return NotFound(new { message = "This student id not found please try again." });
                }
                return Ok(score);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching specific student's score", details = ex.Message });

            }


        }
        [HttpGet("updateScore/{studentId:int}/{score:int}")]


        public async Task<IActionResult> UpdateStudentScore(int studentId, int score)
        {
            try
            {
                if (score < 0)
                    return BadRequest("Score must be greater than 0");

                var student = await _studentRepository.UpdateStudentScore(studentId, score);
                if (student == null)
                    return NotFound(new { message = "This student id not found please try again." });

                return Ok("Student Score Updated Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating specific student's score", details = ex.Message });

            }

        }


    }
}
