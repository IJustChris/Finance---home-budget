using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Services.Interfaces
{
    public interface IHandlerTaskRunner
    {
        IHandlerTask RunAsync(Func<Task> run);
    }
}
