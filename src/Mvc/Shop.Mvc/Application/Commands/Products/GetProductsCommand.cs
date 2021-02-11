﻿using System.Collections.Generic;
using MediatR;
using WizLoad.ApiClient;

namespace Shop.Mvc.Application.Commands.Products
{
    public class GetProductsCommand : IRequest<List<ProductModel>>
    {
    }
}
