using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Domain
{
    public class Comment : Entity
    {
        public string Author { get; set; }
        public string CommetText { get; set; }
        public int PostId { get; set; }
        public bool Approved { get; set; }
        public virtual Post Post { get; set; }
    }
}
