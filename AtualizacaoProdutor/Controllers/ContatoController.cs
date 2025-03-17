using Core.Requests.Update;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace AtualizacaoProdutor.Controllers
{

    [ApiController]
    [Route("/[controller]")]
    public class ContatoController : ControllerBase
    {

        private readonly IBus _bus;
        private readonly IConfiguration _configuration;

        public ContatoController(IBus bus, IConfiguration configuration)
        {
            _bus = bus;
            _configuration = configuration;
        }

        /// <summary>
        /// Atualiza as informações de um contato
        /// </summary>
        /// <param name="contatoUpdateRequest">Objeto do tipo "ContatoUpdateRequest"</param>
        /// <response code="200">Contato atualizado com sucesso</response>
        /// <response code="400">Erro ao atualizar o contato</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ContatoUpdateRequest contatoUpdateRequest)
        {
            // Comentário de teste CI
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var nomeFila = _configuration.GetSection("MassTransit:Queues")["ContatoQueue"] ?? string.Empty;
                var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{nomeFila}"));
                await endpoint.Send(contatoUpdateRequest);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

    }
}
