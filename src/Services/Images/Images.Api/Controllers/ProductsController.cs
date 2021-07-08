using Images.Api.Application.Commands;
using Images.Api.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

        [DisableRequestSizeLimit]
        [HttpPost("Image/Upload")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> UploadProductImage([FromBody] string fileBody,
            [FromQuery] Guid productId,
            [FromQuery] string fileName,
            CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new UploadProductImageCommand
            {
                FileName = fileName,
                FileBody = fileBody,
                ProductId = productId
            }, cancellationToken));
        }

        [DisableRequestSizeLimit]
        [HttpPost("Image/Upload/Spa")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> UploadProductImageAsFormFile([FromForm] IFormFile fileBody,
            [FromQuery] Guid productId,
            [FromQuery] string fileName,
            CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new UploadProductImageCommand
            {
                FileName = fileName,
                FileBody = Convert.ToBase64String(ReadFully(fileBody.OpenReadStream())),
                ProductId = productId
            }, cancellationToken));
        }

        private static byte[] ReadFully(Stream input)
        {
            using var memoryStream = new MemoryStream();

            input.CopyTo(memoryStream);

            return memoryStream.ToArray();
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
