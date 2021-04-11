using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageBoardReact.Models
{
    public class Post
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int ThreadId { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public string[] ImagesSource { get; set; }
    }
}
