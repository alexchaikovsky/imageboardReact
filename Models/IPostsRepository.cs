using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBoardReact.Models
{
    public interface IPostsRepository
    {
        IQueryable<Post> Posts { get; }
        Task SavePostAsync(Post post);
        Task SaveThreadAsync(Post post);
    }
}
