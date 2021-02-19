using Microsoft.AspNetCore.Mvc;
using Shop.Mvc.Application.Commands.Orders;

namespace Shop.Mvc.ViewComponents
{
    public class Order : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("Index", new CreateOrderCommand());
        }
    }
}
