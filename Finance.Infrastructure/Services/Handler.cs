using Finance.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Services
{
    public class Handler : IHandler
    {
        private readonly ISet<IHandlerTask> _handlerTasks = new HashSet<IHandlerTask>();

        public IHandlerTask RunAsync(Func<Task> run)
        {
            var handlerTask = new HandlerTask(this, run);
            _handlerTasks.Add(handlerTask);

            return handlerTask;
        }

        public IHandlerTaskRunner ValidateAsync(Func<Task> validate)
            => new HandlerTaskRunner(this, validate, _handlerTasks);

        public async Task ExecuteAllAsync()
        {
            foreach(var handlerTask in _handlerTasks)
            {
                await handlerTask.ExecuteAsync();
            }

            _handlerTasks.Clear();
        }
    }
}
