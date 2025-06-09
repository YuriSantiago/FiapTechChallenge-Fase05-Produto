using Core.Interfaces.Services;
using Core.Requests.Create;
using MassTransit;

namespace CadastroConsumidor.Eventos
{
    public class ProdutoCriado : IConsumer<ProdutoRequest>
    {

        private readonly IProdutoService _produtoService;

        public ProdutoCriado(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        public Task Consume(ConsumeContext<ProdutoRequest> context)
        {
            _produtoService.Create(context.Message);
            return Task.CompletedTask;
        }

    }
}
