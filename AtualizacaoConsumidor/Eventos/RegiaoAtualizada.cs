using Core.Interfaces.Services;
using Core.Requests.Update;
using MassTransit;

namespace AtualizacaoConsumidor.Eventos
{
    public class RegiaoAtualizada : IConsumer<RegiaoUpdateRequest>
    {
        private readonly IRegiaoService _regiaoService;

        public RegiaoAtualizada(IRegiaoService regiaoService)
        {
            _regiaoService = regiaoService;
        }

        public Task Consume(ConsumeContext<RegiaoUpdateRequest> context)
        {
            _regiaoService.Put(context.Message);
            return Task.CompletedTask;
        }
    }
}
