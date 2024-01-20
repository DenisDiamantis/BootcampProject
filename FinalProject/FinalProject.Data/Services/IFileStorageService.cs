using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace FinalProject.Data.Services
{
	public interface IFileStorageService
	{
		string SaveFile(IFormFile file);
	}


	public class LocalFileStorageService : IFileStorageService
	{
		private readonly IWebHostEnvironment _hostingEnvironment;

        public LocalFileStorageService(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
        }



        public string SaveFile(IFormFile file)
		{
			try
            {

                if (_hostingEnvironment.WebRootPath == null || file == null || file.Length == 0)
				{
					// Handle the case where no file is provided or the file is empty
					return null;
				}

                string uploadImageFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadImageFolder))
                {
                    Directory.CreateDirectory(uploadImageFolder);
                }

                // Create a unique file name to avoid conflicts
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadImageFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                // Return the relative path to the saved file (e.g., "uploads/uniqueFileName")
                return Path.Combine("uploads", uniqueFileName);
			}
			catch (Exception ex)
			{
				// Handle exceptions appropriately (e.g., log or throw)
				return null;
			}
		}
	}
}