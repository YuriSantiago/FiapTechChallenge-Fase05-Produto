using Core.Requests.Create;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace CadastroProdutor.Controllers
{

    [ApiController]
    [Route("/[controller]")]
    public class RegiaoController : ControllerBase
    {
        private readonly IBus _bus;
        private readonly IConfiguration _configuration;

        public RegiaoController(IBus bus, IConfiguration configuration)
        {
            _bus = bus;
            _configuration = configuration;
        }

        /// <summary>
        /// Cadastra uma nova região 
        /// </summary>
        /// <param name="regiaoRequest">Objeto do tipo "RegiaoRequest"</param>
        /// <response code="200">Região cadastrada com sucesso</response>
        /// <response code="400">Erro ao cadastrar a região</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegiaoRequest regiaoRequest)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var nomeFila = _configuration.GetSection("MassTransit:Queues")["RegiaoQueue"] ?? string.Empty;
                var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{nomeFila}"));
                await endpoint.Send(regiaoRequest);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

    }
}
