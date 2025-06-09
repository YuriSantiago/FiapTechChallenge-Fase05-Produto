using Core.Interfaces.Services;
using Core.Requests.Delete;
using MassTransit;

namespace ProdutoConsumidor.Eventos
{
    public class ProdutoDeletado : IConsumer<ProdutoDeleteRequest>
    {
        private readonly IProdutoService _produtoService;

        public ProdutoDeletado(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        public Task Consume(ConsumeContext<ProdutoDeleteRequest> context)
        {
            _produtoService.Delete(context.Message.Id);
            return Task.CompletedTask;
        }
    }
}
