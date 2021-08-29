using Blog.Application;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Implementation.Logging
{
    public class ConsoleUseCaseLogger : IUseCaseLogger
    {
        public void Log(IUseCase useCase, IApplicationActor actor, object data)
        {
            Console.WriteLine($"{actor.Identity} is trying to execute {useCase.Name} using data: " + $"{JsonConvert.SerializeObject(data)}");
        }
    }
}
