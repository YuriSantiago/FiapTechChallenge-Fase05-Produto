using Core.Interfaces.Services;
using Core.Requests.Delete;
using MassTransit;

namespace ExclusaoConsumidor.Eventos
{
    public class RegiaoDeletada : IConsumer<RegiaoDeleteRequest>
    {
        private readonly IRegiaoService _regiaoService;

        public RegiaoDeletada(IRegiaoService regiaoService)
        {
            _regiaoService = regiaoService;
        }

        public Task Consume(ConsumeContext<RegiaoDeleteRequest> context)
        {
            _regiaoService.Delete(context.Message.Id);
            return Task.CompletedTask;
        }
    }
}
