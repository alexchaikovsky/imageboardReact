using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageBoardReact.Models;

namespace ImageBoardReact.Infrastructure
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
