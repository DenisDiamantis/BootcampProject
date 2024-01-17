using FinalProject.Back.Contexts;
using FinalProject.Data.Dtos.ExamDtos;
using FinalProject.Data.Entities.Exam;
using Microsoft.AspNetCore.Authorization;
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

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ExamTemplateDto>>> GetExamTemplates()
		{
			var result = await _context.ExamTemplate.Select(x => ExamTemplateDto.FromEntity(x)).ToListAsync();
			return Ok(result);
		}

		// endpoint to get all the questions for exam
		[HttpGet("GetQuestions/{examTemplateId}")]
		public async Task<ActionResult<IEnumerable<AnswerDto>>> GetExamTemplateQuestions(int examTemplateId)
		{
			var result = await _context.QuestionTemplate.Where(x => x.ExamTemplateId == examTemplateId)
				.Select(q => new { q.QuestionText, q.AnswerA, q.AnswerB, q.AnswerC, q.AnswerD, q.CorrectAnwser })
				.ToListAsync();
			return Ok(result);
		}
		[HttpGet("Marker/{markerId}")]
		[Authorize(Roles = "admin")]
		public async Task<ActionResult<IEnumerable<AnswerDto>>> GetMarkerExams(int markerId)
		{
			var result = await _context.Exam.Where(x => x.MarkerId == markerId && !x.Marked)
				.ToListAsync();
			return Ok(result);
		}

		//get exam questions
		[HttpGet("Exam/{examId}")]
		public async Task<ActionResult<IEnumerable<QuestionTemplateDto>>> GetExamQuestions(int examId)
		{
			var exam = await _context.Exam.FirstOrDefaultAsync(x => x.Id == examId);
			var result = await _context.QuestionTemplate.Where(x => x.ExamTemplateId == exam.ExamTemplateId)
				.Select(x => QuestionTemplateDto.FromEntity(x))
				.ToListAsync();

			return Ok(result);
		}

		//get exam for evaluation
		[HttpGet("Evaluation/{examId}")]
		public async Task<ActionResult<ExamDto>> GetExam(int examId)
		{
			var exam = await _context.Exam.FirstOrDefaultAsync(x => x.Id == examId);
			return Ok(ExamDto.FromEntity(exam));
		}


		[HttpGet("Exams")]
		public async Task<ActionResult<IEnumerable<ExamDto>>> GetAllExams()
		{

			var result = await _context.Exam
				.Select(x => ExamDto.FromEntity(x))
				.ToListAsync();

			return Ok(result);
		}


		// endpoint to assign Marker
		[HttpPost("AddMarker")]
		[Authorize(Roles = "admin")]
		public async Task<ActionResult<ExamDto>> CreateExamTemplate(ExamDto exam)
		{
			var result = await _context.Exam.FirstOrDefaultAsync(x => x.Id == exam.Id);
			result.MarkerId = exam.MarkerId;
			await _context.SaveChangesAsync();
			return Ok(ExamDto.FromEntity(result));
		}
		// endpoint to create examTemplate
		[HttpPost]
		[Authorize(Roles = "admin")]
		public async Task<ActionResult<ExamTemplateDto>> CreateExamTemplate(ExamTemplateDto examTemplate)
		{
			var result = await _context.ExamTemplate.AddAsync(ExamTemplate.ToEntity(examTemplate));
			await _context.SaveChangesAsync();
			return Ok(ExamTemplateDto.FromEntity(result.Entity));
		}

		//save answers
		[HttpPost("saveAnswers")]
		public async Task<ActionResult<AnswerDto>> SaveAnswers(AnswerDto answerDto)
		{

			var result = await _context.Answer.AddAsync(Answer.ToEntity(answerDto));
			await _context.SaveChangesAsync();
			return Ok(AnswerDto.FromEntity(result.Entity));
		}

		//get results

		[HttpGet("ExamResults/{examId}")]
		public async Task<ActionResult<ExamDto>> SaveAnswers(int examId)
		{
			var exam = await _context.Exam.FirstOrDefaultAsync(ex => ex.Id == examId);
			if (exam.CandidateScore < 0)
			{

				int score = 0;
				var correctAnswers = await _context.QuestionTemplate.Where(x => x.ExamTemplateId == exam.ExamTemplateId).Select(a => new { a.CorrectAnwser, a.Id }).ToListAsync();
				var candidateAnswers = await _context.Answer.Where(x => x.ExamId == examId).Select(a => new { a.CandidateAnswer, a.QuestionTemplateId }).ToListAsync();
				foreach (var answer in correctAnswers)
				{
					var examAnswer = candidateAnswers.FirstOrDefault(a => a.QuestionTemplateId == answer.Id);
					if (examAnswer.CandidateAnswer == char.ToUpper(answer.CorrectAnwser))
					{
						score++;
					}
				}
				var examTemplate = await _context.ExamTemplate.FirstOrDefaultAsync(x => x.Id == exam.ExamTemplateId);
				if (examTemplate.MinScore > score)
				{
					exam.Result = false;
				}
				else
				{
					exam.Result = true;
				}

				exam.CandidateScore = (int)(score * 100.0 / examTemplate.MaxScore);

				await _context.SaveChangesAsync();

				return Ok(ExamDto.FromEntity(exam));
			}
			else
			{
				return Ok(ExamDto.FromEntity(exam));
			}
		}


		[HttpPost("createExam/{userId}/{certId}")]
		public async Task<ActionResult<ExamDto>> CreateExam(int userId, int certId)
		{
			var examTemplate = await _context.ExamTemplate.FirstOrDefaultAsync(x => x.CertificateId == certId);
			var candidate = await _context.Candidates.FirstOrDefaultAsync(x => x.UserId == userId);
			var examDto = new ExamDto()
			{
				ExamTemplateId = examTemplate.Id,
				CertificateId = certId,
				CandidateNumber = candidate!.Number,
				ExamDate = examTemplate.ExamDate,
				Marked = false
			};

			var result = await _context.Exam.AddAsync(Exam.ToEntity(examDto));
			await _context.SaveChangesAsync();
			return Ok(ExamDto.FromEntity(result.Entity));
		}

		//add questions for exam
		[HttpPost("AddQuestions")]
		public async Task<ActionResult<QuestionTemplateDto>> AddQuestion(QuestionTemplateDto question)
		{

			var result = await _context.QuestionTemplate.AddAsync(QuestionTemplate.ToEntity(question));
			await _context.SaveChangesAsync();

			return Ok(QuestionTemplateDto.FromEntity(result.Entity));
		}

		//get CandidateExams
		[HttpGet("{userId}")]
		public async Task<ActionResult<IEnumerable<ExamDto>>> GetCandidateExams(int userId)
		{
			var candidate = await _context.Candidates.FirstOrDefaultAsync(x => x.UserId == userId);
			var result = await _context.Exam.Where(x => x.CandidateNumber == candidate.Number).Select(x => ExamDto.FromEntity(x)).ToListAsync();
			return Ok(result);
		}
		//get CandidateExams
		[HttpGet("Answers/{examId}")]
		public async Task<ActionResult<IEnumerable<AnswerDto>>> GetCandidateAnswers(int examId)
		{

			var result = await _context.Answer.Where(x => x.ExamId == examId).ToListAsync();
			return Ok(result);
		}
		//mark exam
		[HttpPost("MarkExam")]
		public async Task<ActionResult<ExamDto>> MarkExam(ExamDto markedExam)
		{
			var exam = await _context.Exam.FirstOrDefaultAsync(ex => ex.Id == markedExam.Id);
			exam.Evaluation = markedExam.Evaluation;
			exam.Marked = markedExam.Marked;
			await _context.SaveChangesAsync();
			return Ok(ExamDto.FromEntity(exam));
		}

	}
	//// endpoint for a candidate to submit their answers
	//[HttpPost("{examId}")]
	//public async Task<ActionResult<ExamDto>> SubmitExamAttempt(List<QuestionDto> questions)
	//{

	//	var correctAnswers = await _context.ExamAnswers
	// .Where(answer => answer.IsCorrect)
	// .ToListAsync();

	//	int score = 0;
	//	var candidateAnswerEntities = new List<CandidateAnswer>();
	//	foreach (var userAnswer in examAttemptDto.CandidateAnswers)
	//	{
	//		if (correctAnswers.Any(ca => ca.ExamQuestionId == userAnswer.ExamQuestionId && ca.Id == userAnswer.ExamAnswerId && userAnswer.IsSelected))
	//		{
	//			score++;
	//		}

	//		var candidateAnswerEntity = new CandidateAnswer
	//		{
	//			ExamAttemptId = examAttemptDto.Id, // Assuming this is set correctly
	//			ExamQuestionId = userAnswer.ExamQuestionId,
	//			ExamAnswerId = userAnswer.ExamAnswerId,
	//			IsSelected = userAnswer.IsSelected
	//		};
	//		candidateAnswerEntities.Add(candidateAnswerEntity);
	//	}

	//	// Each question is worth 25%, so multiply by 25 to get the total score
	//	var percentageScore = score * 25;

	//	// Determine pass or fail
	//	bool passed = percentageScore >= 75;

	//	// Create and save the exam attempt
	//	var examAttempt = new ExamAttempt
	//	{
	//		CandidateId = examAttemptDto.CandidateId,
	//		AttemptDate = DateTime.Now,
	//		Score = percentageScore,
	//		Passed = passed,
	//		CertificateId = examAttemptDto.CertificateId,
	//		CandidateAnswers = candidateAnswerEntities
	//	};
	//	_context.ExamAttempts.Add(examAttempt);
	//	await _context.SaveChangesAsync();

	//	// Create a result DTO to return
	//	var resultDto = new ExamSubmissionResultDto
	//	{
	//		Score = percentageScore,
	//		Passed = passed
	//	};

	//	return Ok(resultDto);
	//}

	//// retrieve exam results for a candidate
	//[HttpGet("results/{candidateId}")]
	//public async Task<ActionResult<IEnumerable<ExamAttemptDto>>> GetExamResults(int candidateId)
	//{
	//	var result = await _context.ExamAttempts.Include(x => x.CandidateAnswers)
	//		.ThenInclude(x => x.ExamAnswerId)
	//		.Where(x => x.CandidateId == candidateId)
	//		.Select(x => ExamAttemptDto.FromEntity(x))
	//		.ToListAsync();
	//	await _context.SaveChangesAsync();
	//	return Ok(result);
	//}

	////retrieve a specific exam result for a candidate
	//[HttpGet("results/{candidateId}/{attemptId}")]
	//public async Task<ActionResult<ExamAttemptDto>> GetExamResult(int candidateId, int attemptId)
	//{
	//	var result = await _context.ExamAttempts.Include(x => x.CandidateAnswers)
	//		.ThenInclude(x => x.ExamAnswerId)
	//		.FirstOrDefaultAsync(x => x.CandidateId == candidateId && x.Id == attemptId);

	//	if (result == null)
	//	{
	//		return NotFound();
	//	}
	//	await _context.SaveChangesAsync();
	//	return Ok(ExamAttemptDto.FromEntity(result));
	//}

	////delete an exam question
	//[HttpDelete("questions/{id}")]
	//public async Task<ActionResult<ExamQuestionDto>> DeleteExamQuestion(int id)
	//{
	//	var examQuestion = await _context.ExamQuestions.FindAsync(id);
	//	if (examQuestion == null)
	//	{
	//		return NotFound();
	//	}

	//	_context.ExamQuestions.Remove(examQuestion);
	//	await _context.SaveChangesAsync();

	//	return Ok(ExamQuestionDto.FromEntity(examQuestion));
	//}

	////update an exam question
	//[HttpPut("questions/{id}")]
	//public async Task<ActionResult<ExamQuestionDto>> UpdateExamQuestion(int id, ExamQuestionDto examQuestionDto)
	//{


	//	var examQuestion = await _context.ExamQuestions
	//							.Include(q => q.Answers)
	//							.FirstOrDefaultAsync(q => q.Id == id);

	//	if (examQuestion == null)
	//	{
	//		return NotFound();
	//	}

	//	examQuestion.Text = examQuestionDto.Text;

	//	// Assuming all updated answers are marked as correct for now
	//	var updatedAnswers = examQuestionDto.Answers.Select(ExamAnswerDto.ToEntity).ToList();
	//	foreach (var answer in examQuestion.Answers)
	//	{
	//		var updatedAnswer = updatedAnswers.FirstOrDefault(a => a.Id == answer.Id);
	//		if (updatedAnswer != null)
	//		{
	//			answer.Text = updatedAnswer.Text;
	//			// Not updating IsCorrect here
	//		}
	//	}

	//	// Add new answers
	//	foreach (var newAnswer in updatedAnswers.Where(a => a.Id == 0))
	//	{
	//		examQuestion.Answers.Add(newAnswer);
	//	}

	//	await _context.SaveChangesAsync();

	//	return Ok(ExamQuestionDto.FromEntity(examQuestion));
	//}



}

