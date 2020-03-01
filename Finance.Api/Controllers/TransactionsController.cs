using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finance.Infrastructure.Commands;
using Finance.Infrastructure.Commands.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Api.Controllers
{
    [Route("Transactions")]
    public class TransactionsController : ApiControllerBase
    {
        public TransactionsController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }


        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetAllTransactions();

            await DispatchAsync(command);

            return Ok(Json(command.transactions));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateTransaction command)
        {
            await DispatchAsync(command);
            return await GetAll();
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UpdateTransaction command)
        {
            //TODO: implement after DB
            await DispatchAsync(command);
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]DeleteTransaction command)
        {
            await DispatchAsync(command);
            return await GetAll();
        }
    }
}
