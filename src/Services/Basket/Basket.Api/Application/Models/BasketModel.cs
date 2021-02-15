using System;
using System.Collections.Generic;

namespace Basket.Api.Application.Models
{
    public class BasketModel
    {
        public IList<Guid> ProductIds { get; set; }
    }
}
