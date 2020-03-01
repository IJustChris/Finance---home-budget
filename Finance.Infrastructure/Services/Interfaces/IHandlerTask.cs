using Finance.Infrastructure.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Services.Interfaces
{
    public interface IHandlerTask
    {
        IHandlerTask Always(Func<Task> always);

        IHandlerTask OnCustomError(Func<ServiceException, Task> onCustomError,
            bool propagateException = false, bool executeOnError = false);

        IHandlerTask OnError(Func<Exception, Task> onError,
            bool propagateException = false, bool executeOnError = false);

        IHandlerTask OnSucces(Func<Task> onSuccess);

        IHandlerTask PropagateException();
         
        IHandlerTask DoNotPropagateException();

        IHandler Next();

        Task ExecuteAsync();

    }
}
