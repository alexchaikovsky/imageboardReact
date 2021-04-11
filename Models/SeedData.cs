using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
namespace ImageBoardReact.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            PostsDbContext context = app.ApplicationServices
            .CreateScope().ServiceProvider.GetRequiredService<PostsDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.Posts.Any())
            {
                context.Posts.AddRange(
                    new Post { Text = "0abcd123", DateTime = DateTime.Now, ThreadId = 1},
                    new Post { Text = "1qweqwe", DateTime = DateTime.Now, ThreadId = 1 },
                    new Post { Text = "2sfasf", DateTime = DateTime.Now, ThreadId = 1 },
                    new Post { Text = "3adsa", DateTime = DateTime.Now, ThreadId = 1 },
                    new Post { Text = "4abcdasd123", DateTime = DateTime.Now, ThreadId = 1 },
                    new Post { Text = "5abcddasd123", DateTime = DateTime.Now, ThreadId = 1 }
                    );
            }     
               
            context.SaveChanges();
        }
    }
}

