using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Board.Api.Models;

namespace Board.Api.Infrastructure
{
    public class ContentChecker
    {
        public static bool CheckFieldsOk (UserPost post)
        {
            if (post.Text == "")
            {
                return false;
            }
            return true;
        }
    }
}
