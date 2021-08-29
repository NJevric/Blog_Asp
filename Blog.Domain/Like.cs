using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Domain
{
    public class Like
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }

        public virtual Post Post { get; set; }

    }
}
