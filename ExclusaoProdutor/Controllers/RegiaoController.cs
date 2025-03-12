using Core.Requests.Delete;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace ExclusaoProdutor.Controllers
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
        /// Deleta uma região por Id
        /// </summary>
        /// <param name="id">Id da região</param>
        /// <response code="200">Região deletada com sucesso</response>
        /// <response code="400">Erro ao deletar a região</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var nomeFila = _configuration.GetSection("MassTransit:Queues")["RegiaoQueue"] ?? string.Empty;
                var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{nomeFila}"));
                await endpoint.Send(new RegiaoDeleteRequest { Id = id });

                return Ok();
            }

            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

    }
}
