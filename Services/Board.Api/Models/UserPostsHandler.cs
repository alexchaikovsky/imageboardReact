using Board.Api.Infrastructure;
using Board.Api.Models.Images;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Board.Api.Models
{
    public class UserPostsHandler : IUserPostsHandler
    {
        private readonly IImageManager imageManager;
        private readonly IDateTimeProvider dateTimeProvider;

        public UserPostsHandler(IImageManager imageManager, IDateTimeProvider dateTimeProvider)
        {
            this.imageManager = imageManager;
            this.dateTimeProvider = dateTimeProvider;
        }
        public async Task<Post> BuildFromUserPostAsync(UserPost userPost, int threadId)
        {
            dateTimeProvider.UpdateTime();
            int imagesCount = userPost.Images?.Count ?? 0;
            string [] fileWebPaths = new string[imagesCount];
            if (userPost.Images != null)
            {
                fileWebPaths = await imageManager.SaveImagesPackAsync(userPost.Images); //await ManageImages(userPost);
            }
            return new Post
            {
                //Id = newPostId,
                Name = userPost.Name,
                Subject = userPost.Subject,
                Text = userPost.Text,
                DateTime = dateTimeProvider.GetTime,
                ThreadId = threadId,
                ImagesSource = fileWebPaths
            };

        }
        public async Task<Post> BuildFromUserPostAsync(UserPost userPost)
        {
            dateTimeProvider.UpdateTime();
            int imagesCount = userPost.Images?.Count ?? 0;
            string [] fileWebPaths = new string[imagesCount];
            if (userPost.Images != null)
            {
                fileWebPaths = await imageManager.SaveImagesPackAsync(userPost.Images); //await ManageImages(userPost);
            }
            return new Post
            {
                //Id = newPostId,
                Name = userPost.Name,
                Subject = userPost.Subject,
                Text = userPost.Text,
                DateTime = dateTimeProvider.GetTime,
                LastPostTime = dateTimeProvider.GetTime,
                ImagesSource = fileWebPaths
            };

        }

        //private async Task<List<string>> ManageImages(UserPost userPost)
        //{
        //    List<string> fileWebPaths = new List<string>();
        //    if (userPost.Images != null)
        //    {
        //        await SaveImages(userPost, fileWebPaths).ConfigureAwait(false);
        //    }

        //    return fileWebPaths;
        ////}

        //private async Task SaveImages(UserPost userPost, List<string> fileWebPaths)
        //{
        //    foreach (var image in userPost.Images)
        //    {
        //        fileWebPaths.Add(await imageManager.SaveImageAsync(image).ConfigureAwait(false));

        //    }
        //}
    }
}

