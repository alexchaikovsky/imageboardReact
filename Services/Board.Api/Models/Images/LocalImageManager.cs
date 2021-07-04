using Board.Api.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Board.Api.Models.Images
{
    public class LocalImageManager : IImageManager
    {
        private readonly IHostEnvironment _hostingEnvironment;
        private readonly string imagesSavePath;
        private readonly ILogger<LocalImageManager> _logger;
        public LocalImageManager(IHostEnvironment environment, ILogger<LocalImageManager> logger)
        {
            _logger = logger;
            _hostingEnvironment = environment;
            imagesSavePath = Path.Combine(_hostingEnvironment.ContentRootPath, Options.ImageManagerFolder);
        }
        private string GetNewName()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }

        public async Task<string> SaveImageAsync(IFormFile image)
        {
            string name = GetNewName() + "." + image.FileName.Split(".").Last();
            if (!Directory.Exists(imagesSavePath))
            {
                throw new FileNotFoundException();
            }
            string fullPath = Path.Combine(imagesSavePath, name);
            _logger.LogInformation(fullPath);
            using (Stream fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return name;
        }

        public Task RemoveImagesAsync(List <string> imagesNames)
        {
            return Task.Run(() => RemoveImages(imagesNames));
        }
        public void RemoveImages(List<string> imagesNames)
        {
                foreach (var name in imagesNames)
                {
                    string fullPath = Path.Combine(imagesSavePath, name);
                    try
                    {
                        File.Delete(fullPath);
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e.Message);
                    }
                }
        }

        public async Task<string []> SaveImagesPackAsync(List<IFormFile> images)
        {
            
                List<Task<string>> tasks = new();
                foreach (var image in images)
                {
                    tasks.Add(SaveImageAsync(image));
                }
                return (await Task.WhenAll(tasks)) ?? Array.Empty<string>();// tasks. .When .Select(task => task.Result).ToArray();
            
        }
    }
}
