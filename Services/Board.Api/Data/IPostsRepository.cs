using Board.Api.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Board.Api.Models;

namespace Board.Api.Data
{
    public interface IPostsRepository
    {
        IQueryable<Post> Posts { get; }
        Task SavePostAsync(Post post);
        Task SaveThreadAsync(Post post);
        Task<List<Post>> GetThreadsInOrderAsync();
        Task<List<Post>> GetPostsInOrderAsync(int threadId);
        //void ClearDb(IEnumerable <Post> entitiesToRemove);
        Task RemovePostsAsync(IEnumerable<Post> postsToRemove);
        //Task DeletePost(Post post);
        //Task DeleteThread(int threadId);
        //Task<bool> DeleteAsync(int id);
    }
}
