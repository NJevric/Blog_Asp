using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.Searches
{
    public class UserSearch : PageSearch
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public IEnumerable<int> UseCaseIds { get; set; } = new List<int>();
    }
}
