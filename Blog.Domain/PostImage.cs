using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Domain
{
    public class PostImage : Entity
    {
        public string Src { get; set; }
        public string Alt { get; set; }
        public int PostId { get; set; }

        public virtual Post Post { get; set; }

    }
}
