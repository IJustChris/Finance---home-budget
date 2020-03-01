using Finance.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Services
{
    public class HandlerTaskRunner : IHandlerTaskRunner
    {
        private readonly IHandler _handler;
        private readonly Func<Task> _validate;
        private readonly ISet<IHandlerTask> _handlerTasks = new HashSet<IHandlerTask>();


        public HandlerTaskRunner(IHandler handler, Func<Task> validate, ISet<IHandlerTask> handlerTasks)
        {
            _handler = handler;
            _validate = validate;
            _handlerTasks = handlerTasks;
        }

        public IHandlerTask RunAsync(Func<Task> run)
        {
            var handlerTask = new HandlerTask(_handler, run);
            _handlerTasks.Add(handlerTask);

            return handlerTask;
        }

        
    }
}
