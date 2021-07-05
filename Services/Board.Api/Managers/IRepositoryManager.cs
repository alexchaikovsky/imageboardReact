using Board.Api.Infrastructure;
using Board.Api.Managers.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Board.Api.Managers
{
    public interface IRepositoryManager
    {
        int MaxNumberOfThreads { get; }
        Task RemoveOldestThreads();
        //Task RemoveThreadAndItsContents(int threadId);
        //Task<bool> RemovePostAndItsContents(int postId);
        //Task RemovePostAsync(Post post);
        Task<bool> RemoveByIdAsync(int postId);
    }
}
