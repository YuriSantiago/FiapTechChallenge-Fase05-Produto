using Core.Interfaces.Services;
using Core.Requests.Create;
using Core.Requests.Delete;
using Core.Requests.Update;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProdutoProdutor.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ProdutoController : ControllerBase
    {
        // Comentário para teste de esteira
        private readonly IBus _bus;
        private readonly IConfiguration _configuration;
        private readonly ICategoriaService _categoriaService;
        private readonly IProdutoService _produtoService;

        public ProdutoController(IBus bus, IConfiguration configuration, ICategoriaService categoriaService, IProdutoService produtoService)
        {
            _bus = bus;
            _configuration = configuration;
            _categoriaService = categoriaService;
            _produtoService = produtoService;
        }

        /// <summary>
        /// Busca todos os produtos cadastrados
        /// </summary>
        /// <returns>Retorna todos os produtos cadastrados</returns>
        /// <response code="200">Listagem retornada com sucesso</response>
        /// <response code="400">Erro ao listar os produtos</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_produtoService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Busca um produto por Id
        /// </summary>
        /// <param name="id">Id do produto</param>
        /// <returns>Retorna um produto filtrado por um Id</returns>
        /// <response code="200">Produto retornado com sucesso</response>
        /// <response code="400">Erro ao buscar o produto</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpGet("{id:int}")]
        public IActionResult Get([FromRoute] int id)
        {
            try
            {
                return Ok(_produtoService.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Busca produtos por categoria
        /// </summary>
        /// <param name="categoria">Categoria dos produtos</param>
        /// <returns>Retorna uma lista de produtos filtrada por uma categoria</returns>
        /// <response code="200">Produtos retornados com sucesso</response>
        /// <response code="400">Erro ao buscar a lista de produtos por categoria</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpGet("getAllByCategory/{categoriaId}")]
        public IActionResult GetAllByCategory([FromRoute] int categoriaId)
        {
            try
            {
                return Ok(_produtoService.GetAllByCategory(categoriaId));
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Cadastra um novo produto 
        /// </summary>
        /// <param name="produtoRequest">Objeto do tipo "ProdutoRequest"</param>
        /// <response code="200">Produto cadastrado com sucesso</response>
        /// <response code="400">Erro ao cadastrar o produto</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost]
        [Authorize(Roles = "ADMIN, FUNCIONARIO")]
        public async Task<IActionResult> Post([FromBody] ProdutoRequest produtoRequest)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var categoria = _categoriaService.GetById(produtoRequest.CategoriaId);

                if (categoria is null)
                    return NotFound("Categoria não encontrada");

                var nomeFila = _configuration.GetSection("MassTransit:Queues")["ProdutoCadastroQueue"] ?? string.Empty;
                var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{nomeFila}"));
                await endpoint.Send(produtoRequest);

                return Ok();
            }

            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza as informações de um produto
        /// </summary>
        /// <param name="produtoUpdateRequest">Objeto do tipo "ProdutoUpdateRequest"</param>
        /// <response code="200">Produto atualizado com sucesso</response>
        /// <response code="400">Erro ao atualizar o produto</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPut]
        [Authorize(Roles = "ADMIN, FUNCIONARIO")]
        public async Task<IActionResult> Put([FromBody] ProdutoUpdateRequest produtoUpdateRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var nomeFila = _configuration.GetSection("MassTransit:Queues")["ProdutoAtualizacaoQueue"] ?? string.Empty;
                var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{nomeFila}"));
                await endpoint.Send(produtoUpdateRequest);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Deleta um produto por Id
        /// </summary>
        /// <param name="id">Id do Produto</param>
        /// <response code="200">Produto deletado com sucesso</response>
        /// <response code="400">Erro ao deletar o produto</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpDelete]
        [Authorize(Roles = "ADMIN, FUNCIONARIO")]
        public async Task<IActionResult> Delete([FromBody] ProdutoDeleteRequest produtoDeleteRequest)
        {

            try
            {
                var nomeFila = _configuration.GetSection("MassTransit:Queues")["ProdutoExclusaoQueue"] ?? string.Empty;
                var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{nomeFila}"));
                await endpoint.Send(new ProdutoDeleteRequest { Id = produtoDeleteRequest.Id });

                return Ok();
            }

            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }


    }
}
