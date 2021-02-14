using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Mvc.Application.Commands.Categories;
using Shop.Mvc.Application.Models;

namespace Shop.Mvc.Areas.Panel.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Area("Panel")]
    [Route("Panel/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [HttpGet("Index")]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            return View(await _mediator.Send(new GetCategoriesCommand(), cancellationToken));
        }

        [HttpGet("Create")]
        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {
            return View(new CategoryCreateModel
            {
                Categories = await _mediator.Send(new GetCategoriesCommand(), cancellationToken),
                CreateCategoryCommand = new CreateCategoryCommand()
            });
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateCategoryCommand createCategoryCommand, CancellationToken cancellationToken)
        {
            await _mediator.Send(createCategoryCommand, cancellationToken);

            return View(new CategoryCreateModel
            {
                Categories = await _mediator.Send(new GetCategoriesCommand(), cancellationToken),
                CreateCategoryCommand = createCategoryCommand
            });
        }
    }
}
