using System.Collections.Generic;
using MediatR;
using WizLoad.ApiClient;

namespace Shop.Mvc.Application.Commands.Producers
{
    public class GetProducersCommand : IRequest<ICollection<ProducerModel>>
    {
    }
}
