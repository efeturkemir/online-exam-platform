using examApi.Models;
using examApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;


namespace examApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly IExamService _examService;

        public ExamsController(IExamService examService)
        {
            _examService = examService;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Exam>>> GetExams()
        {
            var exams = await _examService.GetAllExamsAsync();
            return Ok(exams);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> AddExam(ExamRequest exam)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(new { message = "Unauthorized access." });
            }


            var newExam = new Exam
            {
                examName = exam.examName,
                examType = exam.examType,
                questionCount = exam.questionCount,
                participants = new string[0],
                questions = new Question[0]


            };

            await _examService.AddExamAsync(newExam);

            return Ok(new {message = "Exam added succesfully!"});
        }

        [HttpDelete("{examId}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteExam(string examId)
        {
            try
            {
                await _examService.DeleteExamAsync(examId);
                return Ok(new { message = "Exam deleted succesfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});

            }
            
        }

        [HttpPost("{examId}/questions")]
        [Authorize(Policy = "AdminPolicy")]

        public async Task<IActionResult> AddQuestion(string examId, QuestionRequest question)
        {
            try
            {
                var newQuestion = new Question
                {
                    questionText = question.questionText,
                    answerIndex = question.answerIndex,
                    options = question.options,
                };
                await _examService.AddQuestionsToExamAsync(examId, newQuestion);
                return Ok(new { message = "Questions added to the exam successfully." });
            }
            catch (ArgumentException argEx)
            {
                // Handle argument-related exceptions
                return BadRequest(new { message = argEx.Message });
            }
            catch (InvalidOperationException invOpEx)
            {
                // Handle invalid operation exceptions
                return StatusCode(409, new { message = invOpEx.Message });
            }
            catch (Exception ex)
            {
                // Handle any other exceptions
                return StatusCode(500, new { message = $"An error occurred while adding questions to the exam: {ex.Message}" });
            }
        }

        [HttpPatch("{examId}")]
        [Authorize(Policy = "AdminPolicy")]

        public async Task<IActionResult> AddUserToExam(string examId, string userId)
        {
            try
            {
                
                await _examService.AddUserToExamAsync(examId, userId);
                return Ok("User added to exam successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding user to the exam: {ex.Message}");
            }
        }
    }
}
