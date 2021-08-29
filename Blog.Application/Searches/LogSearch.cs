using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Application.Searches
{
    public class LogSearch : PageSearch
    {
        public string Actor { get; set; }
        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }
    }
}
