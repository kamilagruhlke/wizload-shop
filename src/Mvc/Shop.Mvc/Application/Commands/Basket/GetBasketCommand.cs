using System;
using MediatR;
using WizLoad.ApiClient;

namespace Shop.Mvc.Application.Commands.Basket
{
    public class GetBasketCommand : IRequest<BasketModel>
    {
        public Guid Id { get; set; }
    }
}
