using System;
using MediatR;
using Shop.Mvc.Application.Models;

namespace Shop.Mvc.Application.Commands.Basket
{
    public class GetBasketProductsCommand : IRequest<BasketProductsModel>
    {
        public Guid Id { get; set; }
    }
}
