using Core.Interfaces.Services;
using Core.Requests.Update;
using MassTransit;

namespace AtualizacaoConsumidor.Eventos
{
    public class ContatoAtualizado : IConsumer<ContatoUpdateRequest>
    {
        private readonly IContatoService _contatoService;

        public ContatoAtualizado(IContatoService contatoService)
        {
            _contatoService = contatoService;
        }

        public Task Consume(ConsumeContext<ContatoUpdateRequest> context)
        {
            _contatoService.Put(context.Message);
            return Task.CompletedTask;
        }
    }
}
