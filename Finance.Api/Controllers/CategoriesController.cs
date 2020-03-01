using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finance.Infrastructure.Commands;
using Finance.Infrastructure.Commands.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finance.Api.Controllers
{
    [Route("Categories")]
    public class CategoriesController : ApiControllerBase
    {
        public CategoriesController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }


        [Authorize]
        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            var command = new GetAllCategories();
            await DispatchAsync(command);

            return Ok(Json(command.categories));
        }

        [Authorize]
        [HttpPost("AddCategory")]
        public async Task<IActionResult> Post([FromBody]CreateCategory command)
        {
            await DispatchAsync(command);
            return await Get();
        }

        [Authorize]
        [HttpPost("AddSubcategory")]
        public async Task<IActionResult> Post([FromBody]CreateSubcategory command)
        {
            await DispatchAsync(command);
            return await Get();
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UpdateCateogry command)
        {
            await DispatchAsync(command);
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]DeleteCategory command)
        {
            await DispatchAsync(command);
            return await Get();
        }
    }
}
