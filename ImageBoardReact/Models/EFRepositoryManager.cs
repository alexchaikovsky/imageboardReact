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
            if (repositoryMonitor.ThreadsCount <= MaxNumberOfThreads)
            {
                return;
            }
            var threadsToRemove = postsRepository.Posts
                .Where(post => post.Id == post.ThreadId)
                .OrderBy(post => post.LastPostTime)
                .Take(repositoryMonitor.ThreadsCount - MaxNumberOfThreads)
                .ToList();
            await RemoveThreads(threadsToRemove);
            logger.LogInformation("Cleaning finished!");

        }

        private async Task RemoveThreads(List<Post> threadsToRemove)
        {
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

            var task1 = postsRepository.RemovePostsAsync(entitiesToRemove);
            var task2 = imageManager.RemoveImagesAsync(imagesToRemove);
            await task1;
            await task2;
        }

        async public Task<bool> RemoveByIdAsync(int postId)
        {
            var post = postsRepository.Posts.SingleOrDefault(x => x.Id == postId);
            if (post == null )
            {
                return false;
            }
            if (post.Id == post.ThreadId)
            {
                await RemoveThreadAndItsContents(postId);
                return true;
            }
            await RemovePostAsync(post);
            return true;
        }
        public Task RemovePostAsync(Post post)
        {
            imageManager.RemoveImagesAsync(post.ImagesSource.ToList()).ConfigureAwait(false);
            return postsRepository.RemovePostsAsync(new[] { post});
        }
        public Task RemoveThreadAndItsContents(int threadId)
        {
            var thread = postsRepository.Posts.SingleOrDefault(x => x.Id == threadId);
            List<Post> entitiesToRemove = new();
            List<string> imagesToRemove = new();

            entitiesToRemove.AddRange(
                postsRepository.Posts
                .Where(post => post.ThreadId == thread.Id));
            imagesToRemove.AddRange(
                postsRepository.Posts
                .Where(post => post.ThreadId == thread.Id)
                .Select(post => post.ImagesSource)
                .AsEnumerable()
                .SelectMany(x => x));
            var postsRemoveTask = postsRepository.RemovePostsAsync(entitiesToRemove);
            imageManager.RemoveImagesAsync(imagesToRemove).ConfigureAwait(false);
            return postsRemoveTask;
            //await imagesRemoveTask;

        }
    }
}
