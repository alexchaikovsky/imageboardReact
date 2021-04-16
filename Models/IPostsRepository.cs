using ImageBoardReact.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBoardReact.Models
{
    public interface IPostsRepository
    {
        IQueryable<Post> Posts { get; }
        Task SavePostAsync(Post post);
        Task SaveThreadAsync(Post post);
        Task<List<Post>> GetThreadsInOrderAsync();
        Task<List<Post>> GetPostsInOrderAsync(int threadId);
        void ClearDb(IEnumerable <Post> entitiesToRemove);
        Task ClearDbAsync(IEnumerable<Post> entitiesToRemove);
        Task DeletePost(int id);
        Task DeleteThread(int threadId);
        Task<bool> DeleteAsync(int id);
    }
}
