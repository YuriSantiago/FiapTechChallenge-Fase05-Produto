using Core.Interfaces.Services;
using Core.Requests.Create;
using MassTransit;

namespace CadastroConsumidor.Eventos
{
    public class RegiaoCriada : IConsumer<RegiaoRequest>
    {
        private readonly IRegiaoService _regiaoService;

        public RegiaoCriada(IRegiaoService regiaoService)
        {
            _regiaoService = regiaoService;
        }

        public Task Consume(ConsumeContext<RegiaoRequest> context)
        {
            _regiaoService.Create(context.Message);
            return Task.CompletedTask;

        }
    }
}
