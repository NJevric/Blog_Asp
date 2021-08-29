using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.DataTransfer
{
    public class ReadPostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Like { get; set; } = 0;
        public string AuthorName { get; set; }
        public IEnumerable<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
        public IEnumerable<CommentDto> Comments { get; set; } = new List<CommentDto>();
        public IEnumerable<ImageDto> Images { get; set; } = new List<ImageDto>();
    }
}
