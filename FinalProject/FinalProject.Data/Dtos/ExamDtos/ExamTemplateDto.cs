using FinalProject.Data.Entities.Exam;

namespace FinalProject.Data.Dtos.ExamDtos
{
    public class ExamTemplateDto
	{
		public int Id { get; set; }
		public int CertificateId { get; set; }
		public DateTime ExamDate { get; set; }
		public int MaxScore { get; set; }
		public int MinScore { get; set; }


		public static ExamTemplateDto FromEntity(ExamTemplate examTemplate)
		{
			return new ExamTemplateDto
			{
				Id = examTemplate.Id,
				CertificateId = examTemplate.CertificateId,
				MaxScore = examTemplate.MaxScore,
				MinScore = examTemplate.MinScore,

				ExamDate = examTemplate.ExamDate,
			};
		}
	}
}
