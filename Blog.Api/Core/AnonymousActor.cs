using Blog.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Api.Core
{
    public class AnonymousActor : IApplicationActor
    {
        public int Id => 0;
        public string Identity => "Anonymous Actor";
        public IEnumerable<int> AllowedUseCases => new List<int> { 3,4,14,15,8 };
    }
}
