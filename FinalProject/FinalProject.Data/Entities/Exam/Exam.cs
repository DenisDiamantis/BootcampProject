using FinalProject.Data.Dtos.ExamDtos;

namespace FinalProject.Data.Entities.Exam
{
	public class Exam
	{
		public int Id { get; set; }
		public int ExamTemplateId { get; set; }
		public int CertificateId { get; set; }
		public Guid CandidateNumber { get; set; }
		public DateTime ExamDate { get; set; }
		public int CandidateScore { get; set; }
		public bool Marked { get; set; }
		public bool Evaluation { get; set; }
		public bool Result { get; set; }
		public int MarkerId { get; set; }


		public static Exam ToEntity(ExamDto exam)
		{
			return new Exam
			{
				Id = exam.Id,
				CertificateId = exam.CertificateId,
				CandidateNumber = exam.CandidateNumber,
				CandidateScore = exam.CandidateScore,
				ExamTemplateId = exam.ExamTemplateId,
				ExamDate = exam.ExamDate,
				Marked = exam.Marked,
				Result = exam.Result,
				MarkerId = exam.MarkerId,
				Evaluation = exam.Evaluation
			};
		}
	}
}
