using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Domain
{
    public class Entity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
