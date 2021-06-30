using Board.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Board.Api.Models
{
    public class EFPostsRepository : IPostsRepository
    {
        private BoardDbContext context;
        public EFPostsRepository(BoardDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Post> Posts => context.Posts;


        public Task<List<Post>> GetPostsInOrderAsync(int threadId)
        {
             return context.Posts
                .Where(x => x.ThreadId == threadId)
                .OrderBy(post => post.DateTime)
                .ToListAsync();
        }

        public Task<List<Post>> GetThreadsInOrderAsync()
        {
            return context.Posts
                .Where(post => post.Id == post.ThreadId)
                .OrderByDescending(post => post.LastPostTime)
                .ToListAsync();
        }

        async public Task SavePostAsync(Post post)
        {
            //await context.Posts.AddAsync(post);
            context.Posts.Add(post);
            if (context.Posts.Where(p => p.ThreadId == post.ThreadId).Count() < 500)
            {
                var thread = context.Posts.Single(x => x.Id == post.ThreadId);
                thread.LastPostTime = post.DateTime;
            }
            await context.SaveChangesAsync();
            //context.SaveChanges();
        }
        async public Task SaveThreadAsync(Post post)
        {
            //await context.Posts.AddAsync(post);
            context.Posts.Add(post);
            context.SaveChanges();
            context.Posts.OrderBy(x => x.Id).Last().ThreadId = context.Posts.OrderBy(x => x.Id).Last().Id;
            await context.SaveChangesAsync();
            //context.SaveChanges();
        }

        public void ClearDb(IEnumerable<Post> entitiesToRemove)
        {
            context.Posts.RemoveRange(entitiesToRemove);
            context.SaveChanges();
        }
        public Task RemovePostsAsync(IEnumerable<Post> postsToRemove)
        {
            context.Posts.RemoveRange(postsToRemove);
            return context.SaveChangesAsync();
        }

        public Task DeletePost(int id)
        {
            var post = context.Posts.Single(x => x.Id == id);
            context.Posts.Remove(post);
            return context.SaveChangesAsync();
        }

        async public Task DeleteThread(int threadId)
        {
            await RemovePostsAsync(context.Posts.Where(x => x.ThreadId == threadId).AsEnumerable());
        }

        //async public Task<bool> DeleteAsync(int id)
        //{
        //    var post = context.Posts.SingleOrDefault(x => x.Id == id);
        //    if (post == null)
        //    {
        //        return false;
        //    }
        //    if (post.Id == post.ThreadId)
        //    {
        //        await DeleteThread(id);
        //        return true;
        //    }
        //    await DeletePost(post);
        //    return true;
        //}
    }
}
