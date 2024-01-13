using FinalProject.Data.Dtos.ExamDtos;

namespace FinalProject.Data.Entities.Exam
{
	public class Question
	{
		public int Id { get; set; }
		public int ExamId { get; set; }
		public int ExamTemplateId { get; set; }
		public required string QuestionText { get; set; }
		public required string AnswerA { get; set; }
		public required string AnswerB { get; set; }
		public required string AnswerC { get; set; }
		public required string AnswerD { get; set; }
		public required string CorrectAnwser { get; set; }
		public required string CandidateAnswer { get; set; }


		public static Question ToEntity(QuestionDto question)
		{
			return new Question
			{
				Id = question.Id,
				ExamId = question.ExamId,
				ExamTemplateId = question.ExamTemplateId,
				QuestionText = question.QuestionText,
				AnswerA = question.AnswerA,
				AnswerB = question.AnswerB,
				AnswerC = question.AnswerC,
				AnswerD = question.AnswerD,
				CorrectAnwser = question.CorrectAnwser,
				CandidateAnswer = question.CandidateAnswer,
			};
		}
	}
}
