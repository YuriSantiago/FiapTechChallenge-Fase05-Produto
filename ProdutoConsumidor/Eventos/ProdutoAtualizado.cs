using Core.Interfaces.Services;
using Core.Requests.Update;
using MassTransit;

namespace ProdutoConsumidor.Eventos
{
    public class ProdutoAtualizado : IConsumer<ProdutoUpdateRequest>
    {

        private readonly IProdutoService _produtoService;

        public ProdutoAtualizado(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        public Task Consume(ConsumeContext<ProdutoUpdateRequest> context)
        {
            _produtoService.Put(context.Message);
            return Task.CompletedTask;
        }

    }
}
