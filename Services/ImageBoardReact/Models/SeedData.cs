using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using ImageBoardReact.Authentiication.Security;

namespace ImageBoardReact.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            BoardDbContext context = app.ApplicationServices
            .CreateScope().ServiceProvider.GetRequiredService<BoardDbContext>();
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
            if (!context.Users.Any())
            {
                //PasswordManager passwordManager = new PasswordManager();

                string login = "admin";
                string password = "12345";
                
                var salted = PasswordManager.GetSaltAndHash(password);

                context.Users.Add(new User {Login = login, Role = "admin", Salt = salted.salt, PasswordHash = salted.hash });

                string userLogin = "chad";
                string userPassword = "77777";
                
                var userSalted = PasswordManager.GetSaltAndHash(userPassword);

                context.Users.Add(new User { Login = userLogin, Role = "user", Salt = userSalted.salt, PasswordHash = userSalted.hash });
            }
            context.SaveChanges();
        }
    }
}

