using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.DataTransfer
{
    public class LikeDto
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
    }
}
