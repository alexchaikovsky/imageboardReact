using ImageBoardReact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBoardReact.Infrastructure
{
    public static class ThreadsSorter
    {
        public static IEnumerable<Post> SortByLastPost(IQueryable<Post> posts)
        {
            var repo = posts.Where(post => post.Id == post.ThreadId);
            var sortedByTime = posts.OrderByDescending(x => x.DateTime);
            var q =
                from post in repo
                join thread in sortedByTime on post.ThreadId equals thread.ThreadId
                select post;
            var z = q.AsEnumerable();
            //var z = q.GroupBy(x => x.ThreadId).AsEnumerable().Select(group => group.FirstOrDefault());
            return z.Reverse().DistinctBy(x => x.ThreadId);

            //var q = posts.Reverse().Select(post => post.ThreadId);
            ////return q;
            //return posts.Where(x => x.Id == x.ThreadId);
        }
        public static IEnumerable<Post> SortByLastPostTime(IQueryable <Post> posts)
        {
            var sorted = posts.Reverse().DistinctBy(x => x.ThreadId);
            return sorted;
        }
    }
}
