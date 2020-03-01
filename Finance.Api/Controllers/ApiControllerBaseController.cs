using Finance.Infrastructure.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Finance.Api.Controllers
{
    [Route("[controller]")]
    public abstract class ApiControllerBase : Controller
    {
        private readonly ICommandDispatcher _commandDispatcher;

        protected int userId => User?.Identity?.IsAuthenticated == true ?
            int.Parse(User.Identity.Name) :
            0;


        public ApiControllerBase(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        protected async Task DispatchAsync<T>(T command) where T : ICommand
        {
            if (command is IAuthenticatedCommand authenticatedCommand)
            {
                authenticatedCommand.userId = userId;
            }

            await _commandDispatcher.DispatchAsync(command);
        }

    }
}
