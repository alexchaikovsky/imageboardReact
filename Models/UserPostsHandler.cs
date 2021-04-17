using ImageBoardReact.Models.Images;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImageBoardReact.Models
{
    public class UserPostsHandler : IUserPostsHandler
    {
        public async Task<Post> BuildFromUserPostAsync(UserPost userPost, IImageManager imageManager, int threadId)
        {
            List<string> fileWebPaths = new List<string>();
            if (userPost.Images != null)
            {
                foreach (var image in userPost.Images)
                {
                    fileWebPaths.Add(await imageManager.SaveImageAsync(image));

                }
            }
            return new Post
            {
                //Id = newPostId,
                Name = userPost.Name,
                Subject = userPost.Subject,
                Text = userPost.Text,
                DateTime = DateTime.Now,
                ThreadId = threadId,
                ImagesSource = fileWebPaths.ToArray()
            };

        }
        public async Task<Post> BuildFromUserPostAsync(UserPost userPost, IImageManager imageManager)
        {
            List<string> fileWebPaths = new List<string>();
            if (userPost.Images != null)
            {
                foreach (var image in userPost.Images)
                {
                    fileWebPaths.Add(await imageManager.SaveImageAsync(image).ConfigureAwait(false));

                }
            }
            return new Post
            {
                //Id = newPostId,
                Name = userPost.Name,
                Subject = userPost.Subject,
                Text = userPost.Text,
                DateTime = DateTime.Now,
                LastPostTime = DateTime.Now,
                ImagesSource = fileWebPaths.ToArray()
            };

        }
    }
}

