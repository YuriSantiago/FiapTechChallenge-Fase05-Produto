using Core.Interfaces.Services;
using Core.Requests.Create;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace CadastroProdutor.Controllers
{

    [ApiController]
    [Route("/[controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly IBus _bus;
        private readonly IConfiguration _configuration;
        private readonly IRegiaoService _regiaoService;

        // teste de CI
        public ContatoController(IBus bus, IConfiguration configuration, IRegiaoService regiaoService)
        {
            _bus = bus;
            _configuration = configuration;
            _regiaoService = regiaoService;
        }

        /// <summary>
        /// Cadastra um novo contato 
        /// </summary>
        /// <param name="contatoRequest">Objeto do tipo "ContatoRequest"</param>
        /// <response code="200">Contato cadastrado com sucesso</response>
        /// <response code="400">Erro ao cadastrar o contato</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContatoRequest contatoRequest)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var regiao = _regiaoService.GetByDDD(contatoRequest.DDD);

                if (regiao is null)
                    return NotFound("DDD não encontrado");

                var nomeFila = _configuration.GetSection("MassTransit:Queues")["ContatoQueue"] ?? string.Empty;
                var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{nomeFila}"));
                await endpoint.Send(contatoRequest);

                return Ok();
            }

            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

    }
}
