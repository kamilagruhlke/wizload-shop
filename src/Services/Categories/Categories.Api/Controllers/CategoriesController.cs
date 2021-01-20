using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Categories.Api.Application.Commands;
using Categories.Api.Application.Models;
using Categories.Api.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Categories.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ICategoryQueries _categoryQueries;

        public CategoriesController(IMediator mediator, ICategoryQueries categoryQueries)
        {
            _mediator = mediator;
            _categoryQueries = categoryQueries;
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand createCategoryCommand,
            CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(createCategoryCommand, cancellationToken));
        }

        [HttpPut]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> UpdateCateogory([FromBody] UpdateCategoryCommand updateCategoryCommand,
            CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(updateCategoryCommand, cancellationToken));
        }

        [HttpGet("Active")]
        [ProducesResponseType(typeof(IList<CategoryModel>), 200)]
        public async Task<IActionResult> GetActiveCategories(CancellationToken cancellationToken)
        {
            return Ok(await _categoryQueries.GetActiveCategories(cancellationToken));
        }
    }
}
