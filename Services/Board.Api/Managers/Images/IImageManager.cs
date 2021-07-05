using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Board.Api.Managers.Images
{
    public interface IImageManager
    {
        Task<string> SaveImageAsync(IFormFile image);
        void RemoveImages(List<string> imagesNames);
        Task RemoveImagesAsync(List<string> imagesNames);
        Task<string[]> SaveImagesPackAsync(List<IFormFile> images);
    }
}
