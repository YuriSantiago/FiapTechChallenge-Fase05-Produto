using Core.Requests.Delete;
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

        public ContatoController(IBus bus, IConfiguration configuration)
        {
            _bus = bus;
            _configuration = configuration;
        }

        /// <summary>
        /// Deleta um contato por Id
        /// </summary>
        /// <param name="id">Id do Contato</param>
        /// <response code="200">Contato deletado com sucesso</response>
        /// <response code="400">Erro ao deletar o contato</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            try
            {
                var nomeFila = _configuration.GetSection("MassTransit:Queues")["ContatoQueue"] ?? string.Empty;
                var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{nomeFila}"));
                await endpoint.Send(new ContatoDeleteRequest { Id = id });

                return Ok();
            }

            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

    }
}
