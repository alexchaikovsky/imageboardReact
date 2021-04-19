using ImageBoardReact.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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

        public Task<string> SaveImageAsync(IFormFile image)
        {
            string name = GetNewName() + "." + image.FileName.Split(".").Last();
            string fullPath = Path.Combine(imagesSavePath, name);
            using (Stream fileStream = new FileStream(fullPath, FileMode.Create))
            {
                image.CopyToAsync(fileStream);
            }
            return Task.FromResult(name);
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

        public Task<List<string>> SaveImagesPackAsync(List<IFormFile> images)
        {
            return Task.Run(() =>
            {
                List<Task<string>> tasks = new();
                foreach (var image in images)
                {
                    tasks.Add(SaveImageAsync(image));
                }
                return tasks.Select(task => task.Result).ToList();
            });
            
        }
    }
}
