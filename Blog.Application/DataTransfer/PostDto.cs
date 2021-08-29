using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.DataTransfer
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Like { get; set; } = 0;
        public int UserId { get; set; }
        public string AuthorName { get; set; }
        public IEnumerable<int> CategoryIds { get; set; }

        public IEnumerable<IFormFile> Images { get; set; } = new List<IFormFile>();
    }
}
