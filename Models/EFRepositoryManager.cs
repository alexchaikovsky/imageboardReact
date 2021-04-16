using ImageBoardReact.Infrastructure;
using ImageBoardReact.Models.Images;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBoardReact.Models
{
    public class EFRepositoryManager : IRepositoryManager
    {
        private readonly ILogger<EFRepositoryManager> logger;
        private readonly IImageManager imageManager;
        private readonly IRepositoryMonitor repositoryMonitor;
        private readonly IPostsRepository postsRepository;

        public EFRepositoryManager(ILogger<EFRepositoryManager> logger, IImageManager imageManager, IRepositoryMonitor repositoryMonitor, IPostsRepository postsRepository)
        {
            this.logger = logger;
            this.imageManager = imageManager;
            this.repositoryMonitor = repositoryMonitor;
            this.postsRepository = postsRepository;
        }
        public int MaxNumberOfThreads => 10;
        
        public async Task RemoveOldestThreads()
        {
            var threadsToRemove = postsRepository.Posts
                .Where(post => post.Id == post.ThreadId)
                .OrderBy(post => post.LastPostTime)
                .Take(repositoryMonitor.ThreadsCount - MaxNumberOfThreads)
                .ToList();

            List<Post> entitiesToRemove = new();
            List<string> imagesToRemove = new();

            foreach (var thread in threadsToRemove)
            {
                entitiesToRemove.AddRange(
                    postsRepository.Posts
                    .Where(post => post.ThreadId == thread.Id));
                imagesToRemove.AddRange(
                    postsRepository.Posts
                    .Where(post => post.ThreadId == thread.Id)
                    .Select(post => post.ImagesSource)
                    .AsEnumerable()
                    .SelectMany(x => x));
            }
            entitiesToRemove.AddRange(threadsToRemove);

            var task1 = postsRepository.ClearDbAsync(entitiesToRemove);
            var task2 = imageManager.RemoveImagesAsync(imagesToRemove);
            await task1;
            await task2;
            logger.LogInformation("Cleaning finished!");
            
        }
    }
}
