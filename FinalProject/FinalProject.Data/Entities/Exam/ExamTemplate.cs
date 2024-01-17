using FinalProject.Data.Dtos.ExamDtos;

namespace FinalProject.Data.Entities.Exam
{
	public class ExamTemplate
	{
		public int Id { get; set; }
		public int CertificateId { get; set; } // Foreign key to the Certificate entity
		public DateTime ExamDate { get; set; }
		public int MaxScore { get; set; } // The candidate's score for this attempt
		public int MinScore { get; set; } // The candidate's score for this attempt


		public static ExamTemplate ToEntity(ExamTemplateDto examTemplate)
		{
			return new ExamTemplate
			{
				Id = examTemplate.Id,
				CertificateId = examTemplate.CertificateId,
				MaxScore = examTemplate.MaxScore,
				MinScore = examTemplate.MinScore,
				ExamDate = examTemplate.ExamDate
			};
		}

	}
}
