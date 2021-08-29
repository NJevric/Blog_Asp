using Blog.Application;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Api.Core
{
    public class JwtActor : IApplicationActor
    {
        public int Id { get; set; }
        public string Identity { get; set; }
        public IEnumerable<int> AllowedUseCases { get; set; }
    }
}
