using FinalProject.Data.Entities.Exam;

namespace FinalProject.Data.Dtos.ExamDtos
{
	public class QuestionTemplateDto
	{

		public int Id { get; set; }
		public int ExamTemplateId { get; set; }
		public required string QuestionText { get; set; }
		public required string AnswerA { get; set; }
		public required string AnswerB { get; set; }
		public required string AnswerC { get; set; }
		public required string AnswerD { get; set; }
		public required string CorrectAnwser { get; set; }

		public static QuestionTemplateDto FromEntity(QuestionTemplate questionTemplate)
		{
			return new QuestionTemplateDto
			{
				Id = questionTemplate.Id,
				ExamTemplateId = questionTemplate.ExamTemplateId,
				QuestionText = questionTemplate.QuestionText,
				AnswerA = questionTemplate.AnswerA,
				AnswerB = questionTemplate.AnswerB,
				AnswerC = questionTemplate.AnswerC,
				AnswerD = questionTemplate.AnswerD,
				CorrectAnwser = questionTemplate.CorrectAnwser,
			};
		}

	}
}
