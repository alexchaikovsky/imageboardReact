using Board.Api.Data;
using Board.Api.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Board.Api.Infrastructure
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
