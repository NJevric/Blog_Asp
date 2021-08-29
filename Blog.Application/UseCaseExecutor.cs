using Blog.Application.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Application
{
    public class UseCaseExecutor
    {
        private readonly IApplicationActor _actor;
        private readonly IUseCaseLogger _logger;

        public UseCaseExecutor(IApplicationActor actor, IUseCaseLogger logger)
        {
            _actor = actor;
            _logger = logger;
        }

        public TResult ExecuteQuery<TSearch, TResult>(IQuery<TSearch, TResult> query, TSearch search)
        {
            //Console.WriteLine($"{_actor.Identity} is trying to execute {query.Name} using data: " + $"{JsonConvert.SerializeObject(search)}");

            _logger.Log(query, _actor, search);

            if (!_actor.AllowedUseCases.Contains(query.Id))
            {
                throw new UnauthorizedUseCaseException(query, _actor);
            }
            return query.Execute(search);
        }

        public void ExecuteCommand<TRequest>(ICommand<TRequest> command, TRequest request)
        {
            //Console.WriteLine($"{_actor.Identity} is trying to execute {command.Name} using data: " + $"{JsonConvert.SerializeObject(request)}");
            _logger.Log(command, _actor, request);
            if (!_actor.AllowedUseCases.Contains(command.Id))
            {
                throw new UnauthorizedUseCaseException(command, _actor);
            }

            command.Execute(request);
        }

    }
}
