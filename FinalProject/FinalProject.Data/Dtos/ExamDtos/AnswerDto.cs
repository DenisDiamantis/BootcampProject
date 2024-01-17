using FinalProject.Data.Entities.Exam;

namespace FinalProject.Data.Dtos.ExamDtos
{
	public class AnswerDto
	{
		public int Id { get; set; }
		public int ExamId { get; set; }
		public int QuestionTemplateId { get; set; }
		public char CandidateAnswer { get; set; }

		public static AnswerDto FromEntity(Answer question)
		{
			return new AnswerDto
			{
				Id = question.Id,
				ExamId = question.ExamId,
				QuestionTemplateId = question.QuestionTemplateId,
				CandidateAnswer = question.CandidateAnswer,
			};
		}
	}
}
