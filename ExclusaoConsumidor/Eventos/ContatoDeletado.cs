using Core.Interfaces.Services;
using Core.Requests.Delete;
using MassTransit;

namespace ExclusaoConsumidor.Eventos
{
    public class ContatoDeletado : IConsumer<ContatoDeleteRequest>
    {
        private readonly IContatoService _contatoService;

        public ContatoDeletado(IContatoService contatoService)
        {
            _contatoService = contatoService;
        }

        public Task Consume(ConsumeContext<ContatoDeleteRequest> context)
        {
            _contatoService.Delete(context.Message.Id);
            return Task.CompletedTask;
        }
    }
}
