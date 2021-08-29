using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.Searches
{
    public class PostSearch : PageSearch
    {
        public string Title { get; set; }
        public int? MinLike { get; set; }
        public int? MaxLike { get; set; }
        public IEnumerable<int> CategoryIds { get; set; } = new List<int>();
      
    }
}
