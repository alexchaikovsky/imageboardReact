using ImageBoardReact.Models.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBoardReact.Models
{
    public interface IUserPostsHandler
    {
        Task<Post> BuildFromUserPostAsync(UserPost userPost, IImageManager imageManage, int newPostId, int threadId);
    }
}
