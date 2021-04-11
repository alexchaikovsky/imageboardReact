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
        async public Task SavePost(Post post)
        {
            //await context.Posts.AddAsync(post);
            context.Posts.Add(post);
            await context.SaveChangesAsync();
            //context.SaveChanges();
        }
    }
}
