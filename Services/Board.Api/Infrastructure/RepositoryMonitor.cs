using Board.Api.Data;
using Board.Api.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Board.Api.Infrastructure
{
    public class RepositoryMonitor : IRepositoryMonitor
    {
        private int postsCount;
        private int imagesCount;
        private int threadsCount;
        ILogger<RepositoryMonitor> _logger;
        public RepositoryMonitor(ILogger<RepositoryMonitor> logger)
        {
            _logger = logger;
        }
        public int PostsCount => postsCount;

        public int ThreadsCount => threadsCount;

        public int ImagesCount => imagesCount;

        public void LogState()
        {
            _logger.LogInformation($"Current state:\n " +
                $"\tPosts {postsCount}" +
                $"\tThreads {threadsCount}" +
                $"\tImages {imagesCount}");
        }

        public void Update(IPostsRepository postsRepository)
        {
            threadsCount = postsRepository.Posts.Where(post => post.Id == post.ThreadId).Count();
            postsCount = postsRepository.Posts.Count();
            imagesCount = postsRepository.Posts.Select(post => post.ImagesSource).Sum(arr => arr.Length);
        }
    }
}
