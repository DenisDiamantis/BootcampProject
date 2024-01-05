using FinalProject.Back.Contexts;
using FinalProject.Data.Dtos.ExamDtos;
using FinalProject.Data.Entities.Exam;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly CertificationDbContext _context;

        public ExamController(CertificationDbContext context)
        {
            _context = context;
        }

        // endpoint to get all the questions for exam
        [HttpGet("questions")]
        public async Task<ActionResult<IEnumerable<ExamQuestionDto>>> GetExamQuestions()
        {
            var result = await _context.ExamQuestions.Include(x => x.Answers)
                .Select(x => ExamQuestionDto.FromEntity(x))
                .ToListAsync();
            await _context.SaveChangesAsync();
            return Ok(result);
        }

        // endpoint to add question
        [HttpPost("questions")]
        public async Task<ActionResult<ExamQuestionDto>> AddExamQuestion(ExamQuestionDto examQuestionDto)
        {
            var result = await _context.ExamQuestions.AddAsync(ExamQuestionDto.ToEntity(examQuestionDto));
            await _context.SaveChangesAsync();
            return Ok(ExamQuestionDto.FromEntity(result.Entity));
        }



        //list of candidates with their exam results
        [HttpGet("results")]
        public async Task<ActionResult<IEnumerable<ExamAttemptDto>>> GetExamResults()
        {
            var result = await _context.ExamAttempts.Include(x => x.CandidateAnswers)
                //.ThenInclude(x => x.Id)
                .Select(x => ExamAttemptDto.FromEntity(x))
                .ToListAsync();
            await _context.SaveChangesAsync();
            return Ok(result);
        }



        // endpoint for a candidate to submit their answers
        [HttpPost("submit")]
        public async Task<ActionResult<ExamSubmissionResultDto>> SubmitExamAttempt(ExamAttemptDto examAttemptDto)
        {

            if (examAttemptDto.CandidateAnswers == null || !examAttemptDto.CandidateAnswers.Any())
            {
                return BadRequest("No answers submitted.");
            }

            var correctAnswers = await _context.ExamAnswers
         .Where(answer => answer.IsCorrect)
         .ToListAsync();

            int score = 0;
            var candidateAnswerEntities = new List<CandidateAnswer>();
            foreach (var userAnswer in examAttemptDto.CandidateAnswers)
            {
                if (correctAnswers.Any(ca => ca.ExamQuestionId == userAnswer.ExamQuestionId && ca.Id == userAnswer.ExamAnswerId && userAnswer.IsSelected))
                {
                    score++;
                }

                var candidateAnswerEntity = new CandidateAnswer
                {
                    ExamAttemptId = examAttemptDto.Id, // Assuming this is set correctly
                    ExamQuestionId = userAnswer.ExamQuestionId,
                    ExamAnswerId = userAnswer.ExamAnswerId,
                    IsSelected = userAnswer.IsSelected
                };
                candidateAnswerEntities.Add(candidateAnswerEntity);
            }

            // Each question is worth 25%, so multiply by 25 to get the total score
            var percentageScore = score * 25;

            // Determine pass or fail
            bool passed = percentageScore >= 75;

            // Create and save the exam attempt
            var examAttempt = new ExamAttempt
            {
                CandidateId = examAttemptDto.CandidateId,
                AttemptDate = DateTime.Now,
                Score = percentageScore,
                Passed = passed,
                CertificateId = examAttemptDto.CertificateId,
                CandidateAnswers = candidateAnswerEntities
            };
            _context.ExamAttempts.Add(examAttempt);
            await _context.SaveChangesAsync();

            // Create a result DTO to return
            var resultDto = new ExamSubmissionResultDto
            {
                Score = percentageScore,
                Passed = passed
            };

            return Ok(resultDto);
        }

        // retrieve exam results for a candidate
        [HttpGet("results/{candidateId}")]
        public async Task<ActionResult<IEnumerable<ExamAttemptDto>>> GetExamResults(int candidateId)
        {
            var result = await _context.ExamAttempts.Include(x => x.CandidateAnswers)
                .ThenInclude(x => x.ExamAnswerId)
                .Where(x => x.CandidateId == candidateId)
                .Select(x => ExamAttemptDto.FromEntity(x))
                .ToListAsync();
            await _context.SaveChangesAsync();
            return Ok(result);
        }

        //retrieve a specific exam result for a candidate
        [HttpGet("results/{candidateId}/{attemptId}")]
        public async Task<ActionResult<ExamAttemptDto>> GetExamResult(int candidateId, int attemptId)
        {
            var result = await _context.ExamAttempts.Include(x => x.CandidateAnswers)
                .ThenInclude(x => x.ExamAnswerId)
                .FirstOrDefaultAsync(x => x.CandidateId == candidateId && x.Id == attemptId);

            if (result == null)
            {
                return NotFound();
            }
            await _context.SaveChangesAsync();
            return Ok(ExamAttemptDto.FromEntity(result));
        }

        //delete an exam question
        [HttpDelete("questions/{id}")]
        public async Task<ActionResult<ExamQuestionDto>> DeleteExamQuestion(int id)
        {
            var examQuestion = await _context.ExamQuestions.FindAsync(id);
            if (examQuestion == null)
            {
                return NotFound();
            }

            _context.ExamQuestions.Remove(examQuestion);
            await _context.SaveChangesAsync();

            return Ok(ExamQuestionDto.FromEntity(examQuestion));
        }

        //update an exam question
        [HttpPut("questions/{id}")]
        public async Task<ActionResult<ExamQuestionDto>> UpdateExamQuestion(int id, ExamQuestionDto examQuestionDto)
        {
        

            var examQuestion = await _context.ExamQuestions
                                    .Include(q => q.Answers)
                                    .FirstOrDefaultAsync(q => q.Id == id);

            if (examQuestion == null)
            {
                return NotFound();
            }

            examQuestion.Text = examQuestionDto.Text;

            // Assuming all updated answers are marked as correct for now
            var updatedAnswers = examQuestionDto.Answers.Select(ExamAnswerDto.ToEntity).ToList();
            foreach (var answer in examQuestion.Answers)
            {
                var updatedAnswer = updatedAnswers.FirstOrDefault(a => a.Id == answer.Id);
                if (updatedAnswer != null)
                {
                    answer.Text = updatedAnswer.Text;
                    // Not updating IsCorrect here
                }
            }

            // Add new answers
            foreach (var newAnswer in updatedAnswers.Where(a => a.Id == 0))
            {
                examQuestion.Answers.Add(newAnswer);
            }

            await _context.SaveChangesAsync();

            return Ok(ExamQuestionDto.FromEntity(examQuestion));
        }



    }
}
