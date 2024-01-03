using FinalProject.Data.Dtos.CertificateDtos;

namespace FinalProject.Data.Entities
{
    public class Certificate
    {
        public int Id { get; set; }
        public string Title { get; set; }


        public static Certificate ToEntity(CertificateCreateDto certificateCreateDto)
        {
            return new Certificate
            {
                Title = certificateCreateDto.Title
            };
        }
    }
}
