using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using projectAPI.Core.ServiceCotract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectAPI.Services
{
    public class FileServices : IFileServices
    {
        private readonly IWebHostEnvironment webHostEnvironment;
       public FileServices(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }
        public string Upload(IFormFile formFile)
        {
            if (formFile == null || formFile.Length == 0)
                throw new Exception("Invalid file uploaded");

            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                formFile.CopyTo(fileStream);
            }

            
            string imageUrl = $"/images/{uniqueFileName}";
            return imageUrl;

        }

    }
}
