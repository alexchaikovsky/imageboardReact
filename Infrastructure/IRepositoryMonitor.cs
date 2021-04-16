using ImageBoardReact.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBoardReact.Infrastructure
{
    public interface IRepositoryMonitor
    {
        int PostsCount { get;}
        int ThreadsCount { get; }
        int ImagesCount { get; }
        void LogState();
        void Update(IPostsRepository postsRepository);
    }
}
