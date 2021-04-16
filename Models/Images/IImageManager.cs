using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBoardReact.Models.Images
{
    public interface IImageManager
    {
        Task<string> SaveImageAsync(IFormFile image);
        void RemoveImages(List<string> imagesNames);
        Task RemoveImagesAsync(List<string> imagesNames);
    }
}
