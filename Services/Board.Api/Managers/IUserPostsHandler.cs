using Board.Api.Managers.Images;
using Board.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Board.Api.Managers
{
    public interface IUserPostsHandler
    {
        Task<Post> BuildFromUserPostAsync(UserPost userPost, int threadId);
        Task<Post> BuildFromUserPostAsync(UserPost userPost);
    }
}
