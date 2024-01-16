using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Data.Dtos.CertificateDtos
{
    public class CertificateUpdateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public double Cost { get; set; }
    }
}
