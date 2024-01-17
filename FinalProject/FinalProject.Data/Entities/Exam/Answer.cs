using FinalProject.Data.Dtos.ExamDtos;

namespace FinalProject.Data.Entities.Exam
{
	public class Answer
	{
		public int Id { get; set; }
		public int ExamId { get; set; }
		public int QuestionTemplateId { get; set; }
		public char CandidateAnswer { get; set; }
		public static Answer ToEntity(AnswerDto question)
		{
			return new Answer
			{
				Id = question.Id,
				ExamId = question.ExamId,
				QuestionTemplateId = question.QuestionTemplateId,
				CandidateAnswer = question.CandidateAnswer,
			};
		}
	}
}
