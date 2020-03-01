using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Services.Interfaces
{
    public interface IHandler
    {
        IHandlerTask RunAsync(Func<Task> run);

        IHandlerTaskRunner ValidateAsync(Func<Task> validate);

        Task ExecuteAllAsync();
    }
}
