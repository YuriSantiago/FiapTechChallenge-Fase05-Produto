using Core.Interfaces.Services;
using Core.Requests.Create;
using MassTransit;

namespace CadastroConsumidor.Eventos
{
    public class ContatoCriado : IConsumer<ContatoRequest>
    {
        private readonly IContatoService _contatoService;

        public ContatoCriado(IContatoService contatoService)
        {
            _contatoService = contatoService;
        }

        public Task Consume(ConsumeContext<ContatoRequest> context)
        {
            _contatoService.Create(context.Message);
            return Task.CompletedTask;
        }
    }
}
