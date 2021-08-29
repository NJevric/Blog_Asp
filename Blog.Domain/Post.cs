using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Domain
{
    public class Post : Entity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int Like { get; set; }
        public int UserId { get; set; }
        public virtual ICollection<PostImage> PostImages { get; set; } = new HashSet<PostImage>();
        public virtual ICollection<PostCategory> PostCategories { get; set; } = new HashSet<PostCategory>();
        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public virtual ICollection<Like> Likes { get; set; } = new HashSet<Like>();
        public virtual User User { get; set; }

    }
}
