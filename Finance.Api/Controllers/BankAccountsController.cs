using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finance.Infrastructure.Commands;
using Finance.Infrastructure.Commands.BankAccounts;
using Finance.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Api.Controllers
{
    [Route("BankAccounts")]
    public class BankAccountsController : ApiControllerBase
    {

        public BankAccountsController( ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            var command = new GetBankAccounts();
            await DispatchAsync(command);
            return Ok(Json(command.banks));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateBankAccount command)
        {
            // TODO: Provide created uri
            await DispatchAsync(command);
            return CreatedAtAction($"/bank/",new object());
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UpdateBankAccount command)
        {
            await DispatchAsync(command);
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]DeleteBankAccount command)
        {
            await DispatchAsync(command);
            return Ok();
        }
    }
}
