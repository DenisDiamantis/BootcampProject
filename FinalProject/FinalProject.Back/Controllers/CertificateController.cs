using FinalProject.Back.Contexts;
using FinalProject.Data.Dtos.CertificateDtos;
using FinalProject.Data.Entities;
using FinalProject.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Hosting.Internal;

namespace FinalProject.Back.Controllers

    // Certificate CRUID
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        private readonly CertificationDbContext _context;
        private readonly IFileStorageService _fileStorageService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CertificateController(CertificationDbContext context, IFileStorageService fileStorageService, IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
            _context = context;
            _fileStorageService = fileStorageService;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CertificateViewDto>>> GetAllCertificates()
        {
            var result = await _context.Certificates
                .Select(x => CertificateViewDto.FromEntity(x))
                .ToListAsync();

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<CertificateViewDto>>> GetCertificateById(int id)
        {
            var certificate = await _context.Certificates.FirstOrDefaultAsync(ce => ce.Id == id);
            if (certificate == null)
            {
                return NotFound();
            }

            return Ok(CertificateViewDto.FromEntity(certificate));
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<CertificateCreateDto>> CreateCertificate(CertificateCreateDto certificateDto)
        {
            var certificate = Certificate.ToEntity(certificateDto, _fileStorageService);
            Console.WriteLine(certificateDto.UploadedImage);
            _context.Certificates.Add(certificate);
            await _context.SaveChangesAsync();

            return Ok(certificateDto);

        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<CertificateViewDto>> UpdateCertificate(int id, CertificateCreateDto certificateDto)
        {
            var certificate = await _context.Certificates.FirstOrDefaultAsync(x => x.Id == id);

            if (certificate == null)
            {
                return NotFound();
            }

            certificate.Title = certificateDto.Title;
            certificate.Description = certificateDto.Description;
            certificate.Cost = certificateDto.Cost;
            certificate.Photo = certificateDto.Photo;

            await _context.SaveChangesAsync();

            return Ok(CertificateViewDto.FromEntity(certificate));
        }


        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<CertificateViewDto>> DeleteCertificate(int id)
        {
            var certificate = await _context.Certificates.FirstOrDefaultAsync(ce => ce.Id == id);
            if (certificate == null)
            {
                return NotFound();
            }

            _context.Certificates.Remove(certificate);
            await _context.SaveChangesAsync();

            return Ok(CertificateViewDto.FromEntity(certificate));
        }

        [HttpGet("/image/{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            var imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", imageName);

            if (System.IO.File.Exists(imagePath))
            {
                var imageBytes = System.IO.File.ReadAllBytes(imagePath);
                return File(imageBytes, "image/jpg"); 
            }

            return NotFound();
        }

    }
}
