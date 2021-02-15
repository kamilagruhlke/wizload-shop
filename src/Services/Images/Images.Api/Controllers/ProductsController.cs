using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Images.Api.Application.Commands;
using Images.Api.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Images.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Image/Upload")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> UploadProductImage([FromBody] IFormFile inputFile,
            [FromQuery] Guid productId,
            CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new UploadProductImageCommand
            {
                File = inputFile,
                ProductId = productId
            }, cancellationToken));
        }

        [AllowAnonymous]
        [HttpGet("Images")]
        [ProducesResponseType(typeof(List<ProductImageModel>), 200)]
        public async Task<IActionResult> GetImages([FromQuery] Guid[] productIds, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetImagesByProductIdsCommand {
                ProductIds = productIds.ToList()
            }, cancellationToken));
        }
    }
}
