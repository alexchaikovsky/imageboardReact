using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBoardReact.Models
{
    public class EFPostsRepository : IPostsRepository
    {
        private PostsDbContext context;
        public EFPostsRepository(PostsDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Post> Posts => context.Posts;
        async public Task SavePostAsync(Post post)
        {
            //await context.Posts.AddAsync(post);
            context.Posts.Add(post);
            var thread = context.Posts.Single(x => x.Id == post.ThreadId);
            thread.LastPostTime = post.DateTime;
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
    }
}
