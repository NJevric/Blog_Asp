using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.DataTransfer
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string CommentAuthor { get; set; }
        public string CommentText { get; set; }
        public int PostId { get; set; }
        public bool Approved { get; set; } = false;
    }
}
