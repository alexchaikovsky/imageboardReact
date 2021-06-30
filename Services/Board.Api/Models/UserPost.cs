using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Board.Api.Models
{
    public class UserPost
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
