using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBoardReact.Models.Images
{
    public class LocalImageManager : IImageManager
    {
        private readonly IHostEnvironment _hostingEnvironment;
        private readonly string imagesSavePath;
        public LocalImageManager(IHostEnvironment environment, string folderName)
        {
            _hostingEnvironment = environment;
            imagesSavePath = Path.Combine(_hostingEnvironment.ContentRootPath, folderName);
        }
        private string GetNewName()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }

        public async Task<string> SaveImageAsync(IFormFile image)
        {
            string name = GetNewName() + "." + image.FileName.Split(".").Last();
            string fullPath = Path.Combine(imagesSavePath, name);
            using (Stream fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return name;
        }
    }
}
